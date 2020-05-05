using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todo.Domain
{
    public class SubItemLayout : Entity
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public List<Guid> Layout { get; set; } = new List<Guid>();
        public void UpdateLayout(Guid subItemId, int position)
        {
            Layout.Remove(subItemId);
            Layout.Insert(position, subItemId);

            if (Layout.Distinct().Count() != Layout.Count)
            {
                throw new ArgumentException("Layout contains duplicate Id.");
            }
        }
    }
}
