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
    }
}
