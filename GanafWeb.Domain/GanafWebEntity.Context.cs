﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GanafWeb.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GanafDBEntities : DbContext
    {
        public GanafDBEntities()
            : base("name=GanafDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<contact> contacts { get; set; }
        public DbSet<request> requests { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<post> posts { get; set; }
        public DbSet<authenticatedUser> authenticatedUsers { get; set; }
    }
}
