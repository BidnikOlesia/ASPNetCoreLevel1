using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee Get(int id);

        int Add(Employee employee);

        void Update(Employee employee);

        bool Delete(int id);

    }
}
