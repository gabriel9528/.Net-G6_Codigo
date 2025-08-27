using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.DataContext
{
    public class DapperConnectionHelper : IDapperConnectionHelper
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString { get; set; }
        private string ProviderName { get; set; }

        public DapperConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            ProviderName = "System.Data.SqlClient";
        }
        public IDbConnection GetDapperConnectionHelper()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
