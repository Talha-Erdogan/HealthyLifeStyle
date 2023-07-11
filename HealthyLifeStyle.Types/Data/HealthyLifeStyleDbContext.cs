using HealthyLifeStyle.Types.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Types.Data
{
    public class HealthyLifeStyleDbContext : DbContext
    {
        public DbSet<BloodGroup> BloodGroup { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<NeedForBlood> NeedForBlood { get; set; }
        public DbSet<User> User { get; set; }

        public HealthyLifeStyleDbContext(DbContextOptions<HealthyLifeStyleDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Fiziksel DB nerede olacak? ConnectionString bilgisini burada verebilirsiniz.
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=turkcellShopDb;Integrated Security=True");


            //program.cs içerisindeki deger ile okuyacagız.
            base.OnConfiguring(optionsBuilder);

        }
    }
}
