using MediatR;

namespace Todo.Domain.Services
{
    public class TodoListItemUpdated : INotification
    {
        public TodoListItem Item { get; set; }
    }
}