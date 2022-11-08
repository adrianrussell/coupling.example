using LowCouplingAPI.Controllers;

namespace LowCouplingAPI.Test.Controllers
{
    public class MeasurementControllerFixture
    {
        [Fact]
        public void Put_WhenMeasurementReceived_SavesMeasurement()
        {
            RepositoryBaseMock repository = new RepositoryBaseMock();
            MeasurementController controller = new MeasurementController(repository);

            var measurements = new List<Measurement> { new WindDirectionMeasurement {Value = "N"} };
            var result = controller.Put(1, new DeviceReading {Id = 1,Measurements = measurements});

            Assert.True(result.IsCompleted);
            Assert.Equal(1, repository.CreateCalledCounter);
        }
    }
}
