using System.Text.Json;
using LowCouplingAPI.Controllers;

namespace LowCouplingAPI.Test.Controllers
{
    public class MeasurementFixture
    {
        [Theory]
        [InlineData(true, typeof(WindSpeedMeasurement))]
        [InlineData(true, typeof(WindDirectionMeasurement))]
        [InlineData(true, typeof(TemperatureMeasurement))]
        [InlineData(true, typeof(RainMeasurement))]

        public void CanConvert(bool expected, Type type)
        {
            var converter = new MeasurementJsonConverter();
            var result = converter.CanConvert(type);

            Assert.Equal(expected,result);
        }

        [Theory]
        [InlineData("WindSpeed", 0.1, typeof(WindSpeedMeasurement))]
        [InlineData("Temperature", 0.1, typeof(TemperatureMeasurement))]
        [InlineData("Rain", 0.1, typeof(RainMeasurement))]

        public void DecimalMeasurementReader_ValidJSON_Converts(string typeValue, decimal expected, Type type)
        {
            var converter = new MeasurementJsonConverter();
            string json = $"{{\r\n  \"Type\": \"{typeValue}\",\r\n  \"Value\": {expected}\r\n}}";
            var reader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(json));
            
            var result = (GenericMeasurement<decimal>)converter.Read(ref reader, typeof(Measurement),new JsonSerializerOptions());

            Assert.IsType(type, result);
            Assert.Equal(expected, result!.Value);
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

        [Theory]
        [InlineData("WindSpeed", 0.1, typeof(WindSpeedMeasurement))]
        [InlineData("Temperature", 0.1, typeof(TemperatureMeasurement))]
        [InlineData("Rain", 0.1, typeof(RainMeasurement))]
        public void DecimalMeasurementWriter_ValidJSON_Converts(string typeValue, decimal expected, Type type)
        {
            var windSpeed = (GenericMeasurement<Decimal>)Activator.CreateInstance(type)!;
            windSpeed!.Value = expected;

            var serializeOptions = JsonSerializerOptions();

            var stringValue = JsonSerializer.Serialize(windSpeed, serializeOptions);

            Assert.Equal($"{{\r\n  \"Type\": \"{typeValue}\",\r\n  \"Value\": {expected}\r\n}}", stringValue);
        }

        [Fact]
        public void WindDirectionWriter_ValidJSON_Converts()
        {
            var windDirectionMeasurement = new WindDirectionMeasurement { Value = "N" };
            
            var serializeOptions = JsonSerializerOptions();

            var stringValue = JsonSerializer.Serialize(windDirectionMeasurement, serializeOptions);

            Assert.Equal("{\r\n  \"Type\": \"WindDirection\",\r\n  \"Value\": \"N\"\r\n}",stringValue);
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
