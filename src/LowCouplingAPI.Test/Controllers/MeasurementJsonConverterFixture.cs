using System.Text.Json;
using LowCouplingAPI.Controllers;

namespace LowCouplingAPI.Test.Controllers
{
    public class MeasurementFixture
    {
        [Fact]
        public void WindSpeedCanConvert()
        {
            var converter = new MeasurementJsonConverter();
            var result = converter.CanConvert(typeof(WindSpeedMeasurement));

            Assert.True(result);
        }

        [Fact]
        public void DirectionSpeed_CanConvert()
        {
            var converter = new MeasurementJsonConverter();
            var result = converter.CanConvert(typeof(WindDirectionMeasurement));

            Assert.True(result);
        }

        [Fact]
        public void Temperature_CanConvert()
        {
            var converter = new MeasurementJsonConverter();
            var result = converter.CanConvert(typeof(TemperatureMeasurement));

            Assert.True(result);
        }


        [Fact]
        public void WindSpeedReader_ValidJSON_Converts()
        {
            var converter = new MeasurementJsonConverter();
            string json = "{\r\n  \"Type\": \"WindSpeed\",\r\n  \"Value\": 0.1\r\n}";
            var reader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(json));
            
            var result = converter.Read(ref reader, typeof(Measurement),new JsonSerializerOptions()) as WindSpeedMeasurement;

            Assert.IsType<WindSpeedMeasurement>(result);
            Assert.Equal(0.1M,result!.Value);
        }
        
        [Fact]
        public void WindDirectionReader_ValidJSON_Converts()
        {
            var converter = new MeasurementJsonConverter();
            string json =  @"{""Type"" : ""WindDirection"",""Value"" : ""N""}";

            var reader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(json));

            var result = converter.Read(ref reader, typeof(Measurement), new JsonSerializerOptions()) as WindDirectionMeasurement;

            Assert.IsType<WindDirectionMeasurement>(result);
            Assert.Equal("N", result!.Value);
        }

        [Fact]
        public void Temperature_ValidJSON_Converts()
        {
            var converter = new MeasurementJsonConverter();
            string json = @"{""Type"" : ""Temperature"",""Value"" : 0.1}";

            var reader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(json));

            var result = converter.Read(ref reader, typeof(Measurement), new JsonSerializerOptions()) as TemperatureMeasurement;

            Assert.IsType<TemperatureMeasurement>(result);
            Assert.Equal(0.1M, result!.Value);
        }

        [Fact]
        public void WindSpeedWriter_ValidJSON_Converts()
        {
            var windSpeed = new WindSpeedMeasurement { Value = 0.1M };

            var serializeOptions = JsonSerializerOptions();

            var stringValue = JsonSerializer.Serialize(windSpeed, serializeOptions);

            Assert.Equal("{\r\n  \"Type\": \"WindSpeed\",\r\n  \"Value\": 0.1\r\n}", stringValue);
        }

        [Fact]
        public void WindDirectionWriter_ValidJSON_Converts()
        {
            var windDirectionMeasurement = new WindDirectionMeasurement { Value = "N" };
            
            var serializeOptions = JsonSerializerOptions();

            var stringValue = JsonSerializer.Serialize(windDirectionMeasurement, serializeOptions);

            Assert.Equal("{\r\n  \"Type\": \"WindDirection\",\r\n  \"Value\": \"N\"\r\n}",stringValue);
        }

        [Fact]
        public void TemperatureWriter_ValidJSON_Converts()
        {
            var temperature = new TemperatureMeasurement { Value = 0.1M };

            var serializeOptions = JsonSerializerOptions();

            var stringValue = JsonSerializer.Serialize(temperature, serializeOptions);

            Assert.Equal("{\r\n  \"Type\": \"Temperature\",\r\n  \"Value\": 0.1\r\n}", stringValue);
        }

        private static JsonSerializerOptions JsonSerializerOptions()
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new MeasurementJsonConverter()
                }
            };
            return serializeOptions;
        }
    }
}
