using APlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlus.Data.Context
{
    public class APlusDbContext : DbContext
    {
        public APlusDbContext(DbContextOptions<APlusDbContext> options) : base(options)
        {

        }

        public virtual DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
