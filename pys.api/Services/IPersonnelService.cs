using pys.api.Entities;
using System.Collections.Generic;

namespace pys.api.Services
{
    public interface IPersonnelService
    {
        void InsertPersonnel(Personnel personnel);
        List<Personnel> PersonnelList();
    }

}
