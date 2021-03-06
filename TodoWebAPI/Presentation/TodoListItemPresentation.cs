﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Presentation
{
    public class TodoListItemPresentation
    {
        public TodoListItemPresentation(TodoListItem todo)
        {
            Id = todo.Id;
            Notes = todo.Notes;
            Completed = todo.Completed;
            Name = todo.Name;
        }
        public Guid Id { get; set; }
        public string Notes { get; set; }
        public bool Completed { get; set; }
        public string Name { get; set; }
    }
}
