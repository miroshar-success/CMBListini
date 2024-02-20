using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CMBListini.Models
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {

            //Verify if user is already logged in
            if (Session["Username"] == null)
                return View("Index");
            else return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformLogin(string Username, string Password)
        {
            Boolean GetLan = false;
            string visitorIPAddress = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (String.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Request.UserHostAddress;
            if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
            {
                GetLan = true;
                visitorIPAddress = string.Empty;
            }
            if (GetLan && string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            visitorIPAddress = "127.0.0.1";
                        }
                    }
                }

            }

            //check useragent
            string LogUserAgent = HttpContext.Request.UserAgent.ToString();


            //Check if login data is present
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                return Content(JsonConvert.SerializeObject(new { status = false, message = "Dati non validi" }));
            }

            else
            {
                try
                {
                    //Lookup for user into DB
                    using (CMBContext db = new CMBContext())
                    {
                        // string pwdSHA256Hash = CalculateSHA256(Password);
                        User usr = db.Users.Where(u => u.UserID == Username/* && u.PasswordHash == pwdSHA256Hash*/).FirstOrDefault();
                        //Organization org = db.Organizations.Where(t => t.OrganizationID == usr.OrganizationID).FirstOrDefault();
                        //User ok
                        if (usr != null)
                        {
                            Organization org = db.Organizations.Where(t => t.OrganizationID == usr.OrganizationID).FirstOrDefault();

                            if (org != null)
                            {
                                if (usr.ValidityDate > DateTime.Now)
                                {
                                    Session["Username"] = usr.UserID;
                                    Session["Organization"] = org.OrganizationName;
                                    LogWriter(usr.UserID, visitorIPAddress, LogUserAgent, true);
                                    return Content(JsonConvert.SerializeObject(new { status = true }));
                                }
                                else
                                {
                                    return Content(JsonConvert.SerializeObject(new { status = false, message = "Utente scaduto" }));
                                }

                            }

                            return Content(JsonConvert.SerializeObject(new { status = true }));
                        }

                        //User not found
                        else
                        {
                            LogWriter(Username, visitorIPAddress, LogUserAgent, false);
                            return Content(JsonConvert.SerializeObject(new { status = false, message = "Utente inesistente o password errata" }));
                        }
                    }
                }

                catch (Exception ex)
                {
                    return Content(JsonConvert.SerializeObject(new { status = false, message = ex.Message }));
                }
            }
        }


        private void LogWriter(string LogUser, string LogIp, string LogUserAgent, Boolean Success)
        {
            using (CMBContext dbCtx = new CMBContext())
            {
                LoginHistory newLog = new LoginHistory()
                {
                    LogUsername = LogUser,
                    LogIPAddress = LogIp,
                    LogUserAgent = LogUserAgent,
                    LoginSuccessful = Success

                };


                dbCtx.LoginHistories.Add(newLog);
                dbCtx.SaveChanges();
            }
        }



        private string CalculateSHA256(string Value)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(Value));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}