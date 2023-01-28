using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Common.Class
{
    public class ClassStrings
    {

        private string spSelectppointment = "SpSelectppointment";
        private string spInsertAppointment = "SpInsertAppointment";
        private string spUpdateAppointment = "SpUpdateAppointment";
        private string spDeleteAppointment = "SpDeleteAppointment";

        private string twoRows = " El registro se encuentra duplicado";
        public string TwoRows { get => twoRows; set => twoRows = value; }
        public string SpSelectppointment { get => spSelectppointment; set => spSelectppointment = value; }
        public string SpInsertAppointment { get => spInsertAppointment; set => spInsertAppointment = value; }
        public string SpUpdateAppointment { get => spUpdateAppointment; set => spUpdateAppointment = value; }
        public string SpDeleteAppointment { get => spDeleteAppointment; set => spDeleteAppointment = value; }
    }
}
