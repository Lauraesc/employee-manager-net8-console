using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Horarios
{
    public sealed class Horario
    {
        public DateTime Inicio { get; }
        public DateTime Fin { get; }


        public Horario(DateTime inicio, DateTime fin)
        {
            if (fin <= inicio) throw new ArgumentException("Fin debe ser mayor que Inicio");
            Inicio = inicio;
            Fin = fin;
        }


        public bool SeSuperponeCon(Horario otro) => Inicio < otro.Fin && otro.Inicio < Fin;
        public TimeSpan Duracion => Fin - Inicio;
    }
}
