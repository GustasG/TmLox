using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmLox.Errors
{
    public class ValueError: LoxError
    {
        public ValueError()
        {

        }

        public ValueError(string message)
            : base(message)
        {
        }
    }
}
