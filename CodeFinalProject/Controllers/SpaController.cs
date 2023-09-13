using CodeFinalProject.DAL;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Controllers
{
    public class SpaController : Controller
    {
        private readonly HimaraDbContext _context;
        public SpaController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            SpaViewModel SpaVM = new SpaViewModel
            {
                Staff = _context.Staffs.Where(x=>x.Title=="Spa").ToList(),
                SpaServices = _context.SpaServices.Take(4).ToList(),
            };
            return View(SpaVM);
        }
    }
}
