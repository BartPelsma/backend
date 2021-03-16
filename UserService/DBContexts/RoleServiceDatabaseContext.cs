﻿using Microsoft.EntityFrameworkCore;
using UserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.DBContexts
{
    /// <summary>
    /// Context of the database, used to communicate with the database.
    /// </summary>
    public class RoleServiceDatabaseContext : DbContext
    {
        /// <summary>
        /// DbSet for the Role class, A DbSet represents the collection of all entities in the context. 
        /// DbSet objects are created from a DbContext using the DbContext.Set method.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// OnConfiguring builds the connection between the database and the API using the given connection string
        /// </summary>
        /// <param name="optionsBuilder">Used for adding options to the database to configure the connection.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=mssql.fhict.local;Database=dbi331842;User Id=dbi331842;Password=xRqMZgWy76GrxM2;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}