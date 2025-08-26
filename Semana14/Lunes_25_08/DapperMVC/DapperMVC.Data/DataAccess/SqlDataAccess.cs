
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DapperMVC.Data.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "DefaultConnection")
        {
            using IDbConnection dbConnection =
                new SqlConnection(_config.GetConnectionString(connectionId));

            return await dbConnection.QueryAsync<T>(spName, parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string spName, T parameters, string connectionId = "DefaultConnection")
        {
            using IDbConnection dbConnection =
                new SqlConnection(_config.GetConnectionString(connectionId));

            await dbConnection.ExecuteAsync(spName, parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
