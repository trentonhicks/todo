﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace TodoWebAPI.Models
{
    public class TodoListModel
    {
        public Guid Id { get; set; }
        public string ListTitle { get; set; }
        public bool Completed { get; set; }
        public List<string> Contributors { get; set; }
        public byte Role { get; set; }
    }
}
