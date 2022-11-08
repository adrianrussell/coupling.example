using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using LowCouplingAPI.Controllers;


namespace LowCouplingAPI.Test.Integration
{
    public class MeasurementControllerFixture
    {
        [Fact]
        public async Task Put()
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new MeasurementJsonConverter()
                }
            };


            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(_ =>
                { });
            
            var client = application.CreateClient();
            var device = new DeviceReading();
            device.Id = 1;
            device.Device = "WindSock";
            device.Measurements.Add(new WindDirectionMeasurement {Value = "N"});
            
            var payload = JsonSerializer.Serialize(device, serializeOptions);
            
            var response = await client.PutAsync("api/Measurement/1", new StringContent(payload, Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
        }
    }
}
