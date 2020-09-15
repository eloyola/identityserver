namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using API.Models;
    using Bogus;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EmployeeController: ControllerBase
    {
        private IEnumerable<Employee> Employee;
        public EmployeeController()
        {
            var fakeEmployee = new Faker<Employee>()
              .RuleFor(x => x.LastName, x => x.Person.LastName)
              .RuleFor(x => x.Name, x => x.Person.FullName)
              .RuleFor(x => x.Country, x => x.Person.Address.City)
              .RuleFor(x => x.Email, x => x.Person.Email);
            this.Employee = fakeEmployee.Generate(10);

        }
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return this.Employee;
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return this.Employee.ToList().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
