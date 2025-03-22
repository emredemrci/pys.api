using pys.api.Entities;
using System.Collections.Generic;

namespace pys.api.Services
{
    public interface IPersonnelSalaryService
    {
        void InsertPersonnelSalary(PersonnelSalary personnelSalary); 
        List<PersonnelSalary> PersonnelSalaryList(); 
    }
}
