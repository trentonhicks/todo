using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace Todo.Domain.Services
{
    public class TodoListService
    {
        private readonly ITodoListRepository _listRepository;
        private readonly IAccountRepository _accountRepository;

        public TodoListService(ITodoListRepository listRepository, IAccountRepository accountRepository)
        {
            _listRepository = listRepository;
            _accountRepository = accountRepository;
        }

        public async Task<TodoList> CreateTodoListAsync(int accountId, string listTitle)
        {
            var doesAccountExist = await _accountRepository.DoesAccountWithAccountIdExistAsync(accountId);

            if (!doesAccountExist)
                return null;

            var todoList = new TodoList()
            {
                AccountId = accountId,
                ListTitle = listTitle
            };
            await _listRepository.AddTodoListAsync(todoList);

            return todoList;
        }

        public async Task RenameTodoListAsync(int listId, string listTitle)
        {
            var todoList = await _listRepository.FindTodoListIdByIdAsync(listId);

            todoList.ListTitle = listTitle;

            await _listRepository.SaveChangesAsync();
        }
    }
}
