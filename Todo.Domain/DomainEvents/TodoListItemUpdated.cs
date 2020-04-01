using MediatR;

namespace Todo.Domain.DomainEvents
{
    public class TodoListItemUpdated : INotification
    {
        public TodoListItem Item { get; set; }
    }
}