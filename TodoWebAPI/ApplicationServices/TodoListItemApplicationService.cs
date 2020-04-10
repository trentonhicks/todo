using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace Todo.WebAPI.ApplicationServices
{
    public class TodoListItemApplicationService
    {
        private readonly ITodoListRepository _listRepository;
        private readonly ITodoListItemRepository _listItemRepository;
        private readonly ISubItemRepository _subItemRepository;

        public TodoListItemApplicationService(ITodoListRepository listRepository,
            ITodoListItemRepository todoListItemRepository,
            ISubItemRepository subItemRepository)
        {
            _listRepository = listRepository;
            _listItemRepository = todoListItemRepository;
            _subItemRepository = subItemRepository;
        }

        public async Task<TodoListItem> CreateTodoListItemAsync(int listId, int accountId, string todoName, string notes, DateTime? dueDate)
        {
            var list = await _listRepository.FindTodoListIdByIdAsync(listId);

            if (list == null)
                return null;

            var todoItem = list.CreateListItem(todoName, notes, dueDate);

            await _listItemRepository.AddTodoListItemAsync(todoItem);

            await _listItemRepository.SaveChangesAsync();

            return todoItem;
        }

        public async Task UpdateTodoListItemAsync(int todoListItemId, string notes, string todoName, DateTime? dueDate)
        {
            var todoListItem = await _listItemRepository.FindToDoListItemByIdAsync(todoListItemId);

            todoListItem.Notes = notes;
            todoListItem.Name = todoName;
            todoListItem.DueDate = dueDate;
        }

        public async Task DeleteTodoListItem(int todoListItemId)
        {
            await _listItemRepository.RemoveTodoListItemAsync(todoListItemId);
        }

        public async Task MarkTodoListItemAsCompletedAsync(int todoListItemId, bool completed)
        {
            var subItemCount = await _listItemRepository.GetSubItemCountAsync(todoListItemId);

            if (subItemCount > 0)
                return;

            var item = await _listItemRepository.FindToDoListItemByIdAsync(todoListItemId);

            if(completed == true)
            {
                item.SetCompleted();
            }
            else if(completed == false)
            {
                item.SetNotCompleted();
            }

            await _listItemRepository.SaveChangesAsync();
        }

        public async Task MarkTodoListItemAsCompletedAsync(int listItemId)
        {
            var subItems = await _subItemRepository.FindAllSubItemsByListItemIdAsync(listItemId);
            var listItem = await _listItemRepository.FindToDoListItemByIdAsync(listItemId);

            listItem.SetCompleted(subItems);

            await _listItemRepository.SaveChangesAsync();
        }
    }
}
