using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI
{
    public static class CheckBoxstate
    {
        public static bool Finished = true;
        public static bool Unfinished = false;

        public static string ConvertToString(bool state)
        {
            string stringValue;

            switch (state)
            {
                default:
                    stringValue = "Unfinished";
                    break;
                case true:
                    stringValue = "Finished";
                    break;
                case false:
                    stringValue = "Unfinished";
                    break;
            }
            return stringValue;
        }
    }
}
