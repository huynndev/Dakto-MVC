using Sora.Entites.AD;
using Sora.Entites.CS;
using Sora.Entites.IC;
using Sora.Entites.ME;
using System.Data.Entity;

namespace Sora.EFCore.Contexts
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext() : base("name=HospitalDbConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ADUser> ADUsers { get; set; }

        public DbSet<CSCompany> CSCompany { get; set; }

        public DbSet<ICArticle> ICArticles { get; set; }

        public DbSet<ICArticleCategory> ICArticleCategories { get; set; }

        public DbSet<ICContactMessage> ICContactMessages { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<MEDoctor> MEDoctors { get; set; }

        public DbSet<MEDoctorSpecialist> MEDoctorSpecialists { get; set; }

        public DbSet<MESpecialist> MESpecialists { get; set; }

        public DbSet<MESpecialistType> MESpecialistTypes { get; set; }

        public DbSet<ICProductGroup> ICProductGroups { get; set; }

        public DbSet<ICProduct> ICProducts { get; set; }

        public DbSet<ICProductDetail> ICProductDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
