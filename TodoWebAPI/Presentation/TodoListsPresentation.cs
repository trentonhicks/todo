using System;
using System.Collections.Generic;
using Todo.Domain;
using TodoWebAPI.Models;

namespace TodoWebAPI.Presentation
{
    public class TodoListsPresentation
    {
        public List<TodoListModel> TodoLists { get; set; }
        public List<AccountContributorsPresentation> Contributors { get; set; }
        public TodoListsPresentation(List<TodoListModel> todoLists, List<AccountContributorsPresentation> contributors)
        {
            TodoLists = todoLists;
            Contributors = contributors;
        }
    }
}
