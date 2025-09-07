using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Evaluaciones
{
    public sealed class Evaluacion
    {
        public DateTime Fecha { get; }
        public int Puntuacion { get; } // 1..5
        public string Comentario { get; }


        public Evaluacion(DateTime fecha, int puntuacion, string comentario)
        {
            if (puntuacion is < 1 or > 5) throw new ArgumentOutOfRangeException(nameof(puntuacion));
            Fecha = fecha;
            Puntuacion = puntuacion;
            Comentario = comentario ?? string.Empty;
        }
    }
}
