using DataRooms.UI.Code.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace DataRooms.UI.Areas.Home.Controllers
{
    public class SmtpTestController : Controller
    {
        private static Logger logger = LogManager.GetLogger("myAppLoggerRules");
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendTestEmail(string from, string to)
        {
            string errMessage = string.Empty;
            try
            {
                logger.Debug(from);
                logger.Debug(to);
                SendEmail sendEmail = new SendEmail();
                sendEmail.SendTestEmail(from, to);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace.ToString());
                errMessage = ex.Message;
            }
            TempData["ErrMessage"] = errMessage;
            return RedirectToAction("Index");
        }
    }
}