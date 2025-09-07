using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Common
{
    public static class Guard
    {
        public static void AgainstNullOrWhiteSpace(string? value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{paramName} no puede ser nulo o vacío.", paramName);
        }
        public static void AgainstNegative(decimal value, string paramName)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(paramName, "No puede ser negativo.");
        }
    }
}
