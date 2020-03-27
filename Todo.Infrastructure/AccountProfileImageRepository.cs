using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI
{
    public class AccountProfileImageRepository : IAccountProfileImageRepository
    {
        private readonly string _connectionString;

        public AccountProfileImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public byte[] ConvertStringToByteArray(string image)
        {
            byte[] imageBytes = Convert.FromBase64String(image);
            return imageBytes;
        }

        public async Task StoreImageProfileAsync(int accountId, string profileImage)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"Update Accounts SET Picture = @pic WHERE ID = @id";

                    var byteArray = ConvertStringToByteArray(profileImage);

                    command.Parameters.AddWithValue(@"pic", byteArray);
                    command.Parameters.AddWithValue(@"id", accountId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
