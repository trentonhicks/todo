using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Presentation
{
    public class TodoPresentation
    {
        public TodoPresentation(TodoListItem todo)
        {
            Id = todo.Id;
            ParentId = todo.ParentId;
            Notes = todo.Notes;
            Completed = todo.Completed;
            ToDoName = todo.ToDoName;
        }
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Notes { get; set; }
        public bool Completed { get; set; }
        public string ToDoName { get; set; }
    }
}
