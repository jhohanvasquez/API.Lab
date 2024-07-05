using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Contracts
{
    public interface IUserRepository
    {
        Task SaveUser(User data);

        Task<List<User>> GetUser(string cedula);
    }
}
