using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Todo.Infrastructure;

namespace TodoWebAPI.Models
{
    public class CreateAccountModel : IRequest<Account>
    {
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
    }
}
