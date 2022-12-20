using HighCouplingAPI.Application;
using Microsoft.AspNetCore.Mvc;

namespace HighCouplingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindController : ControllerBase
    {
        private readonly IRepositoryBase _repository;

        public WindController(IRepositoryBase repository)
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

