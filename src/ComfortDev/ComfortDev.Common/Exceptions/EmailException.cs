using System;
using System.Collections.Generic;
using System.Text;

namespace ComfortDev.Common.Exceptions
{
    public class EmailException: ApplicationException
    {
        public EmailException(string message): base(message) { }
    }
}
