using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using HighCouplingAPI.Controllers;


namespace HighCouplingAPI.Test.Integration
{
    public class TemperatureControllerFixture
    {
        [Fact]
        public async Task Put()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(_ =>
                { });

            var client = application.CreateClient();
            var payload = JsonSerializer.Serialize(new TemperatureMeasurement { Id = 1, DegreesCentigrade = 1 });


            var response = await client.PutAsync("api/TemperatureMeasurement/1", new StringContent(payload, Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
        }
    }
}
