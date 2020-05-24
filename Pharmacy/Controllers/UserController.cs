using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Api.Auxiliary;
using Pharmacy.Application.Common.DTO.Out;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Authorize(Roles = "admin,mainadmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;
        private readonly IPaginationService _paginationService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ICurrentUser currentUser, ILogger<UserController> logger, 
                              IMapper mapper, IPaginationService paginationService)
        {
            _userService = userService;
            _currentUser = currentUser;
            _paginationService = paginationService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                await _userService.DeleteUser(currentUserId, userId);

                return Ok();
            }
            catch(ObjectException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpPut("promote/{userId}")]
        public async Task<IActionResult> PromoteUser(string userId, string role)
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                await _userService.PromoteToRole(currentUserId, userId, role);

                return Ok();
            }
            catch (ObjectException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
        
        [HttpPut("demote/{userId}")]
        public async Task<IActionResult> DemoteUser(string userId, string role)
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                await _userService.DemoteToRole(currentUserId, userId, role);

                return Ok();
            }
            catch (ObjectException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpGet("get/{role}")]
        public async Task<IActionResult> GetUserInRole([FromQuery]PaginationQuery paginationQuery, string role)
        {
            try
            {
                var usersAndTheirTotalNumber = await _userService.GetUsersInRole(role, paginationQuery);

                var mappedUsers = _mapper.Map<IEnumerable<UserOutDto>>(usersAndTheirTotalNumber.Users);

                var paginatedReponse = _paginationService.FormPaginatedResponse(usersAndTheirTotalNumber.Total, mappedUsers, paginationQuery);

                return Ok(paginatedReponse);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
    }
}