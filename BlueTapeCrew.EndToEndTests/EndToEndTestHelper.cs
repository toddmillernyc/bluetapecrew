using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.EndToEndTests
{
    public class EndToEndTestHelper
    {
        private static string _connectionString;

        public EndToEndTestHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SeedAdminRole(string userEmail)
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.ExecuteAsync(
                "INSERT INTO dbo.AspNetRoles(Id,[Name],NormalizedName,ConcurrencyStamp) VALUES(NEWID(),'Admin','ADMIN',NEWID())");
            var roleId = (await conn.QueryAsync<string>("SELECT TOP 1 Id FROM dbo.AspNetRoles WHERE [Name] = 'Admin'")).FirstOrDefault();
            var userId = (await conn.QueryAsync<string>($"SELECT TOP 1 Id FROM dbo.AspNetUsers WHERE Email = '{userEmail}'")).FirstOrDefault();
            await conn.ExecuteAsync($"INSERT INTO dbo.AspNetUserRoles(UserId,RoleId) VALUES('{userId}','{roleId}')");
        }
    }
}
