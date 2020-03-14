using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Models;

namespace TodoWebAPI
{
    public class ContextService
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;
        public ContextService(ToDoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
    }
}
