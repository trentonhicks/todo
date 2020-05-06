using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Domain.DomainEvents;

namespace Todo.Domain
{
    public partial class TodoList : Entity
    {
        public TodoList()
        {

        }

        public TodoList(string title)
        {
            ListTitle = title;

            DomainEvents.Add(new TodoListCreated { List = this });
        }

        public Guid Id { get; set; }
        public string ListTitle { get; set; }
        public bool Completed { get; private set; }

        public TodoListItem CreateListItem(Guid listId, string name, string notes, DateTime? dueDate)
        {
            var todoItem = new TodoListItem()
            {
                ListId = listId,
                Name = name,
                Notes = notes,
                DueDate = dueDate
            };

            DomainEvents.Add(new TodoListItemCreated { Item = todoItem, List = this });

            return todoItem;
        }
        public void SetCompleted(List<TodoListItem> items)
        {
            if (!items.All(item => item.ListId == Id) || items.Count == 0)
                return;

            var itemsCompleted = items.All(item => item.Completed);

            if (Completed && !itemsCompleted)
            {
                Completed = false;
                DomainEvents.Add(new TodoListCompletedStateChanged { List = this});
            }
            else if(!Completed && itemsCompleted)
            {
                Completed = true;
                DomainEvents.Add(new TodoListCompletedStateChanged { List = this });
            }
        }
       
    }
}
