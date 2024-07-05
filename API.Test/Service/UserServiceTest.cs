using API.Domain.Entities;
using API.Domain.Services;
using API.Test.Repositories;
using Assert = NUnit.Framework.Assert;
using API.Domain.Interfaces;
using API.Domain.Contracts;
using API.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace API.Test.Service
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly IUserService userService;
        private readonly IUserRepository userRepository;
        private readonly UserController userController;


        public UserServiceTest()
        {
            userRepository = new UserStubRepository();
            userService = new UserService(userRepository);
            userController = new UserController(userService);
        }

        [TestMethod]
        public async Task GetOk()
        {
            int statusCodeExpec = 200;
            var cedula = "123";
            var response = await userController.GetUsers(cedula);
            var result = response.Result as OkObjectResult;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(statusCodeExpec, result.StatusCode);
        }

        [TestMethod]
        public async Task GetFailNoFound()
        {
            int statusCodeExpec = 204;
            var cedula = "1234";
            var response = await userController.GetUsers(cedula);
            var result = response.Result as NoContentResult;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(statusCodeExpec, result.StatusCode);
        }
    }
}
