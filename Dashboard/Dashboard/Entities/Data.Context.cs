﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dashboard.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DashboardEntities : DbContext
    {
        public DashboardEntities()
            : base("name=DashboardEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ModuleStatu> ModuleStatus { get; set; }
        public virtual DbSet<RefModuleType> RefModuleTypes { get; set; }
        public virtual DbSet<StatusItem> StatusItems { get; set; }
    }
}
