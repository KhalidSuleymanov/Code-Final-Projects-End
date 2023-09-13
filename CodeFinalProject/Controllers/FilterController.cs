using CodeFinalProject.DAL;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFinalProject.Controllers
{
    public class FilterController : Controller
    {
        private readonly HimaraDbContext _context;

        public FilterController(HimaraDbContext context)
        {
            _context = context;
        }
       
    }
}
