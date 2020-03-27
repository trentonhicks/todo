using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Presentation
{
    public class ListPresentation
    {
        public ListPresentation(TodoListModel list, List<TodoListItem> todos)
        {
            TodoList = list;
            TodoItemPreview = todos;
        }
        
        public TodoListModel TodoList { get; set; }
        public List<TodoListItem> TodoItemPreview { get; set; }
    }
}
