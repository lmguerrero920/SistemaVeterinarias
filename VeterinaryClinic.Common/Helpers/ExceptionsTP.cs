using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEARNING.Common.Helpers
{
    public class ExceptionsTP
    {
         
        public static void ManagerException()
        {
            try
            {
                throw new AccessViolationException();

            }
            catch (Exception)
            {
                  Problem("Elemento Duplicado", null,501);
               // throw new ClassNotFoundException("***El Nombre se encuentra duplicado.***");
            }
        }

        private static void Problem(object twoRows, object p, int v)
        {
            twoRows = "Duplicado";
            p = null;
            v = 501;
        }
    }
}
