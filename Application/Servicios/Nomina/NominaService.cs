using EmployeeManager.Domain.Horarios;
using EmployeeManager.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Servicios.Nomina
{
    public sealed class NominaService(IEmpleadoRepository repo)
: INominaService
    {
        private readonly IEmpleadoRepository _repo = repo;


        public async Task<Dictionary<Guid, decimal>> CalcularNominaAsync(MesAnio periodo)
        {
            var lista = await _repo.ListarAsync();
            return lista.ToDictionary(e => e.Id, e => e.CalcularSalario(periodo));
        }
    }
}
