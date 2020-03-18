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
        public ListPresentation(Lists list)
        {
            ListId = list.Id;
            ListTitle = list.ListTitle;

            Todos = new List<TodoPresentation>();

            foreach(var todo in list.ToDos)
            {
                Todos.Add(new TodoPresentation(todo));
            }
        }
        public int ListId { get; set; }
        public string ListTitle { get; set; }
        public List<TodoPresentation> Todos { get; set; }
    }
}
