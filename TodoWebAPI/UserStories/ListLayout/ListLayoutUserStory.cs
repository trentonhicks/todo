using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;
using TodoWebAPI.Models;

namespace TodoWebAPI.UserStories.ListLayout
{
    public class ListLayoutUserStory : IRequestHandler<ListLayout>
    {
        private readonly ITodoListLayoutRepository _todoListLayout;
        public ListLayoutUserStory(ITodoListLayoutRepository todoListLayout)
        {
            _todoListLayout = todoListLayout;
        }
        public async Task<Unit> Handle(ListLayout request, CancellationToken cancellationToken)
        {
            var todoListLayout = await _todoListLayout.FindLayoutByListIdAsync(request.ListId);

            todoListLayout.UpdateLayout(request.ItemId, request.Position);

            _todoListLayout.Update(todoListLayout);
            await _todoListLayout.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
