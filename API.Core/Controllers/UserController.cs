using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Options;
using AutoMapper;
using API.Domain.Interfaces;
using API.Domain.Configuration;
using API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.DTOs;

namespace API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
        [Route("SaveUser")]
        public ActionResult SaveUser([FromBody] User user)
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

        [HttpGet]
        [Route("GetUsers")]
        public ActionResult<List<User>> GetUsers(string cedula)
        {
            try
            {
                var userRepo = _userService.GetUser(cedula);
                return Ok(_mapper.Map<List<UserDTO>>(userRepo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}