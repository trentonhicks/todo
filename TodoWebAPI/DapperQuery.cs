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
        private readonly string _connectionString;

        public DapperQuery(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings")["Development"];
        }
        public async Task<AccountPresentation> GetAccountAsync(Guid accountId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<AccountPresentation>("SELECT * From Accounts Where ID = @accountId", new { accountId = accountId });

                return result.FirstOrDefault();
            }
        }

        public async Task<List<TodoListItemModel>> GetAllTodoItemAsync(Guid accountId, Guid listId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<TodoListItemModel>("SELECT * From TodoListItems Where AccountID = @accountId AND ListID = @listId", new { accountId = accountId, listId = listId });

                return result.ToList();
            }
        }

        public async Task<TodoListModel> GetListAsync(Guid accountId, Guid listId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<TodoListModel>("SELECT * From TodoLists Where AccountID = @accountId AND ID = @listId", new { accountId = accountId, listId = listId });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<TodoListModel>> GetListsAsync(Guid accountId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<TodoListModel>("SELECT * From TodoLists Where AccountID = @accountId", new { accountId = accountId });
                return result.ToList();
            }
        }

        public async Task<TodoListLayoutPresentation> GetTodoListLayoutAsync(Guid listId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<TodoListLayoutPresentation>("SELECT * FROM TodoListLayouts WHERE ListId = @listId", new { listId = listId });
                return result.FirstOrDefault();
            }
        }


        public async Task<List<SubItemModel>> GetSubItems(Guid listItemId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<SubItemModel>("SELECT * FROM SubItems WHERE ListItemID = @listItemId", new { listItemId = listItemId });
                
                return result.ToList();
            }
        }

        public async Task<List<TodoListItem>> GetDueDatesFromListItemsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<TodoListItem>("SELECT * FROM TodoListItems WHERE DueDate IS NOT NULL");
                return result.ToList();
            }
        }
    }
}
