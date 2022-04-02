using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCRUD.Entities
{
    public class SimpleCrudDbContext : DbContext
    {
        public SimpleCrudDbContext()
        {

        }
        public SimpleCrudDbContext(DbContextOptions<SimpleCrudDbContext> options) : base(options)
        {

        }
        public virtual DbSet<TblEmployees> TblEmployees { get; set; }
        public virtual DbSet<TblUsers> TblUsers { get; set; }
        public virtual DbSet<TblAddress> TblAddresses { get; set; }
    }
}
