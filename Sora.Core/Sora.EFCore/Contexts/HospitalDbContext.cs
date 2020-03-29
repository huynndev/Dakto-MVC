using Sora.Entites.AD;
using Sora.Entites.BR;
using Sora.Entites.CS;
using Sora.Entites.GE;
using Sora.Entites.HR;
using Sora.Entites.IC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
