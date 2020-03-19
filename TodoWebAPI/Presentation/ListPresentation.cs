using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Presentation
{
    public class ListPresentation
    {
        public ListPresentation(TodoListModel list, List<ToDos> todos)
        {
            TodoList = list;
            TodoItemPreview = todos;
        }
        
        public TodoListModel TodoList { get; set; }
        public List<ToDos> TodoItemPreview { get; set; }
    }
}
