using HrManagement.Application.Positions;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var positions = await _positionService.GetAllAsync();
            return Ok(positions);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var position = await _positionService.GetByIdAsync(id);
            return Ok(position);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePositionRequest request)
        {
            var positionId = await _positionService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, positionId);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatePositionRequest request)
        {
            var result = await _positionService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _positionService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
