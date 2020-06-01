using System;
using System.Collections.Generic;

namespace Todo.Domain
{
    public class TodoListAuthorizationValidator
    {
        private List<string> Contributors { get; set; }
        private string UserEmail;
        public TodoListAuthorizationValidator(List<string> contributors, string userEmail)
        {
            Contributors = contributors;
            UserEmail = userEmail;
        }

        public bool IsUserAuthorized()
        {
            return Contributors.Exists(c => c == UserEmail);
        }
    }
}