using CodeFinalProject.DAL;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Controllers
{
    public class OurStaffController : Controller
    {
        private readonly HimaraDbContext _context;

        public OurStaffController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var staffs = _context.Staffs.Where(x => x.Title == "Staff").Take(8).ToList();
            return View(staffs);
        }
    }
}
