using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.EditItem
{
    public class EditItemUserStory : IRequestHandler<EditItem>
    {
        private readonly ITodoListItemRepository _todoListItemRepository;

        public EditItemUserStory(ITodoListItemRepository todoListItemRepository)
        {
            _todoListItemRepository = todoListItemRepository;
        }
        public async Task<Unit> Handle(EditItem request, CancellationToken cancellationToken)
        {
            var todoListItem = await _todoListItemRepository.FindToDoListItemByIdAsync(request.Id);

            todoListItem.Name = request.ToDoName;
            todoListItem.Notes = request.Notes;
            todoListItem.DueDate = request.DueDate;

            await _todoListItemRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
