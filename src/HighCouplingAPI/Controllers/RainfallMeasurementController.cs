using HighCouplingAPI.Application;
using Microsoft.AspNetCore.Mvc;

namespace HighCouplingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallMeasurementController : ControllerBase
    {
        private readonly IRepositoryBase _repository;

        public RainfallMeasurementController(IRepositoryBase repository)
        {
            _repository = repository;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, RainfallMeasurement rainfallMeasurement)
        {
            await _repository.Create(rainfallMeasurement);
            return NoContent();

        }
    }
}

