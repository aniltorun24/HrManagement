using HrManagement.Application.Departments;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateDepartmentRequest request)
        {
            var departmentId = await _departmentService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, departmentId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateDepartmentRequest request)
        {
            var result = await _departmentService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _departmentService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
