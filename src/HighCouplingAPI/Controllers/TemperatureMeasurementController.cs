using HighCouplingAPI.Application;
using Microsoft.AspNetCore.Mvc;

namespace HighCouplingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureMeasurementController : ControllerBase
    {
        private readonly IRepositoryBase _repository;

        public TemperatureMeasurementController(IRepositoryBase repository)
        {
            _repository = repository;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, TemperatureMeasurement temperatureMeasurement)
        {
            await _repository.Create(temperatureMeasurement);
            return NoContent();
        }
    }
}

