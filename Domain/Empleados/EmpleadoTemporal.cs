using EmployeeManager.Domain.Horarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Empleados
{
    public sealed class EmpleadoTemporal : EmpleadoBase
    {
        public decimal TarifaHora { get; private set; }


        public EmpleadoTemporal(string documento, string nombres, string apellidos,
        decimal tarifaHora) : base(documento, nombres, apellidos)
        {
            if (tarifaHora <= 0) throw new ArgumentOutOfRangeException(nameof(tarifaHora));
            TarifaHora = tarifaHora;
        }


        public override decimal CalcularSalario(MesAnio periodo)
        {
            // Calcula según horas trabajadas en el mes (polimorfismo por override)
            var horas = Horarios
            .Where(h => h.Inicio.Year == periodo.Anio && h.Inicio.Month == periodo.Mes)
            .Sum(h => h.Duracion.TotalHours);
            return (decimal)horas * TarifaHora;
        }


        public override string TipoContrato => "Temporal";
    }
}
