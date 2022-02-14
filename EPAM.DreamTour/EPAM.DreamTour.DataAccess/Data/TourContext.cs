using EPAM.DreamTour.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.DreamTour.DataAccess.Data
{
    public class TourContext : DbContext
    {
        public TourContext(DbContextOptions options) : base(options) { }

        public DbSet<TourModel> Tours { get; set; }
    }
}
