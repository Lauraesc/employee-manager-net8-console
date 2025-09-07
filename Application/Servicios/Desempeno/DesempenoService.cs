using EmployeeManager.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Servicios.Desempeno
{
    public sealed class DesempenoService(IEmpleadoRepository repo) : IDesempenoService
    {
        private readonly IEmpleadoRepository _repo = repo;


        public async Task<double> PromedioEmpleadoAsync(Guid empleadoId)
        {
            var emp = await _repo.ObtenerPorIdAsync(empleadoId);
            if (emp is null || emp.Evaluaciones.Count == 0) return 0d;
            return emp.Evaluaciones.Average(e => e.Puntuacion);
        }
    }
}
