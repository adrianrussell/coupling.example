using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using LowCouplingAPI.Controllers;


namespace LowCouplingAPI.Test.Integration
{
    public class RainfallControllerFixture
    {
        [Fact]
        public async Task Put()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(_ =>
                { });

            var client = application.CreateClient();
            var payload = JsonSerializer.Serialize(new RainfallMeasurement { Id = 1, MillimetersPerHour = 1 });


            var response = await client.PutAsync("api/RainfallMeasurement/1", new StringContent(payload, Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
        }
    }
}
