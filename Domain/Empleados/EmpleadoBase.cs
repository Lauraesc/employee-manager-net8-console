using EmployeeManager.Domain.Common;
using EmployeeManager.Domain.Evaluaciones;
using EmployeeManager.Domain.Horarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Empleados
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(EmpleadoPlanta), typeDiscriminator: "planta")]
    [JsonDerivedType(typeof(EmpleadoTemporal), typeDiscriminator: "temporal")]
    public abstract class EmpleadoBase
    {
        // Encapsulamiento: se protege el estado interno y se exponen sólo getters/operaciones controladas.
        private readonly List<Horario> _horarios = new();
        private readonly List<Evaluacion> _evaluaciones = new();


        public Guid Id { get; init; } = Guid.NewGuid();
        public string Documento { get; private set; }
        public string Nombres { get; private set; }
        public string Apellidos { get; private set; }


        protected EmpleadoBase(string documento, string nombres, string apellidos)
        {
            Guard.AgainstNullOrWhiteSpace(documento, nameof(documento));
            Guard.AgainstNullOrWhiteSpace(nombres, nameof(nombres));
            Guard.AgainstNullOrWhiteSpace(apellidos, nameof(apellidos));
            Documento = documento.Trim();
            Nombres = nombres.Trim();
            Apellidos = apellidos.Trim();
        }


        public IReadOnlyList<Horario> Horarios => _horarios.AsReadOnly();
        public IReadOnlyList<Evaluacion> Evaluaciones => _evaluaciones.AsReadOnly();


        public Result AgregarHorario(Horario nuevo)
        {
            // Encapsulamiento + Regla de negocio RF-04: evitar choques.
            if (_horarios.Any(h => h.SeSuperponeCon(nuevo)))
                return Result.Fail("Conflicto: el horario se superpone con uno existente.");
            _horarios.Add(nuevo);
            return Result.Ok();
        }


        public void RegistrarEvaluacion(Evaluacion e)
        {
            _evaluaciones.Add(e);
        }


        public void Renombrar(string nombres, string apellidos)
        {
            Guard.AgainstNullOrWhiteSpace(nombres, nameof(nombres));
            Guard.AgainstNullOrWhiteSpace(apellidos, nameof(apellidos));
        }

        public abstract decimal CalcularSalario(MesAnio periodo);

        public abstract string TipoContrato { get; }
    }
}
