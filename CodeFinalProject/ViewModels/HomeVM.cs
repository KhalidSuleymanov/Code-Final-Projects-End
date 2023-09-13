using CodeFinalProject.Models;

namespace CodeFinalProject.ViewModels
{
    public class HomeVM
    {
        public List<Feature> Features { get; set; }
        public List<Staff> Staffs { get; set; }
        public List<Gallery> Galleries { get; set; }
        public List<Meal> Meals { get; set; }
        public List<Place> Places { get; set; }
        public List<AboutService> AboutServices { get; set; }
        public List<Service> Services { get; set; }
    }
}
