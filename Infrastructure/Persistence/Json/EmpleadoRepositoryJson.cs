using EmployeeManager.Domain.Empleados;
using EmployeeManager.Domain.Repositorios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Persistence.Json
{

    public sealed class EmpleadoRepositoryJson(string filePath) : IEmpleadoRepository
    {
        private readonly string _file = filePath;
        private readonly Dictionary<Guid, EmpleadoBase> _cache = new();
        private bool _loaded;

        private async Task EnsureLoadedAsync()
        {
            if (_loaded) return;
            if (File.Exists(_file))
            {
                var json = await File.ReadAllTextAsync(_file);
                var opts = JsonPolymorphismOptions.Create();
                var lista = JsonSerializer.Deserialize<List<EmpleadoBase>>(json, opts) ?? new();
                foreach (var e in lista) _cache[e.Id] = e;
            }
            _loaded = true;
        }

        private async Task PersistAsync()
        {
            var opts = JsonPolymorphismOptions.Create();
            var json = JsonSerializer.Serialize(_cache.Values.ToList(), opts);
            await File.WriteAllTextAsync(_file, json);
        }

        public async Task AgregarAsync(EmpleadoBase empleado)
        {
            await EnsureLoadedAsync();
            _cache[empleado.Id] = empleado;
            await PersistAsync();
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            await EnsureLoadedAsync();
            var ok = _cache.Remove(id);
            if (ok) await PersistAsync();
            return ok;
        }


        public async Task<List<EmpleadoBase>> ListarAsync()
        {
            await EnsureLoadedAsync();
            return _cache.Values.ToList();
        }


        public async Task<EmpleadoBase?> ObtenerPorIdAsync(Guid id)
        {
            await EnsureLoadedAsync();
            return _cache.TryGetValue(id, out var e) ? e : null;
        }


        public async Task<bool> ActualizarAsync(EmpleadoBase empleado)
        {
            await EnsureLoadedAsync();
            if (!_cache.ContainsKey(empleado.Id)) return false;
            _cache[empleado.Id] = empleado;
            await PersistAsync();
            return true;
        }

    }

}
