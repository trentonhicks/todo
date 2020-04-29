﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.EditSubItem
{
    public class EditSubItem : IRequest
    {
        public int AccountId { get; set; }
        public int SubItemId { get; set; }
        [Required]
        [StringLength(50)]
        [FromBody]
        public string Name { get; set; }
    }
}
