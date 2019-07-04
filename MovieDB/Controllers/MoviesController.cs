using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MovieDB.Classes;
using MovieDB.Models;
using Newtonsoft.Json;

namespace MovieDB.Controllers
{
    public class MoviesController : Controller
    {
        public static List<Movie> res=new List<Movie> ();
        private List<Movie> _toAdd=new List<Movie> ();


        // GET: Movies/Random
        public ActionResult Random ()
        {
            return View ();
        }

        public ActionResult TestFunction(string MovieName)
        {
            res.Clear ();;
            try
            {
                var tosearch=MovieName.Split('\n').ToArray ();
                for (var i=0; i < tosearch.Length; i++)
                    foreach (var item in CallAPI(tosearch[i]))
                        res.Add(item);
            }
            catch (NullReferenceException e)
            {
                return Json(new {Success=false, e.Message});
            }


            return RedirectToAction("Result", "Movies", res);
        }


        public List<Movie> CallAPI(string searchtext)
        {
            var searchMovies=new List<Movie> ();
            try
            {
                var apiKey= MovieDB.Properties.Settings.Default.API_KEY;
                var apiRequest=
                    WebRequest.Create("https://api.themoviedb.org/3/search/movie?" + apiKey + "&query=" + searchtext) as
                        HttpWebRequest;
                apiRequest.UseDefaultCredentials=true;
                apiRequest.PreAuthenticate=true;
                apiRequest.Credentials=CredentialCache.DefaultCredentials;
                var apiResponse="";
                ServicePointManager.SecurityProtocol=SecurityProtocolType.Ssl3
                                                     | SecurityProtocolType.Tls
                                                     | SecurityProtocolType.Tls11
                                                     | SecurityProtocolType.Tls12;


                using (var response=apiRequest.GetResponse () as HttpWebResponse)
                {
                    var reader=new StreamReader(response.GetResponseStream ());
                    apiResponse=reader.ReadToEnd ();
                }


                var rootObject=JsonConvert.DeserializeObject<RootObject>(apiResponse);
                foreach (var i in rootObject.results) searchMovies.Add(new Movie(i.id, i.title));

                return searchMovies;
            }
            catch (Exception ex)
            {
                return searchMovies;
            }
        }

        public ActionResult Result ()
        {
            var movie=new MovieModel {Movies=res};
            movie.Movies=res;
            return View(movie);
        }

        [HttpPost]
        public ActionResult Result(MovieModel model)
        {
            for (var i=0; i < res.Count; i++)
                if (model.Movies[i].ischecked)
                    AddtoDB(model.Movies[i]);


            return  RedirectToAction("Items","Movies");
        }


        public ActionResult Items ()
        {
            var db=new MovieContext ();
            return View(db.MovieTable.ToList ());
        }


        public void AddtoDB(Movie movie)
        {
            using (var db=new MovieContext ())
            {
                Movie mv = new Movie(){Id = movie.Id, Name = movie.Name, ischecked = movie.ischecked, movieId = movie.movieId};
                db.MovieTable.Add(mv);
                db.SaveChanges();
            }
        }

        public ActionResult Details(string name, int movieId)
        {
            var search_link="https://www.themoviedb.org/movie/" + movieId + "-" + name;
            return Redirect(search_link);
        }

        public ActionResult Delete(int id)
        {
            using (var db=new MovieContext ())
            {
                var deletedmovie=db.MovieTable.Where(c => c.Id == id).FirstOrDefault ();
                db.MovieTable.Remove(deletedmovie);
                db.SaveChanges ();
            }

            return RedirectToAction("Items");
        }
    }
}