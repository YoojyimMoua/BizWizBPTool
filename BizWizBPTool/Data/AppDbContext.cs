﻿using BizWizBPTool.Models;
using Microsoft.EntityFrameworkCore;

namespace BizWizBPTool.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BusinessPlan> BusinessPlans { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {}



    }
}
