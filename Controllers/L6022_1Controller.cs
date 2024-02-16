using CMBListini.Models;
using CMBListini.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace CMBListini.Controllers
{
    public class L6022_1Controller : Controller
    {
        // GET: L6022_1
        public ActionResult Index()
        {

            ViewBag.Username = Session["username"];
            ViewBag.Organization = Session["Organization"];
            try
            {
                return View("Index", GetComponentsViewModelData());
            }
            catch
            {
                return RedirectToAction("Index", "Dashboard");
            }



        }
        private ViewModel6022_1 GetComponentsViewModelData()
        {
            ViewModel6022_1 VM = new ViewModel6022_1();


            L6022_1Calc quotation = new L6022_1Calc();

            using (CMBContext dbCtx = new CMBContext())
            {
                //Discount
                string Organization = (string)Session["Organization"];
                var OrganizationData = dbCtx.Organizations.Where(x => x.OrganizationName == Organization).First();
                string StringDiscount = OrganizationData.OrganizationDiscount ?? "0+0";
                if (!StringDiscount.Contains("+"))
                {
                    StringDiscount += "+0";
                }
                string Discount = StringDiscount;
                string[] subs = Discount.Split('+');
                //
                VM = new L6022_1Calc().ToViewModel(Int32.Parse(subs[0]), Int32.Parse(subs[1]));
                VM.OrganizationData = dbCtx.Organizations.Where(x => x.OrganizationName == Organization).First();

            }

            return VM;
        }
    }
}