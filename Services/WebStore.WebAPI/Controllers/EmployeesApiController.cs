﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddress.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeesData employeesData;

        public EmployeesApiController(IEmployeesData EmployeesData)=>employeesData = EmployeesData;

        [HttpGet]
        public IActionResult GetALl()
        {
            return Ok(employeesData.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(employeesData.Get(id));
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            var id = employeesData.Add(employee);
            return Ok(id);
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            employeesData.Update(employee);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = employeesData.Delete(id);
            return result ? Ok(true) : NotFound(false);
        }
    }
}
