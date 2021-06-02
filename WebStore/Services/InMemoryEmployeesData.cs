using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _Employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович", Age = 30, EmploymentDate = "18.05.2020", Position = "Специалист отдела продаж" },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", MiddleName = "Петров", Age = 28, EmploymentDate = "10.05.2019", Position = "Менеджер по работе с клиентами" },
            new Employee { Id = 3, LastName = "Сергеев", FirstName = "Сергей", MiddleName = "Сергеевич", Age = 19, EmploymentDate = "18.01.2021", Position = "Специалист отдела поддержки" }
        };
        private int _CurrentMaxId;

        public InMemoryEmployeesData()
        {
            _CurrentMaxId = _Employees.Max(x => x.Id);
        }

        public Employee Get(int id) => _Employees.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Employee> GetAll() => _Employees;

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee))
                return employee.Id;

            employee.Id = ++_CurrentMaxId;
            _Employees.Add(employee);

            return employee.Id;
        }

        public void Update(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee))  return;

            var db_item = Get(employee.Id);
            if (db_item is null) return;

            db_item.LastName = employee.LastName;
            db_item.FirstName = employee.FirstName;
            db_item.MiddleName = employee.MiddleName;
            db_item.Age = employee.Age;
            db_item.EmploymentDate = employee.EmploymentDate;
            db_item.Position = employee.Position;
        }

        public bool Delete(int id)
        {
            var db_item = Get(id);
            if (db_item is null) return false;
            return _Employees.Remove(db_item);
        }
    }
}
