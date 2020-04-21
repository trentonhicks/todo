using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.EditItem
{
    public class EditItem : IRequest
    {
        public int AccountId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int ListId { get; set; }
        public bool Completed { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
