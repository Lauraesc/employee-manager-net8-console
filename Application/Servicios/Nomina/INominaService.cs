using EmployeeManager.Domain.Horarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Servicios.Nomina
{
    public interface INominaService
    {
        Task<Dictionary<Guid, decimal>> CalcularNominaAsync(MesAnio periodo);
    }
}
