using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace credit.Models
{
    public class CreditContext:IdentityDbContext<User>
    {
        public DbSet<Message> Message { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>(e =>
            {
                e.HasIndex(x => x.Id);
            });

           
        }
    }
}
