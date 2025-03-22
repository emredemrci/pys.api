using pys.api.Data;
using pys.api.Entities;

namespace pys.api.Services
{
    public class PersonnelService : IPersonnelService
    {
        private PYSDBContext _context;

        public PersonnelService(PYSDBContext context)
        {
            _context = context;
        }

        public void InsertPersonnel(Personnel personnel)
        {
            _context.Personnel.Add(personnel);
            _context.SaveChanges();
        }

        public List<Personnel> PersonnelList()
        {
            List<Personnel> personnelList = _context.Personnel.ToList();

            return personnelList;
        }
    }
}
