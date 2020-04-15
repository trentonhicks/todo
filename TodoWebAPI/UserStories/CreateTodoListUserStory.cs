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
    public class CreateTodoListUserStory : IRequestHandler<CreateListModel, TodoList>
    {
        private readonly ITodoListRepository _repository;

        public CreateTodoListUserStory(ITodoListRepository repository)
        {
            _repository = repository;
        }
        public async Task<TodoList> Handle(CreateListModel request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(request.ListTitle))
                return null;

            var todoList = new TodoList(request.AccountId, request.ListTitle);

            await _repository.AddTodoListAsync(todoList);

            await _repository.SaveChangesAsync();

            return todoList;
        }
    }
}
