using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Api.Auxiliary;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Authorize(Roles = "manager,admin,mainadmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IImageService imageService, ILogger<ImageController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }

        [HttpPost]
        [Route("add/{medicamentId}")]
        public async Task<IActionResult> AddImages(IList<IFormFile> images, int medicamentId)
        {
            try
            {
                await _imageService.CreateImages(images, medicamentId);

                return Ok();
            }
            catch(Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpDelete("delete/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            try
            {
                await _imageService.DeleteImage(imageId);

                return Ok();
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
    }
}