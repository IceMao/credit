using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace credit.Models
{
    public class CreditContext:IdentityDbContext<User,IdentityRole<long>,long>
    {
        public DbSet<AnnouncementIllegal> AnnouncementIllegal { get; set; }
        public DbSet<AnnouncementUnsual> AnnouncementUnsual { get; set; }
        public DbSet<AnnouncementRandom> AnnouncementRandom { get; set; }
        public DbSet<InfoRandom> InfoRandom { get; set; }
        public DbSet<InfoIllegal> InfoIllegal { get; set; }
        public DbSet<InfoUnusual> InfoUnusual { get; set; }
        public DbSet<BaseInfo> BaseInfo { get; set; }
        public DbSet<TypeCS> TypeCS { get; set; }
        public DbSet<YearReportEnterprise> YearReportEnterprise { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AnnouncementRandom>(e =>
            {
                e.HasIndex(x => x.Id);
            });
            builder.Entity<AnnouncementUnsual>(e =>
            {
                e.HasIndex(x => x.Id);
            });
            builder.Entity<AnnouncementIllegal>(e =>
            {
                e.HasIndex(x => x.Id);
            });
            builder.Entity<InfoRandom>(e =>
            {
                e.HasIndex(x => x.Id);
            });

            builder.Entity<BaseInfo>(e =>
            {
                e.HasIndex(x => x.Id);
            });
            builder.Entity<YearReportEnterprise>(e =>
            {
                e.HasIndex(x => x.Id);
            });
        }
    }
}
