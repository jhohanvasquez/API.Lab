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
        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public async Task<ActionResult<Task<List<User>>>> GetUsers(string cedula)
        {
            try
            {
                var userRepo = await _userService.GetUser(cedula);
                if (userRepo.Count > 0)
                {
                    return Ok(userRepo);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}