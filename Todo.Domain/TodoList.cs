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

        public TodoList(int accountId, string title)
        {
            AccountId = accountId;
            ListTitle = title;

            DomainEvents.Add(new TodoListCreated { List = this });
        }

        public int Id { get; set; }
        public string ListTitle { get; set; }
        public int AccountId { get; private set; }
        public bool Completed { get; private set; }

        public TodoListItem CreateListItem(string name, string notes, DateTime? dueDate)
        {
            var todoItem = new TodoListItem()
            {
                ListId = Id,
                Name = name,
                Notes = notes,
                AccountId = AccountId,
                DueDate = dueDate
            };

            DomainEvents.Add(new TodoListItemCreated { Item = todoItem, List = this });

            return todoItem;
        }
        public void SetCompleted(List<TodoListItem> items)
        {
            if (!items.All(item => item.ListId == Id))
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
