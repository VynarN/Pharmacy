using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentFormController : ControllerBase
    {
        private readonly IMedicamentFormService _medicamentFormService;

        private readonly ILogger<MedicamentFormController> _logger;

        public MedicamentFormController(IMedicamentFormService medicamentFormService, ILogger<MedicamentFormController> logger)
        {
            _medicamentFormService = medicamentFormService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMedicamentForm(string medicamentForm)
        {
            return Ok();
        }
    }
}