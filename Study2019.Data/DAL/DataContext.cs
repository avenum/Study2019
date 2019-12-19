using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study2019.Data.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=InstaDbEntities")
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }

        internal DbSet<Entities.Avatar> Avatars { get; set; }
        internal DbSet<Entities.User> Users { get; set; }
        internal DbSet<Entities.Image> Images { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

    }
}
