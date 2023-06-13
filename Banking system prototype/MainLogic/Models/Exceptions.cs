using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLogic
{
    public class Exceptions : Exception
    {
        public string ErrorMessage { get; private set; }
        public Exceptions(string ErrorMsg):base(ErrorMsg)
        {
            this.ErrorMessage = ErrorMessage;
        }

    }
}

