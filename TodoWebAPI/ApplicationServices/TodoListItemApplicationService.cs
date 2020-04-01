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
        private readonly IMediator _mediator;

        public TodoListItemApplicationService(ITodoListRepository listRepository, ITodoListItemRepository todoListItemRepository, IMediator mediator)
        {
            _listRepository = listRepository;
            _listItemRepository = todoListItemRepository;
            _mediator = mediator;
        }

        public async Task<TodoListItem> CreateTodoListItemAsync(int listId, int? parentId, int accountId, string todoName, string notes)
        {
            var doesListExist = await _listRepository.FindTodoListIdByIdAsync(listId);

            if (doesListExist == null)
                return null;

            var todoItem = new TodoListItem()
            {
                ListId = listId,
                ParentId = parentId,
                ToDoName = todoName,
                Notes = notes,
                AccountId = accountId
            };

            await _listItemRepository.AddTodoListItemAsync(todoItem);
            return todoItem;
        }

        public async Task UpdateTodoListItemAsync(int todoListItemId, string notes, string todoName)
        {
            var todoListItem = await _listItemRepository.FindToDoListItemByIdAsync(todoListItemId);

            todoListItem.Notes = notes;
            todoListItem.ToDoName = todoName;

            await _mediator.Publish(new TodoListItemUpdated{Item = todoListItem});
        }

        public async Task DeleteTodoListItem(int todoListItemId)
        {
            await _listItemRepository.RemoveTodoListItemAsync(todoListItemId);
        }

        public async Task MarkTodoListItemAsCompletedAsync(int todoListItemId, bool state)
        {
            var item = await _listItemRepository.FindToDoListItemByIdAsync(todoListItemId);

            if(state == true)
            {
                item.SetCompleted();
            }
            else if(state == false)
            {
                item.SetNotCompleted();
            }

            foreach (var domainEvent in item.DomainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
