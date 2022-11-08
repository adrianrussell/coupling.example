using HighCouplingAPI.Controllers;

namespace HighCouplingAPI.Test.Controllers
{
    public class TemperatureMeasurementControllerFixture
    {
        [Fact]
        public void Put_WhenMeasurementReceived_SavesMeasurement()
        {
            RepositoryBaseMock repository = new RepositoryBaseMock();
            TemperatureMeasurementController controller = new TemperatureMeasurementController(repository);

            var result = controller.Put(1, new TemperatureMeasurement { Id = 1, DegreesCentigrade = 2 });

            Assert.True(result.IsCompleted);
            Assert.Equal(1, repository.CreateCalledCounter);
        }
    }
}
