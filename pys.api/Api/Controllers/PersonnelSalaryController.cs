using Microsoft.AspNetCore.Mvc;
using pys.api.Api.Model;
using pys.api.Data;
using pys.api.Entities;
using pys.api.Services;
using System.Net;
using pys.api.Models;
namespace pys.api.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelSalaryController : ControllerBase
    {
        private PYSDBContext _context;
        private IPersonnelSalaryService _personnelSalaryService;

        public PersonnelSalaryController(PYSDBContext context, IPersonnelSalaryService personnelSalaryService)
        {
            _context = context;
            _personnelSalaryService = personnelSalaryService;
        }

        [HttpGet("GetPersonnelSalary")]
        public ActionResult<List<PersonnelSalary>> GetPersonnelSalary()
        {

            var personnelSalaryList = _personnelSalaryService.PersonnelSalaryList();
            return _context.Set<pys.api.Entities.PersonnelSalary>().ToList();

        }

        [HttpPost("AddPersonnelSalary")]
        public ActionResult<HttpStatusCode> AddPersonnelSalary([FromBody] PersonnelSalaryAddModel model)
        {
            var personnel = new PersonnelSalary
            {
                name = model.Name,
                Surname = model.Surname,
                Salary = model.Salary,

            };

            _context.PersonnelSalary.Add(personnel);
            _context.SaveChanges();

            return HttpStatusCode.OK;
        }


        [HttpPost("DeletePersonnelSalary")]
        public ActionResult<HttpStatusCode> DeletePersonnelSalary([FromBody] PersonnelSalaryDeleteModel model)
        {
            PersonnelSalary personnelSalary = _context.PersonnelSalary.FirstOrDefault(x => x.Id == model.Id);

            if (personnelSalary == null)
                return HttpStatusCode.NotFound;

            _context.PersonnelSalary.Remove(personnelSalary);
            _context.SaveChanges();

            return HttpStatusCode.OK;
        }

        [HttpPost("UpdatePersonnelSalary")]
        public ActionResult<HttpStatusCode> UpdatePersonnelSalary([FromBody] PersonnelSalaryUpdateModel model)
        {
            var personnelSalary = _context.PersonnelSalary.FirstOrDefault(x => x.Id == model.Id);

            personnelSalary.Id = model.Id;
            personnelSalary.name = model.Name;
            personnelSalary.Surname = model.Surname;
            personnelSalary.Salary = model.Salary;

            _context.Update(personnelSalary);
            _context.SaveChanges();

            return HttpStatusCode.OK;
        }
    }
}

