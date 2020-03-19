using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI
{
    public static class ExceptionHandler
    {
        public static async void BoolCheck(bool torf, string customMessage = "The parent does not exist")
        {
            if (torf == false)
            {
                throw new Exception(customMessage);
            }
        }

        public static async void ThrowException(string message)
        {
            throw new Exception(message);
        }
    }
}
