using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.Services.InMemory
{
    [Obsolete("Поддержка класса размещения сотрудников в памяти прекращена", true)]
    public class InMemoryEmployeesData : IEmployeesData
    {

        private int _CurrentMaxId;

        public InMemoryEmployeesData()
        {
            _CurrentMaxId = TestData.Employees.Max(x => x.Id);
        }

        public Employee Get(int id) => TestData.Employees.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Employee> GetAll() => TestData.Employees;

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (TestData.Employees.Contains(employee))
                return employee.Id;

            employee.Id = ++_CurrentMaxId;
            TestData.Employees.Add(employee);

            return employee.Id;
        }

        public void Update(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (TestData.Employees.Contains(employee)) return;

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
            return TestData.Employees.Remove(db_item);
        }

    }
}
