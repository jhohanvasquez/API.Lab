using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Interfaces
{
    public interface IUserService
    {
        void SendUser(User user);
        Task<List<User>> GetUser(string email);
    }
}

