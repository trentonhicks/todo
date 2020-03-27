using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace TodoWebAPI.InMemory
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        public InMemoryAccountRepository()
        {
            _accounts = new List<Account>();
            _accounts.Add(new Account() { Id = 1, FullName = "Parker", UserName = "parker", Picture = new byte[0], Password = "1234", Email = "plwieseler@gmail.com" });
            _list = new List<TodoList>();
            _list.Add(new TodoList() { Id = 1, AccountId = 1, ListTitle = "New List" });
            _todo = new List<TodoListItem>();
            _todo.Add(new TodoListItem() { Id = 1, Completed = false, ListId = 1, Notes = "", ToDoName = "yes" });
        }
        private List<Account> _accounts;
        private List<TodoList> _list;
        private List<TodoListItem> _todo;
        
       
        //public async Task DeleteAccountsAsync(int accountId)
        //{
        //    var getAccount = _accounts.Find(x => x.Id == accountId);
        //    var getList = _list.Find(x => x.AccountId == accountId);

        //    if (getList == null)
        //    {
        //        _accounts.Remove(getAccount);
        //    }

        //    DeleteToDo(getList);
        //    _list.Remove(getList);
        //    _accounts.Remove(getAccount);
        //}
        public Task<Account> GetAccountAsync(int accountId)
        {
            var account =  _accounts.Find(x => x.Id == accountId);
            if(account == null)
            {
                return null;
            }

            return Task.FromResult(account);
        }

        
        Task IAccountRepository.AddAccountAsync(Account account)
        {
            account.Id = 1;
            _accounts.Add(account);
            return Task.FromResult(account);
        }

        public Task<Account> FindAccountByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DoesAccountWithUserNameExistAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DoesAccountWithAccountIdExistAsync(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
