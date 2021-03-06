﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace credit.Models
{
    public class CreditContext:IdentityDbContext<User,IdentityRole<long>,long>
    {
        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<BaseInfo> BaseInfo { get; set; }
        public DbSet<TypeCS> TypeCS { get; set; }
        public DbSet<YearReportEnterprise> YearReportEnterprise { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Announcement>(e =>
            {
                e.HasIndex(x => x.Id);
            });
            builder.Entity<Info>(e =>
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
