using System;
using Microsoft.EntityFrameworkCore;
using SQS.Publisher.Models;

namespace SQS.Publisher.Data
{
    public class OrderContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "OrdersDb");
        }


        public DbSet<Orders> Orders { get; set;}
    }
}

