using pys.api.Data;
using pys.api.Entities;
using System.Collections.Generic;
using System.Linq;
using PersonnelSalaryEntity = pys.api.Entities.PersonnelSalary;


namespace pys.api.Services
{
    public class PersonnelSalaryService : IPersonnelSalaryService
    {
        private PYSDBContext _context;

        public PersonnelSalaryService(PYSDBContext context)
        {
            _context = context;
        }

        public void InsertPersonnelSalary(PersonnelSalaryEntity personnelSalary)
        {
            _context.PersonnelSalary.Add(personnelSalary);
            _context.SaveChanges();
        }

        public List<PersonnelSalaryEntity> PersonnelSalaryList()
        {
            return _context.PersonnelSalary.ToList();
        }

    }
}
