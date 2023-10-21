using Dapper;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace BaltaIoChallenge.WebApi.Data.v1
{
    public static class Database
    {
        public static async Task SeedDbInitialValues(string connectionString)
        {
            var rolesToCheck = new List<string> { "admin", "user" };

            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                foreach (var role in rolesToCheck)
                {
                    var roleExists = await RoleExistsAsync(connection, transaction, role);

                    if (!roleExists)
                        await InsertRoleAsync(connection, transaction, role);
                }

                var dataExists = DataExists(connection);

                if (!dataExists)
                {
                    var insertDataCmd = GetInserts();
                    await connection.ExecuteAsync(insertDataCmd, transaction);

                    var updateDataCmd = @"UPDATE `Ibge` SET `State`= 'RR' WHERE `id` IN 
                    (
                        '1400050',
                        '1400027',
                        '1400100',
                        '1400159',
                        '1400175',
                        '1400209',
                        '1400233',
                        '1400282',
                        '1400308',
                        '1400407',
                        '1400456',
                        '1400472',
                        '1400506',
                        '1400605',
                        '1400704'
                    )";

                    await connection.ExecuteAsync(updateDataCmd, transaction);
                    await transaction.CommitAsync();
                }
            }
            catch (MySqlException ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Something went wrong while trying to seed the database: {ex.Message}");
            }

            async Task<bool> RoleExistsAsync(MySqlConnection connection, DbTransaction transaction, string role)
            {
                var query = "SELECT 1 FROM Roles WHERE Name = @Role LIMIT 1";
                return await connection.QueryFirstOrDefaultAsync<bool>(query, new { Role = role }, transaction);
            }

            async Task InsertRoleAsync(MySqlConnection connection, DbTransaction transaction, string role)
            {
                var query = "INSERT INTO `Roles`(`Name`) VALUES (@Role)";
                await connection.ExecuteAsync(query, new { Role = role }, transaction);
            }

            bool DataExists(MySqlConnection connection)
            {
                var query = "SELECT 1 FROM Ibge LIMIT 1";
                return connection.QueryFirstOrDefault<bool>(query);
            }
        }

        private static string GetInserts()
            => @"INSERT INTO `Ibge` (`id`, `state`, `city`) VALUES ('1100015', 'RO', 'Alta Floresta D''''Oeste');
INSERT INTO `Ibge` (`id`, `state`, `city`) VALUES ('5300108', 'DF', 'Brasília');
";
    }
}
