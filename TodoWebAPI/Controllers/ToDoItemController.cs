using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Data;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
namespace TodoWebAPI.Controllers
{
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;
        private EFTodoItemRepository _todo;
        //private IToDoItemRepository _todoMemory = new InMemoryToDoItemRepository();


        public ToDoItemController(ToDoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _todo = new EFTodoItemRepository(_context);

        }


        [HttpPost("accounts/{accountId}/lists/{listId}/todos")]
        public async Task<IActionResult> CreateTodo(int accountId, int listId, [FromBody] CreateToDoModel todos)
        {
            var todo = new TodoItemModel()
            {
                ToDoName = todos.ToDoName,
                ParentId = todos.ParentId,
                Notes = todos.Notes,
                Completed = todos.Completed,
                ListId = listId
            };
            var toDoItem = await _todo.CreateToDoAsync(todo);

            return Ok(toDoItem);
        }

        //var list = _context.Lists.Find(listId);

        //todos.ListId = listId; 

        //if (list != null)
        //{
        //    if (list.AccountId != accountId)
        //    {
        //        return BadRequest("List belongs to another account");
        //    }
        //    if (todos.ParentId != null)
        //    {
        //        return BadRequest();
        //    }
        //    _context.ToDos.Add(todos);
        //    _context.SaveChanges();
        //    return Ok();

        //return NotFound("List doesn't exist.");

        [HttpPut("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> EditTodo(int accountId, int todoId, [FromBody] ToDos todo)
        {

            var foo = await _todo.UpdateToDoAsync(todoId, todo);

            return Ok(foo);


            //var todo = _context.ToDos.Find(todoId);
            //if (_contextService.AccountExists(accountId))
            //{
            //    if (_contextService.ToDoExists(todoId))
            //    {
            //        var toDoName = todo.ToDoName;
            //        var toDoNote = todo.Notes;
            //        var toDoState = todo.Completed;
            //        if (toDoName != null)
            //        {
            //            _context.ToDos.Update(todo);
            //            _context.SaveChanges();
            //            return Ok();
            //        }
            //    }

            //    //logic to update the title
            //    //logic to update the note
            //    //logic to update the state
            //}
            //return NotFound();
        }

        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> DeleteTodo(int accountId, int todoId)
        {
            await _todo.DeleteToDoAsync(todoId);

            return Ok();

            //var todo = _context.ToDos.Find(todoId);
            //if (_contextService.AccountExists(accountId))
            //{
            //    if (todo != null)
            //    {
            //        _context.ToDos.Remove(todo);
            //        _context.SaveChanges();
            //        return Ok("ToDo list removed.");
            //    }
            //    else
            //    {
            //        return NotFound("ToDo is already empty");
            //    }
            //}
            //return NotFound("Account doesn't exist.");
        }
    }
}
