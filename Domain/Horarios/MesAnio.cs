using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Horarios
{
    public readonly record struct MesAnio(int Anio, int Mes)
    {
        public override string ToString() => $"{Anio:D4}-{Mes:D2}";
    }
}
