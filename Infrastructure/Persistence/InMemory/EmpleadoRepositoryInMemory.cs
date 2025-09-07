using EmployeeManager.Domain.Empleados;
using EmployeeManager.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Persistence.InMemory
{
    public sealed class EmpleadoRepositoryInMemory : IEmpleadoRepository
    {
        private readonly Dictionary<Guid, EmpleadoBase> _store = new();


        public Task AgregarAsync(EmpleadoBase empleado)
        {
            _store[empleado.Id] = empleado;
            return Task.CompletedTask;
        }


        public Task<bool> EliminarAsync(Guid id)
        => Task.FromResult(_store.Remove(id));


        public Task<List<EmpleadoBase>> ListarAsync()
        => Task.FromResult(_store.Values.ToList());


        public Task<EmpleadoBase?> ObtenerPorIdAsync(Guid id)
        => Task.FromResult(_store.TryGetValue(id, out var e) ? e : null);


        public Task<bool> ActualizarAsync(EmpleadoBase empleado)
        {
            if (!_store.ContainsKey(empleado.Id)) return Task.FromResult(false);
            _store[empleado.Id] = empleado;
            return Task.FromResult(true);
        }
    }
}
