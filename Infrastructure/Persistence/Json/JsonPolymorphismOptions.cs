using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Persistence.Json
{
    public static class JsonPolymorphismOptions
    {
        public static JsonSerializerOptions Create()
        {
            var opts = new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            };
            // Los atributos en EmpleadoBase habilitan la serialización polimórfica.
            return opts;
        }
    }
}
