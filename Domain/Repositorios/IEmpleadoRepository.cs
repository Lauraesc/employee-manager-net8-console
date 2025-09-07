using EmployeeManager.Domain.Empleados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Repositorios
{
    public interface IEmpleadoRepository
    {
        Task<List<EmpleadoBase>> ListarAsync();
        Task<EmpleadoBase?> ObtenerPorIdAsync(Guid id);
        Task AgregarAsync(EmpleadoBase empleado);
        Task<bool> EliminarAsync(Guid id);
        Task<bool> ActualizarAsync(EmpleadoBase empleado);
    }
}
