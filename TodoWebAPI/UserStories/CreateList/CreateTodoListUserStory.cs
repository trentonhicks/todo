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
    public class CreateTodoListUserStory : IRequestHandler<CreateList, TodoList>
    {
        private readonly ITodoListRepository _repository;

        public CreateTodoListUserStory(ITodoListRepository repository)
        {
            _repository = repository;
        }
        public async Task<TodoList> Handle(CreateList request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(request.ListTitle))
                return null;

            var todoList = new TodoList(request.AccountId, request.ListTitle);

            todoList.Id = _repository.NextId();

            await _repository.AddTodoListAsync(todoList);

            await _repository.SaveChangesAsync();

            return todoList;
        }
    }
}
