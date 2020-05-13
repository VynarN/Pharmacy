using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<ManufacturerController> _logger;

        public CategoryController(ILogger<ManufacturerController> logger)
        {

        }
    }
}