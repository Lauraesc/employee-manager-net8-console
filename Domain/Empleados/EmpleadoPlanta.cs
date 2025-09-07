using EmployeeManager.Domain.Horarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Empleados
{
    public sealed class EmpleadoPlanta : EmpleadoBase
    {
        public decimal SalarioBaseMensual { get; private set; }
        public decimal BonoFijo { get; private set; }


        public EmpleadoPlanta(string documento, string nombres, string apellidos,
        decimal salarioBaseMensual, decimal bonoFijo = 0m)
        : base(documento, nombres, apellidos)
        {
            if (salarioBaseMensual <= 0) throw new ArgumentOutOfRangeException(nameof(salarioBaseMensual));
            if (bonoFijo < 0) throw new ArgumentOutOfRangeException(nameof(bonoFijo));
            SalarioBaseMensual = salarioBaseMensual;
            BonoFijo = bonoFijo;
        }


        public override decimal CalcularSalario(MesAnio _)
        => SalarioBaseMensual + BonoFijo; 


        public override string TipoContrato => "Planta";
    }
}
