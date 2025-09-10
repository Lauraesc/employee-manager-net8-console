# Employee Manager - .NET 8 Console Application

This project is a **console-based Employee Management System** developed in **C# using .NET 8**.  
It demonstrates object-oriented programming principles such as **inheritance, polymorphism, encapsulation, and abstraction**, as well as design principles like **Dependency Inversion (DIP)**.

---

## 1. Features

The system implements the following **functional requirements (RF)**:

| RF   | Description |
|------|-------------|
| RF-01 | CRUD operations for employees (`Application/UseCases/EmpleadosCrud.cs`) |
| RF-02 | Employee types using inheritance (`EmpleadoPlanta.cs`, `EmpleadoTemporal.cs`) |
| RF-03 | Polymorphic salary calculation (`EmpleadoBase.CalcularSalario()`) |
| RF-04 | Schedule management with conflict validation (`EmpleadoBase.AgregarHorario()`, `Horario.SeSuperponeCon()`) |
| RF-05 | Performance evaluation (`Evaluacion`, `EmpleadoBase.RegistrarEvaluacion()`) |
| RF-06 | Payroll report (`NominaService`) |
| RF-07 | Performance report (`DesempenoService`) |
| RF-08 | Persistence using repository pattern and DIP (`IEmpleadoRepository`, InMemory/Json) |

---


---
## 2. Project Structure

EmployeeManager
│
├─ Application/
│ ├─ UseCases/
│ │ └─ EmpleadosCrud.cs # CRUD operations
│ └─ Services/
│ ├─ NominaService.cs # Payroll calculations
│ └─ DesempenoService.cs # Performance report
│
├─ Domain/
│ ├─ Empleados/
│ │ ├─ EmpleadoBase.cs
│ │ ├─ EmpleadoPlanta.cs
│ │ └─ EmpleadoTemporal.cs
│ ├─ Horarios/
│ │ └─ Horario.cs
│ ├─ Evaluaciones/
│ │ └─ Evaluacion.cs
│ └─ Repositorios/
│ └─ IEmpleadoRepository.cs
│
├─ Infrastructure/
│ └─ Persistence/
│ ├─ InMemory/
│ │ └─ EmpleadoRepositoryInMemory.cs
│ └─ Json/
│ └─ EmpleadoRepositoryJson.cs
│
└─ Program.cs # Console menu and entry point

## 3. How to Run

1. **Clone the repository:**


git clone https://github.com/Lauraesc/employee-manager-net8-console.git
cd employee-manager-net8-console

2. **Build the project:**
 dotnet build
3. **Run the application:**
dotnet run
4. **Menu Options:**
Add new employees (Planta or Temporal)
List employees
Update employee details
Delete employees
Add schedules
Register performance evaluations
Generate payroll report
Generate performance report

Design Decisions

OOP Principles:
Inheritance: EmpleadoPlanta and EmpleadoTemporal inherit from EmpleadoBase.
Polymorphism: CalcularSalario() overridden per employee type.
Encapsulation: Schedule conflict logic in EmpleadoBase.AgregarHorario() and Horario.SeSuperponeCon().
Abstraction: Services handle business logic separate from data storage.
Repository Pattern (DIP):
The application depends on IEmpleadoRepository interface rather than concrete classes.
Two implementations: InMemory and Json storage.

Separation of Concerns:

CRUD operations (EmpleadosCrud.cs) are separate from the console UI (Program.cs).
Business logic in services (NominaService, DesempenoService).
Domain entities encapsulate data and behavior.

Authors: Ziuvar Ruiz Alvarez, Laura Escobar Rojo
Technology: .NET 8, C#
Project Type: Console App, OOP, DIP, Repository Pattern

