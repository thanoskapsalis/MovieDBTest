using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MovieDB.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext () : base("MoviesConnection")
        {

        }
        public DbSet<Movie> MovieTable { get; set; }
    }
}