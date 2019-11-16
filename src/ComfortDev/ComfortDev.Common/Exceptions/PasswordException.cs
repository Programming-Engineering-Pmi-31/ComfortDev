using System;
using System.Collections.Generic;
using System.Text;

namespace ComfortDev.Common.Exceptions
{
    public class PasswordException: ApplicationException
    {
        public PasswordException(string message): base(message) { }
    }
}
