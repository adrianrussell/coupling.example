using HighCouplingAPI.Controllers;

namespace HighCouplingAPI.Test.Controllers
{
    public class RainfallMeasurementControllerFixture
    {
        [Fact]
        public void Put_WhenMeasurementReceived_SavesMeasurement()
        {
            RepositoryBaseMock repository = new RepositoryBaseMock();
            RainfallMeasurementController controller = new RainfallMeasurementController(repository);

            var result = controller.Put(1, new RainfallMeasurement { Id = 1, MillimetersPerHour = 2 });

            Assert.True(result.IsCompleted);
            Assert.Equal(1, repository.CreateCalledCounter);
        }
    }
}
