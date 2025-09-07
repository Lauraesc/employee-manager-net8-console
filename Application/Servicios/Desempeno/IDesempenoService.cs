using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Servicios.Desempeno
{
    public interface IDesempenoService
    {
        Task<double> PromedioEmpleadoAsync(Guid empleadoId);
    }
}
