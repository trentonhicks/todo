using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using TodoWebAPI.Models;

namespace TodoWebAPI.UserStories
{
    public class DeleteTodoListUserStory : IRequestHandler<DeleteTodoModel>
    {
        private readonly ITodoListItemRepository _todoListItem;
        private readonly ITodoListRepository _listRepository;

        public DeleteTodoListUserStory(ITodoListItemRepository todoListItem, ITodoListRepository listRepository)
        {
            _todoListItem = todoListItem;
            _listRepository = listRepository;
        }
        public async Task<Unit> Handle(DeleteTodoModel request, CancellationToken cancellationToken)
        {
            await _todoListItem.RemoveAllTodoListItemsFromAccountAsync(request.ListId);

            await _listRepository.RemoveTodoListAsync(request.ListId);

            await _listRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
