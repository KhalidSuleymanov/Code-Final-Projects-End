namespace CodeFinalProject.Areas.Manage.ViewModels
{
    public class AdminDetailViewModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
