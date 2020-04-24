﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.DomainEvents
{
    public class SubItemMovedToTrash : INotification
    {
        public SubItem SubItem { get; set; }
        public int? ItemId { get; set; }
    }
}
