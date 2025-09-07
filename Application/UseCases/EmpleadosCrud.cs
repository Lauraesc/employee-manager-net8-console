using EmployeeManager.Domain.Empleados;
using EmployeeManager.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.UseCases
{
    public sealed class EmpleadosCrud(IEmpleadoRepository repo, IUnidadTrabajo uow)
    {
        private readonly IEmpleadoRepository _repo = repo;
        private readonly IUnidadTrabajo _uow = uow;


        public Task<List<EmpleadoBase>> ListarAsync() => _repo.ListarAsync();


        public async Task<Guid> CrearPlantaAsync(string doc, string nom, string ape, decimal baseMensual, decimal bono)
        {
            var emp = new EmpleadoPlanta(doc, nom, ape, baseMensual, bono);
            await _repo.AgregarAsync(emp);
            await _uow.GuardarCambiosAsync();
            return emp.Id;
        }


        public async Task<Guid> CrearTemporalAsync(string doc, string nom, string ape, decimal tarifaHora)
        {
            var emp = new EmpleadoTemporal(doc, nom, ape, tarifaHora);
            await _repo.AgregarAsync(emp);
            await _uow.GuardarCambiosAsync();
            return emp.Id;
        }


        public Task<EmpleadoBase?> ObtenerAsync(Guid id) => _repo.ObtenerPorIdAsync(id);


        public async Task<bool> EliminarAsync(Guid id)
        {
            var ok = await _repo.EliminarAsync(id);
            if (ok) await _uow.GuardarCambiosAsync();
            return ok;
        }


        public async Task<bool> ActualizarAsync(EmpleadoBase emp)
        {
            var ok = await _repo.ActualizarAsync(emp);
            if (ok) await _uow.GuardarCambiosAsync();
            return ok;
        }
    }
}
