using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateApi.model;

namespace TemplateApi.Context
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<UserInEvent> UserInEvents { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Suggestion> Suggestions { get; set; }

        public DbSet<Complaint> Complaints { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<request_support> request_Supports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Name = "John" });

            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, CategoryName = "TestCategory" });

        }
    }
}
