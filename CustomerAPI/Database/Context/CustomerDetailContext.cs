using CustomerAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Database.Context
{
    public class CustomerDetailContext : DbContext
    {
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public CustomerDetailContext(DbContextOptions<CustomerDetailContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Server=.;Database=CustomerDetail;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
    }
}
