using Newtonsoft.Json;
using System.Web.Mvc;

namespace CMBListini.Controllers
{

    // GET: Dashboard
    [SessionValidation]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Username = Session["username"];
            ViewBag.Organization = Session["Organization"];
            return View("Index");
        }

        [HttpPost]
        public ActionResult GetQuotationList()
        {

            return Content(JsonConvert.SerializeObject(new { status = false, message = "" }));

        }

        [HttpPost]
        public ActionResult GetInfos(int QuotationID)
        {



            return Content(JsonConvert.SerializeObject(new { status = false, message = "" }));

        }

        //advancedSearch
    }
}
