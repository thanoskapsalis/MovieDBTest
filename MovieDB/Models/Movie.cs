using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MovieDB.Models
{
    [Table("Movie")]
    public  class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public bool ischecked { get; set; }

        public int movieId { get; set; }

        public string overview { get; set; }
        public string posterpath { get; set; }
        public string releasedate { get; set; }

       // public virtual ICollection<Enrollment> Enrollments { get; set; }
        public Movie(int movieId,string Name,string overview,string posterpath,string releasedate)
        {
            this.Name=Name;
            this.movieId=movieId;
            this.overview=overview;
            this.posterpath=posterpath;
            this.releasedate=releasedate;
            ischecked=false;
        }

        public Movie ()
        {
        }

    }


    public class MovieModel
    {
        public List<Movie> Movies { get; set; }
    }
}