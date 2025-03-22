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
    public class PersonnelController : ControllerBase
    {
        private PYSDBContext _context;
        private IPersonnelService _personnelService;
        
        public PersonnelController(PYSDBContext context, IPersonnelService personnelService)
        {
            _context = context;
            _personnelService = personnelService;
        }


        [HttpGet("GetPersonnels")]
        public  ActionResult<List<Personnel>> GetPersonnel()
        {
            var personnelList = _personnelService.PersonnelList();
            return personnelList;
        }

        [HttpPost("AddPersonnel")]
        public ActionResult<HttpStatusCode> AddPersonnel([FromBody] PersonnelAddModel model)
        {
            var personnel = new Personnel
            {
                Name = model.Name,
                Surname = model.Surname,
                Age = model.Age,
                Gender = model.Gender,
                FatherName = model.FatherName,
                MotherName = model.MotherName,
                FirstStartDate = model.FirstStartDate,
                EndDate = model.EndDate
            };

            _context.Personnel.Add(personnel);
            _context.SaveChanges();

            return HttpStatusCode.OK;
        }


        [HttpPost("DeletePersonnel")]
        public ActionResult<HttpStatusCode> DeletePersonnel([FromBody] PersonnelDeleteModel model)
        {

            Personnel personnel = _context.Personnel.FirstOrDefault(x => x.Id == model.Id); //linq
            if (personnel == null)
                return HttpStatusCode.NotFound;

            _context.Personnel.Remove(personnel);
            _context.SaveChanges();

            return HttpStatusCode.OK;
        }

        [HttpPost("UpdatePersonnel")]
        public ActionResult<HttpStatusCode> UpdatePersonnel([FromBody] PersonnelUpdateModel model)
        {
            var personnel = _context.Personnel.FirstOrDefault(x => x.Id == model.Id);

            personnel.Id = model.Id;
            personnel.Name = model.Name;
            personnel.Surname = model.Surname;
            personnel.FatherName = model.FatherName;
            personnel.MotherName = model.MotherName;
            personnel.Gender = model.Gender;

            _context.Update(personnel);
            _context.SaveChanges();

            return HttpStatusCode.OK;

        }
    }
}
