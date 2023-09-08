using LibraryClass.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Data
{
    public class MyDBContext : DbContext //IdentityDbContext<IdentityUser> 
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        public DbSet<Item> items { get; set; }
        public DbSet<PlaceOrder> placeOrders { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Register> registers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
