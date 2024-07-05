using API.Domain.Contracts;
using API.Domain.Entities;
using API.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Repositories
{
    public class UserStubRepository : IUserRepository
    {
        public UserStubRepository()
        {

        }

        public Task<List<User>> GetUser(string cedula)
        {
            List<User> oListUser = new List<User>();
            if(cedula == "123")
            {
                User user = new User
                {
                    Cedula = "123",
                    Nombre = "user 1",
                    Email = "user1@hotmail.com",
                    Ciudad = 1
                };
                oListUser.Add(user);
            }           

            return Task.FromResult(oListUser);
        }

        public Task SaveUser(User data)
        {
            return Task.CompletedTask;
        }
    }
}
