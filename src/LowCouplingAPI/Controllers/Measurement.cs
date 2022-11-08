using System.Text.Json.Serialization;
using System.Text.Json;

namespace LowCouplingAPI.Controllers;

public abstract class Measurement
{
    public string Type { get; protected set; } = string.Empty;
}

public abstract class GenericMeasurement<TDataType> : Measurement where TDataType : notnull
{
    public TDataType Value { get; set; } = default!;
}


public class WindDirectionMeasurement : GenericMeasurement<string>
{
    public WindDirectionMeasurement()
    {
        Type = "WindDirection";
    }
}

public class WindSpeedMeasurement : GenericMeasurement<decimal>
{
    public WindSpeedMeasurement()
    {
        Type = "WindSpeed";
    }
}

public class TemperatureMeasurement : GenericMeasurement<decimal>
{
    public TemperatureMeasurement()
    {
        Type = "Temperature";
    }
}

public class MeasurementJsonConverter : JsonConverter<Measurement>
{
    public override bool CanConvert(Type typeToConvert) =>
        typeof(Measurement).IsAssignableFrom(typeToConvert);

    public override Measurement Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {

        if (reader.TokenType == JsonTokenType.None)
            reader.Read();

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();
        reader.Read();
        if (reader.TokenType != JsonTokenType.PropertyName)
            throw new JsonException();
        var propertyName = reader.GetString();
        if (propertyName != "Type")
            throw new JsonException();
        reader.Read();
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException();
        var type = reader.GetString();

        Measurement measurement = type switch
        {
            "WindDirection" => new WindDirectionMeasurement(),
            "WindSpeed" => new WindSpeedMeasurement(),
            "Temperature" => new TemperatureMeasurement(),

            _ => throw new JsonException("Conversion of Type Not Supported")
        };
        
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                propertyName = reader.GetString();
                reader.Read();
                switch (propertyName)
                {
                    case "Value":
                        switch (measurement)
                        {
                            case WindDirectionMeasurement directionMeasurement:
                                directionMeasurement.Value = reader.GetString()!;
                                break;
                            case WindSpeedMeasurement speedMeasurement:
                                speedMeasurement.Value = reader.GetDecimal();
                                break;
                            case TemperatureMeasurement temperatureMeasurement:
                                temperatureMeasurement.Value = reader.GetDecimal();
                                break;
                        }
                        break;
                }
            }
        }
        return measurement;
    }


    public override void Write(Utf8JsonWriter writer,
        Measurement measurement, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        switch (measurement)
        {
            case WindDirectionMeasurement directionMeasurement:
                writer.WriteString("Type", "WindDirection");
                writer.WriteString("Value", directionMeasurement.Value);
                break;
            case WindSpeedMeasurement speedMeasurement:
                writer.WriteString("Type", "WindSpeed");
                writer.WriteNumber("Value", speedMeasurement.Value);
                break;
            case TemperatureMeasurement temperatureMeasurement:
                writer.WriteString("Type", "Temperature");
                writer.WriteNumber("Value", temperatureMeasurement.Value);
                break;
        }

        writer.WriteEndObject();
    }
}