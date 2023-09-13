using CodeFinalProject.DAL;
using CodeFinalProject.Models;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly HimaraDbContext _context;
        public  BlogController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Detail(int id)
        //{
        //    var vm = _getBlogDetail(id);
        //    if (vm.Blog == null)
        //    {
        //        return View("Error");
        //    }
        //    return View(vm);
        //}
        //private BlogDetailViewModel _getBlogDetail(int id)
        //{
        //    var blog = _context.Blogs.Where(x => x.Id==id).ToList();
        //    BlogDetailViewModel vm = new BlogDetailViewModel()
        //    {
        //        Blog = blog,
        //    };
        //    return vm;
        //}
    }
}
