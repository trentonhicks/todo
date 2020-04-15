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

        public async Task UpdateTodoListItemAsync(int todoListItemId, string notes, string todoName, DateTime? dueDate)
        {
            var todoListItem = await _listItemRepository.FindToDoListItemByIdAsync(todoListItemId);

            todoListItem.Notes = notes;
            todoListItem.Name = todoName;
            todoListItem.DueDate = dueDate;
        }

        public async Task MarkTodoListItemAsCompletedAsync(int listItemId)
        {
            var subItems = await _subItemRepository.FindAllSubItemsByListItemIdAsync(listItemId);
            var listItem = await _listItemRepository.FindToDoListItemByIdAsync(listItemId);

            listItem.SetCompleted(subItems);

            await _listItemRepository.SaveChangesAsync();
        }

        public async Task TrashItemAsync(int listItemId)
        {
            var item = await _listItemRepository.FindToDoListItemByIdAsync(listItemId);

            item.MoveToTrash();

            await _listItemRepository.SaveChangesAsync();
        }
    }
}
