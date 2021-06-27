using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InSQL
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly WebStoreDB _db;

        public SqlEmployeesData(WebStoreDB db) => _db = db;

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            _db.Employees.Add(employee);
            _db.SaveChanges();
            return employee.Id;

        }

        public bool Delete(int id)
        {
            //if (Get(id) is not { } employee) return false;

            var employee = _db.Employees.Select(e => new Employee { Id = e.Id }).FirstOrDefault(e => e.Id == id);
            if (employee is null) return false;

            _db.Employees.Remove(employee);
            _db.SaveChanges();

            return true;
        }

        public Employee Get(int id) => _db.Employees.SingleOrDefault(em => em.Id == id);

        public IEnumerable<Employee> GetAll() => _db.Employees.ToArray();

        public void Update(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            _db.Employees.Update(employee);
            _db.SaveChanges();
        }
    }
}
