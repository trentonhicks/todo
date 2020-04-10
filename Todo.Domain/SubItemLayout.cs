using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todo.Domain
{
    public class SubItemLayout : Entity
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public int ItemId { get; set; }
        public List<int> Layout { get; set; } = new List<int>();
        public void UpdateLayout(int subItemId, int position)
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
