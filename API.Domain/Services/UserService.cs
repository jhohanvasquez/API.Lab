using API.Domain.Configuration;
using API.Domain.Contracts;
using API.Domain.Entities;
using API.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace API.Domain.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;

        bool disposed = false;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #region Public Members

        public async Task<List<User>> GetUser(string cedula)
        {
            try
            {
                return await this.userRepository.GetUser(cedula);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SendUser(User user)
        {
            try
            {
                this.userRepository.SaveUser(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion


        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                userRepository.Dispose();
            }

            disposed = true;
        }
        #endregion

    }
}
