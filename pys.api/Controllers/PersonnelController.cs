using Microsoft.AspNetCore.Mvc;
using pys.api.Data;
using pys.api.Models;

public class PersonnelController : Controller
{
    private readonly PYSDBContext _context;

    public PersonnelController(PYSDBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var personnelList = _context.Personnel.ToList();
        return View(personnelList);
    }
}
