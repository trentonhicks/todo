using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Presentation
{
    public class TodoListLayoutPresentation
    {   
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Layout { get; set; }
    }
}
