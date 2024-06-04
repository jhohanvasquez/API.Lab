using AutoMapper;
using Dapper;
using Microsoft.Extensions.Options;
using API.Domain.Configuration;
using API.Domain.Contracts;
using API.Domain.Entities;
using API.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {


        private readonly SqlSettings sqlSettings;
        private readonly IDbManager<LogUserDTO> dbManager;

        public UserRepository(IOptions<ApiSettings> settings, IDbManager<LogUserDTO> dbManager)
        {
            this.sqlSettings = settings.Value.environmentVariables.SqlSettings;
            this.dbManager = dbManager;
        }


        public async Task SaveUser(User data)
        {
            try
            {
                User commandResponse = Mapper.Map<User>(data);
                var queryParameters = GetParametersSave(commandResponse);
                await this.dbManager.ExecuteStoreProcedure(sqlSettings.SPSaveUser, queryParameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private DynamicParameters GetParametersSave(User data)
        {

            var queryParameters = new DynamicParameters();

            queryParameters.Add("@Cedula", data.Cedula);
            queryParameters.Add("@Nombre", data.Nombre);
            queryParameters.Add("@Apellidos", data.Apellidos);
            queryParameters.Add("@Email", data.Email);
            queryParameters.Add("@Ciudad", data.Ciudad, DbType.Int32);

            return queryParameters;
        }

        public async Task<List<User>> GetUser(string email)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@email", email);
                var readers = await this.dbManager.ExecuteReaderStoreProcedure(sqlSettings.SPGetUser, queryParameters);

                return MapUsers(readers);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private List<User> MapUsers(IEnumerable<Object> readers)
        {
            List<User> users = new List<User>();

            foreach (IDictionary<string, object> row in readers)
            {

                var user = new User()
                {
                    Cedula = row.FirstOrDefault(x => x.Key == "Cedula").Value.ToString(),
                    Nombre = row.FirstOrDefault(x => x.Key == "Nombre").Value.ToString(),
                    Apellidos = row.FirstOrDefault(x => x.Key == "Apellidos").Value.ToString(),
                    Email = row.FirstOrDefault(x => x.Key == "Email").Value.ToString(),
                    Ciudad = Convert.ToInt32(row.FirstOrDefault(x => x.Key == "Ciudad").Value)
                };
                users.Add(user);
            }

            return users;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        public void Dispose()
        {

            Dispose(true);

            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
