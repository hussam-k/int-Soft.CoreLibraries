using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intSoft.MVC.Core.Exceptions
{
    public class EmailNotConfirmedException : Exception
    {
        public EmailNotConfirmedException()
        {
            
        }

        public EmailNotConfirmedException(string message) : base(message)
        {
            
        }
    }
}
