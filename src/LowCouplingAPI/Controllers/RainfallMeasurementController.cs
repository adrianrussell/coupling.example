using LowCouplingAPI.Application;
using Microsoft.AspNetCore.Mvc;

namespace LowCouplingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallMeasurementController : ControllerBase
    {
        private IRepositoryBase _repository;

        public RainfallMeasurementController(IRepositoryBase repository)
        {
            _repository = repository;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody]RainfallMeasurement rainfallMeasurement)
        {
            await _repository.Create(rainfallMeasurement);
            return NoContent();
        }
    }
}
