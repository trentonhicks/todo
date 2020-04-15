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
    public class RenameTodoListUserStory : IRequestHandler<UpdateListModel, TodoList>
    {
        private readonly ITodoListRepository _repository;

        public RenameTodoListUserStory(ITodoListRepository repository)
        {
            _repository = repository;
        }
        public async Task<TodoList> Handle(UpdateListModel request, CancellationToken cancellationToken)
        {
            var todoList = await _repository.FindTodoListIdByIdAsync(request.ListId);

            todoList.ListTitle = request.ListTitle;

            await _repository.SaveChangesAsync();

            return todoList;
        }
    }
}
