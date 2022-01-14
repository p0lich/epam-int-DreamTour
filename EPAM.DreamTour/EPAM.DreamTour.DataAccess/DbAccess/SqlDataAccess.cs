using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.DreamTour.DataAccess.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _configuration;

        public SqlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default")
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId)))
            {
                return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IDictionary<T, U>> LoadData<T, U, V>(string storedProcedure, V parameters, string connectionId = "Default")
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId)))
            {
                var collection = await connection.QueryAsync<T, U, KeyValuePair<T, U>>(storedProcedure, (s, i) => new KeyValuePair<T, U>(s, i));

                var dict = collection.ToDictionary(kv => kv.Key, kv => kv.Value);

                return dict;
            }
        }

        public async Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default")
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId)))
            {
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
