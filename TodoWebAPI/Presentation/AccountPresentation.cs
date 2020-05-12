using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Presentation
{
    public class AccountPresentation
    {
        public Guid Id { get; set; }
        public string PictureUrl { get; set;}
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
