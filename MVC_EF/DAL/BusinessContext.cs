using MVC_EF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MVC_EF.DAL
{
    public class BusinessContext : DbContext
    {
        public BusinessContext() : base("BusinessCxs") {}

        public DbSet <Area> Areas { get; set; }
        public DbSet <Contact> Contacts { get; set; }
        public DbSet <Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}