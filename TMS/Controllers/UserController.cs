using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using TMS.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace TMS.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        public IUserService userService;
        private readonly Mapper userMapper;

        public UserController()
        {
            userService = new UserService();
            var userConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserVM>();
                cfg.CreateMap<UserVM, UserDTO>();
            });
            userMapper = new Mapper(userConfig);
        }

        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(UserVM userVM)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userDTOs = userMapper.Map<List<UserVM>>(userService.GetUsers());
                var existingUser = userDTOs.Where(u => u.UserName  == userVM.UserName).FirstOrDefault();
                if (existingUser != null)
                {
                    return Content(HttpStatusCode.Conflict, new { error = "User already Exist" });
                }
                var newUser = userMapper.Map<UserDTO>(userVM);
                userService.Register(newUser);
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Registering: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }
}