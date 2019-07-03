using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieDB.Controllers
{
    public class ListController : Controller
    {
        // GET: List/SearchList
        public ActionResult SearchList()
        {
            return View();
        }
    }
}