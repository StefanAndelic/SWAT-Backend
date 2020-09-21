using System;
using Microsoft.EntityFrameworkCore;
using SWAT_Project_API.Models;

namespace SWAT_Project_API.Data
{
    public class SWATDbContext : DbContext
    {
        public SWATDbContext(DbContextOptions<SWATDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }


    }
}
