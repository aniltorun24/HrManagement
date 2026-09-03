using HrManagement.Application.Companies;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyRequest request)
        {
            var result = await _companyService.CreateAsync(request);

            return StatusCode(StatusCodes.Status201Created, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _companyService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _companyService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateCompanyAsync request)
        {
            var result = await _companyService.UpdateAsync(id, request);
            return Ok(result);

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _companyService.DeleteAsync(id);
            return Ok(result);

        }
    }
}
