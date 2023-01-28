using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEARNING.Common.Helpers
{
    [Serializable]
    public class ClassNotFoundException : Exception
    {
        public ClassNotFoundException() { }

        public ClassNotFoundException(string message)
            : base(message) { }

        public ClassNotFoundException(string message, Exception inner)
            : base(message, inner) { }

    }
     
}
