using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Models;

namespace TodoWebAPI.Presentation
{
    public class ListPresentation
    {
        public ListPresentation(Lists list)
        {
            ListId = list.Id;
            ListTitle = list.ListTitle;
        }
        public int ListId { get; set; }
        public string ListTitle { get; set; }
    }
}
