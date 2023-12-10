using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Exceptions
{
    public class CustomUnauthorizedAccessException : UnauthorizedAccessException
    {
        public CustomUnauthorizedAccessException(string message) : base(message)
        {
         
            HResult = 401; 
        }
    }
}
