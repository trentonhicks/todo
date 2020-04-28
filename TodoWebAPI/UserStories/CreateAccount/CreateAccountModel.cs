using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace TodoWebAPI.Models
{
    public class CreateAccountModel : IRequest<Account>
    {
        public string FullName { get; set; }
        [Required(ErrorMessage ="UserName Required")]
        public string UserName { get; set; }
        public string Picture { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
    }
}
