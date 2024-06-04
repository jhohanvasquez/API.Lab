using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Options;
using AutoMapper;
using API.Domain.Interfaces;
using API.Domain.Configuration;
using API.Domain.Entities;

namespace Satrack.userVehicle.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SqlSettings _settings;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IOptions<ApiSettings> settings, IMapper mapper)
        {
            _userService = userService;
            _settings = settings.Value.environmentVariables.SqlSettings;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            try
            {
                _userService.SendUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}