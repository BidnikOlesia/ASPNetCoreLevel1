using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get} = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович", Age = 30, EmploymentDate = Convert.ToDateTime("05/18/2020"), Position = "Специалист отдела продаж" },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", MiddleName = "Петров", Age = 28, EmploymentDate = Convert.ToDateTime("10/05/2019"), Position = "Менеджер по работе с клиентами" },
            new Employee { Id = 3, LastName = "Сергеев", FirstName = "Сергей", MiddleName = "Сергеевич", Age = 19, EmploymentDate = Convert.ToDateTime("11/01/2021"), Position = "Специалист отдела поддержки" }
        };
    }
}
