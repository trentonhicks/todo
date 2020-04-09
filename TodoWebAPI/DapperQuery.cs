using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Domain;
using Todo.Domain.Repositories;
using System.Threading;
using System.Data.SqlClient;
using TodoWebAPI.Presentation;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure;
using Todo.WebAPI.ApplicationServices;
using TodoWebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoWebAPI
{
    public class DapperQuery
    {
        private readonly IConfiguration _config;

        public DapperQuery(IConfiguration config)
        {
            _config = config;
        }
        public async Task<AccountPresentation> GetAccountAsync(int accountId)
        {
            using (var connection = new SqlConnection(_config.GetSection("ConnectionStrings")["Development"]))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<AccountPresentation>("SELECT * From Accounts Where ID = @accountId", new { accountId = accountId });

                return result.FirstOrDefault();
            }
        }

        public async Task<List<TodoListItemModel>> GetAllTodoItemAsync(int accountId, int listId)
        {
            using (var connection = new SqlConnection(_config.GetSection("ConnectionStrings")["Development"]))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<TodoListItemModel>("SELECT * From TodoListItems Where AccountID = @accountId AND ListID = @listId", new { accountId = accountId, listId = listId });

                return result.ToList();
            }
        }

        public async Task<TodoListModel> GetListAsync(int accountId, int listId)
        {
            using (var connection = new SqlConnection(_config.GetSection("ConnectionStrings")["Development"]))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<TodoListModel>("SELECT * From TodoLists Where AccountID = @accountId AND ID = @listId", new { accountId = accountId, listId = listId });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<TodoListModel>> GetListsAsync(int accountId)
        {
            using (var connection = new SqlConnection(_config.GetSection("ConnectionStrings")["Development"]))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<TodoListModel>("SELECT * From TodoLists Where AccountID = @accountId", new { accountId = accountId });
                return result.ToList();
            }
        }

        //public async Task StoreImageProfileQueryAsync(int accountId, string profileImage)
        //{
        //    using (var connection = new SqlConnection(_config.GetSection("ConnectionStrings")["Development"]))
        //    {
        //        var foo = new AccountProfileImageRepository(profileImage);

        //        await connection.OpenAsync();
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = @"Update Accounts SET Picture = @pic WHERE ID = @id";
        //            var byteArray = foo.ConvertStringToByteArray(profileImage);
        //            command.Parameters.AddWithValue(@"pic", byteArray);
        //            command.Parameters.AddWithValue(@"id", accountId);
        //            await command.ExecuteNonQueryAsync();
        //        }
        //    }
        //}
    }
}
