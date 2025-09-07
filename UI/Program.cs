using EmployeeManager.Application.Servicios.Desempeno;
using EmployeeManager.Application.Servicios.Nomina;
using EmployeeManager.Application.UseCases;
using EmployeeManager.Domain.Empleados;
using EmployeeManager.Domain.Evaluaciones;
using EmployeeManager.Domain.Horarios;
using EmployeeManager.Domain.Repositorios;
using EmployeeManager.Infrastructure.Persistence.InMemory;
using EmployeeManager.Infrastructure.UnitOfWork;

Console.OutputEncoding = System.Text.Encoding.UTF8;


IEmpleadoRepository repo = new EmpleadoRepositoryInMemory();
var uow = new UnidadTrabajoSimple();


var crud = new EmpleadosCrud(repo, uow);
INominaService nominaService = new NominaService(repo);
IDesempenoService desempenoService = new DesempenoService(repo);


while (true)
{
    Console.WriteLine("\n=== GESTOR DE EMPLEADOS (.NET 8, POO) ===");
    Console.WriteLine("1) Listar empleados");
    Console.WriteLine("2) Crear empleado PLANTA");
    Console.WriteLine("3) Crear empleado TEMPORAL");
    Console.WriteLine("4) Renombrar empleado");
    Console.WriteLine("5) Eliminar empleado");
    Console.WriteLine("6) Asignar horario (con validación de choques)");
    Console.WriteLine("7) Registrar evaluación de desempeño");
    Console.WriteLine("8) Reporte de nómina mensual");
    Console.WriteLine("9) Reporte de promedio de desempeño");
    Console.WriteLine("0) Salir");
    Console.Write("Seleccione: ");


    var op = Console.ReadLine();
    Console.WriteLine();


    try
    {
        switch (op)
        {
            case "1": await ListarAsync(); break;
            case "2": await CrearPlantaAsync(); break;
            case "3": await CrearTemporalAsync(); break;
            case "4": await RenombrarAsync(); break;
            case "5": await EliminarAsync(); break;
            case "6": await AsignarHorarioAsync(); break;
            case "7": await RegistrarEvaluacionAsync(); break;
            case "8": await ReporteNominaAsync(); break;
            case "9": await ReporteDesempenoAsync(); break;
            case "0": return;
            default: Console.WriteLine("Opción no válida."); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
async Task ListarAsync()
{
    var lista = await crud.ListarAsync();
    if (lista.Count == 0) { Console.WriteLine("(sin empleados)\n"); return; }
    foreach (var e in lista)
    {
        Console.WriteLine($"- {e} | Id: {e.Id}");
    }
}

async Task CrearPlantaAsync()
{
    Console.Write("Documento: "); var doc = Console.ReadLine()!;
    Console.Write("Nombres: "); var nom = Console.ReadLine()!;
    Console.Write("Apellidos: "); var ape = Console.ReadLine()!;
    Console.Write("Salario base mensual: "); var baseStr = Console.ReadLine()!;
    Console.Write("Bono fijo (0 si no aplica): "); var bonoStr = Console.ReadLine()!;


    var id = await crud.CrearPlantaAsync(doc, nom, ape,
    decimal.Parse(baseStr), decimal.Parse(bonoStr));
    Console.WriteLine($"Creado PLANTA con Id {id}\n");
}
async Task CrearTemporalAsync()
{
    Console.Write("Documento: "); var doc = Console.ReadLine()!;
    Console.Write("Nombres: "); var nom = Console.ReadLine()!;
    Console.Write("Apellidos: "); var ape = Console.ReadLine()!;
    Console.Write("Tarifa por hora: "); var thStr = Console.ReadLine()!;


    var id = await crud.CrearTemporalAsync(doc, nom, ape, decimal.Parse(thStr));
    Console.WriteLine($"Creado TEMPORAL con Id {id}\n");
}

async Task RenombrarAsync()
{
    var emp = await PickEmpleadoAsync();
    if (emp is null) return;
    Console.Write("Nuevos nombres: "); var nom = Console.ReadLine()!;
    Console.Write("Nuevos apellidos: "); var ape = Console.ReadLine()!;
    emp.Renombrar(nom, ape);
    await crud.ActualizarAsync(emp);
    Console.WriteLine("Actualizado.\n");
}
async Task EliminarAsync()
{
    var emp = await PickEmpleadoAsync();
    if (emp is null) return;
    await crud.EliminarAsync(emp.Id);
    Console.WriteLine("Eliminado.\n");
}
async Task AsignarHorarioAsync()
{
    var emp = await PickEmpleadoAsync();
    if (emp is null) return;
    Console.Write("Inicio (yyyy-MM-dd HH:mm): "); var i = DateTime.Parse(Console.ReadLine()!);
    Console.Write("Fin (yyyy-MM-dd HH:mm): "); var f = DateTime.Parse(Console.ReadLine()!);


    var res = emp.AgregarHorario(new Horario(i, f));
    if (!res.IsSuccess) Console.WriteLine($"No se pudo: {res.Error}\n");
    else { await crud.ActualizarAsync(emp); Console.WriteLine("Horario agregado.\n"); }
}
async Task RegistrarEvaluacionAsync()
{
    var emp = await PickEmpleadoAsync();
    if (emp is null) return;
    Console.Write("Fecha (yyyy-MM-dd): "); var fecha = DateTime.Parse(Console.ReadLine()!);
    Console.Write("Puntuación 1..5: "); var punt = int.Parse(Console.ReadLine()!);
    Console.Write("Comentario: "); var com = Console.ReadLine() ?? string.Empty;
    emp.RegistrarEvaluacion(new Evaluacion(fecha, punt, com));
    await crud.ActualizarAsync(emp);
    Console.WriteLine("Evaluación registrada.\n");
}
async Task ReporteNominaAsync()
{
    Console.Write("Periodo año (YYYY): "); var anio = int.Parse(Console.ReadLine()!);
    Console.Write("Periodo mes (1..12): "); var mes = int.Parse(Console.ReadLine()!);
    var per = new MesAnio(anio, mes);


    var mapa = await nominaService.CalcularNominaAsync(per);
    var lista = await crud.ListarAsync();


    Console.WriteLine($"\n--- Nómina {per} ---");
    decimal total = 0m;
    foreach (var kv in mapa)
    {
        var e = lista.First(x => x.Id == kv.Key);
        Console.WriteLine($"{e.Nombres} {e.Apellidos} [{e.TipoContrato}] => {kv.Value:C}");
        total += kv.Value;
    }
    Console.WriteLine($"TOTAL: {total:C}\n");
}
async Task ReporteDesempenoAsync()
{
    var emp = await PickEmpleadoAsync();
    if (emp is null) return;
    var prom = await desempenoService.PromedioEmpleadoAsync(emp.Id);
    Console.WriteLine($"Promedio de {emp.Nombres} {emp.Apellidos}: {prom:F2}\n");
}


async Task<EmpleadoBase?> PickEmpleadoAsync()
{
    var lista = await crud.ListarAsync();
    if (lista.Count == 0) { Console.WriteLine("No hay empleados\n"); return null; }
    for (int i = 0; i < lista.Count; i++)
        Console.WriteLine($"{i + 1}) {lista[i]} | Id: {lista[i].Id}");
    Console.Write("Seleccione #: ");
    if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > lista.Count)
    { Console.WriteLine("Selección inválida\n"); return null; }
    return lista[idx - 1];
}