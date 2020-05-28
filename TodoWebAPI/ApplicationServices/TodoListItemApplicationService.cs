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
        private readonly ITodoListItemRepository _listItemRepository;
        private readonly ISubItemRepository _subItemRepository;

        public TodoListItemApplicationService(ITodoListItemRepository todoListItemRepository,
            ISubItemRepository subItemRepository)
        {
            _listItemRepository = todoListItemRepository;
            _subItemRepository = subItemRepository;
        }

        public async Task MarkTodoListItemAsCompletedAsync(Guid listItemId)
        {
            var subItems = await _subItemRepository.FindAllSubItemsByListItemIdAsync(listItemId);
            var listItem = await _listItemRepository.FindToDoListItemByIdAsync(listItemId);

            listItem.SetCompleted(subItems);

            await _listItemRepository.SaveChangesAsync();
        }
    }
}
