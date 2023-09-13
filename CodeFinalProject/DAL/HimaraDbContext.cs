using CodeFinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeFinalProject.DAL
{
    public class HimaraDbContext : IdentityDbContext
    {
        public HimaraDbContext(DbContextOptions<HimaraDbContext> options) : base(options) { }
        public DbSet<AboutService> AboutServices { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealIngredient> MealIngredients { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SpaService> SpaServices { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<RecentPost> RecentPosts { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<RestaurantModal> RestaurantModals { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RoomReview> RoomReviews { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<UserContact> UsersContact { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealIngredient>().HasKey(x => new { x.MealId, x.IngredientId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
