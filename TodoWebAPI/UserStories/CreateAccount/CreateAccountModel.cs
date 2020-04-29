using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Todo.Infrastructure;

namespace TodoWebAPI.Models
{
    public class CreateAccountModel : IRequest<Account>
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
