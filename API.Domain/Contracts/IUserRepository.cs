using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Contracts
{
    public interface IUserRepository : IDisposable
    {
        Task SaveUser(User data);

        Task<List<User>> GetUser(string email);
    }
}
