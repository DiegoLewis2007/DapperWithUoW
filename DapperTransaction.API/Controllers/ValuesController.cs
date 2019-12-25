using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DapperTransactionWithUoW.Domain.Entities;
using DapperTransactionWithUoW.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperTransactionWithUoW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IService service;


        public ValuesController(IService service)
        {
            this.service = service;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return service.GetAll().ToList();
        }

         // POST api/values
        [HttpPost]
        public void Post([FromBody] int batch)
        {
            var persons = new List<User>();

            Parallel.For(0, batch - 1,
            index => {

                lock (this)
                {
                    persons.Add(new Faker<User>("en_US")
                    .RuleFor(s => s.Id, g => g.UniqueIndex)
                    .RuleFor(s => s.Nome, g => g.Name.FirstName())
                    .RuleFor(s => s.SobreNome, g => g.Name.FullName())
                    .RuleFor(s => s.DataNascimento, g => g.Date.Future())
                    .RuleFor(s => s.Empresa, g=> g.Company.CompanyName())
                    .RuleFor(s => s.Ativo, false)
                    .RuleFor(s => s.Valor, g => g.Finance.Amount())
                    .Generate());
                }
            });

            service.Add(persons);
        }
    }
}
