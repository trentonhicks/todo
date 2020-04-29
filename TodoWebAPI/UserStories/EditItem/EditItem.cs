﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.EditItem
{
    public class EditItem : IRequest
    {
        public int AccountId { get; set; }
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Notes { get; set; }
        public int ListId { get; set; }
        public bool Completed { get; set; }
        public DateTime? DueDate { get; set; }
    }
}