using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Todo.Domain
{
    public class TodoListLayout : Entity
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public List<int> Layout { get; set; } = new List<int>();
        public void UpdateLayout(int todoListItemId,  int todoListItemPosition)
        {
            Layout.Remove(todoListItemId);
            Layout.Insert(todoListItemPosition, todoListItemId);

            if (Layout.Distinct().Count() != Layout.Count)
            {
                throw new ArgumentException("Layout contains duplicate Id.");
            }
        }
    }
}
