using LowCouplingAPI.Application;
using Microsoft.AspNetCore.Mvc;

namespace LowCouplingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IRepositoryBase _repository;

        public MeasurementController(IRepositoryBase repository)
        {
            _repository = repository;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] DeviceReading deviceReading)
        {
            await _repository.Create(deviceReading);
            return NoContent();
        }
    }
}
