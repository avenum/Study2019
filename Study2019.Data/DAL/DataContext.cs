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

        public DbSet<Entities.Avatar> Avatars { get; set; }
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Image> Images { get; set; }
        public DbSet<Entities.PostImage> PostImages { get; set; }
        public DbSet<Entities.Post> Posts { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

    }
}
