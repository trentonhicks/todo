using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.CreateItem
{
    public class CreateItemUserStory : IRequestHandler<CreateItem, TodoListItem>
    {
        private readonly ITodoListRepository _todoListRepository;
        private readonly ITodoListItemRepository _todoListItemRepository;

        public CreateItemUserStory(ITodoListRepository todoListRepository, ITodoListItemRepository todoListItemRepository)
        {
            _todoListRepository = todoListRepository;
            _todoListItemRepository = todoListItemRepository;
        }
        public async Task<TodoListItem> Handle(CreateItem request, CancellationToken cancellationToken)
        {
            var list = await _todoListRepository.FindTodoListIdByIdAsync(request.ListId);

            if (list == null)
                return null;

            var todoItem = list.CreateListItem(request.Name, request.Notes, request.DueDate);

            await _todoListItemRepository.AddTodoListItemAsync(todoItem);

            await _todoListItemRepository.SaveChangesAsync();

            return todoItem;
        }
    }
}
