using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Models;
using System;
using SixLabors.ImageSharp;
using System.IO;
using SixLabors.ImageSharp.Formats;
using System.Configuration;
using System.Data.SqlClient;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace TodoWebAPI
{
    public class ImageHandler
    {
        private readonly string _connectionString;
        public ImageHandler(string connectionString)
        {
            _connectionString = connectionString;
        }
        public byte[] ConvertStringToByteArray(string image)
        {
            byte[] imageBytes = Convert.FromBase64String(image);
            return imageBytes;
        }

        public void StoreImageProfile(CreateAccountModel account)
        {
            var id = account.Id;
            var s = account.Picture;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"Update Accounts SET Picture = @pic WHERE ID = @id";

                    var pic = ConvertStringToByteArray(s);

                    command.Parameters.AddWithValue(@"pic", pic);
                    command.Parameters.AddWithValue(@"id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
