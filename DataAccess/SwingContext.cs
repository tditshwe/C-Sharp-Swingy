using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Swingy.Models;

namespace Swingy.DataAcces
{
    public class SwingContext: DbContext
    {
        public SwingContext() : base("SwingContext")
        {
        }

        public DbSet<HeroEntity> Heroes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
