using LowCouplingAPI.Application;
using LowCouplingAPI.Controllers;

namespace LowCouplingAPI.Test.Controllers
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

    public class RepositoryBaseMock : IRepositoryBase
    {
        public int CreateCalledCounter { get; private set; }

        public int UpdateCalledCounter { get; private set; }

        public Task Create<T>(T entity)
        {
            CreateCalledCounter++;
            return Task.CompletedTask;
        }

        public Task Update<T>(T entity)
        {
            UpdateCalledCounter++;
            return Task.CompletedTask;
        }
    }
}