using Client.WebsearchMedia.Models;
using EntityDataAccess.Core;
using EntityObjects.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityServices;
using WebSeearchMediaService;

namespace Client.WebsearchMedia.Controllers
{
    public class HomeController : Controller
    {
        EFDbContext _dbContext = new EFDbContext();
        // GET: Home
        public ActionResult Index()
        {
            DateTime datetime = DateTime.Now.Date;
            var data = _dbContext.CommonLeads.Where(m => m.GeneratedDateTime >= datetime);

            List<TransferCommonLeads> tCmnData = new List<TransferCommonLeads>();
            foreach(CommonLead cmn in data)
            {
                var tlog = _dbContext.TransferLogs.Where(m => m.CommonLeadId == cmn.Id).OrderByDescending(m=>m.ID).FirstOrDefault();

                TransferCommonLeads t = new Models.TransferCommonLeads
                {
                    Title = cmn.Title,
                    CommonLeadId = cmn.Id,
                    Address = cmn.Address,
                   // BuyerId = tlog.BuyerMasterId,
                    BuyerName = null,
                    City = cmn.City,
                    Email = cmn.Email,
                    FirstName = cmn.FirstName,
                    FullName = cmn.FirstName +" "+cmn.LastName,
                    GeneratedDateTime = cmn.GeneratedDateTime,
                    HomePhone = cmn.HomePhone,
                    Keyword = cmn.Keyword,
                    LastName = cmn.LastName,
                    PostCode = cmn.PostCode,
                    ProductName = cmn.ProductName,
                    Source = cmn.Source,
                    Status = cmn.Status,
                    MatchType = cmn.MatchType,
                   // TransferDateTime = tlog.GeneratedDatetime,
                    WorkPhone = cmn.WorkPhone

                };

                if(tlog != null)
                {
                    var buyerName = _dbContext.BuyerMasters.Where(m => m.Id == tlog.BuyerMasterId).FirstOrDefault().Name;
                    t.BuyerId = tlog.BuyerMasterId;
                    t.BuyerName = buyerName;
                    t.Descriptions = tlog.Description;
                    t.TransferDateTime = tlog.GeneratedDatetime;
                    t.Status = tlog.Status;

                }
                tCmnData.Add(t);
            }


            return View(tCmnData.OrderByDescending(m=>m.CommonLeadId));
        }


        //[HttpPost]
        //public ActionResult Index(DateTime startDate , DateTime endDate)
        //{
        //    DateTime datetime = DateTime.Now.Date;
        //    var data = _dbContext.CommonLeads.Where(m => m.GeneratedDateTime >= datetime);

        //    List<TransferCommonLeads> tCmnData = new List<TransferCommonLeads>();
        //    foreach (CommonLead cmn in data)
        //    {
        //        var tlog = _dbContext.TransferLogs.Where(m => m.CommonLeadId == cmn.Id).FirstOrDefault();

        //        TransferCommonLeads t = new Models.TransferCommonLeads
        //        {
        //            Title = cmn.Title,
        //            CommonLeadId = cmn.Id,
        //            Address = cmn.Address,
        //            // BuyerId = tlog.BuyerMasterId,
        //            BuyerName = null,
        //            City = cmn.City,
        //            Email = cmn.Email,
        //            FirstName = cmn.FirstName,
        //            FullName = cmn.FirstName + " " + cmn.LastName,
        //            GeneratedDateTime = cmn.GeneratedDateTime,
        //            HomePhone = cmn.HomePhone,
        //            Keyword = cmn.Keyword,
        //            LastName = cmn.LastName,
        //            PostCode = cmn.PostCode,
        //            ProductName = cmn.ProductName,
        //            Source = cmn.Source,
        //            Status = cmn.Status,
        //            MatchType = cmn.MatchType,
        //            // TransferDateTime = tlog.GeneratedDatetime,
        //            WorkPhone = cmn.WorkPhone

        //        };

        //        if (tlog != null)
        //        {
        //            var buyerName = _dbContext.BuyerMasters.Where(m => m.Id == tlog.BuyerMasterId).FirstOrDefault().Name;
        //            t.BuyerId = tlog.BuyerMasterId;
        //            t.BuyerName = buyerName;
        //            t.Descriptions = tlog.Description;
        //            t.TransferDateTime = tlog.GeneratedDatetime;
        //            t.Status = tlog.Status;

        //        }
        //        tCmnData.Add(t);
        //    }


        //    return View(tCmnData.OrderByDescending(m => m.CommonLeadId));
        //}

        public ActionResult Edit(int id)
        {
            //if (id > 0)
            //{
            //    var cmnLead = _dbContext.CommonLeads.Where(m => m.Id == id).FirstOrDefault();


            //    string prodName = UtilityMethods.GetProductName(cmnLead.ProductName);


            //    if (prodName == GlobalConstant.Life)
            //    {
            //        var lifeLead = _dbContext.LifeLeads.Where(m => m.CommonLeadId == cmnLead.Id).FirstOrDefault();
            //    }
            //}
            CommonLead cmnLead = UtilityMethods.GetCommonLeads(id);
            return View(cmnLead);
        }
      

        public ActionResult LeadTransferDetails(CommonLead cmnlead)
        {
            CommonLead cmn = UtilityMethods.GetCommonLeads(cmnlead.Id);

            return View(cmn);


        }

        public ActionResult SendLeadToAdmin(int id)
        {
            CaptureService _capture = new CaptureService();
            // var id = _captureService.NewAdminLeadCapture(lead, "http://localhost:3431/dataConverstion/capture");

            //  var id = _captureService.NewAdminLeadCapture(lead, "https://api.websearchmedia.co.uk/dataConverstion/capture");
            if (id > 0) {
                var returnid = _capture.LeadTransfer("https://api.websearchmedia.co.uk/dataConverstion/LeadTransfer?leadId="+id);
                    }
            return View();
        }
        [HttpPost]
        public ActionResult UpdateCommonlead(CommonLead cmn)
        {
            CommonLead cmnLead = UtilityMethods.GetCommonLeads(cmn.Id);
            cmnLead.FirstName = cmn.FirstName;
            cmnLead.GeneratedDateTime = cmn.GeneratedDateTime;
            cmnLead.IpAddress = cmn.IpAddress ?? string.Empty;
            cmnLead.Keyword = cmn.Keyword ?? string.Empty;
            cmnLead.MatchType = cmn.MatchType ?? string.Empty;
            cmnLead.LastName = cmn.LastName;
            cmnLead.PostCode = cmn.PostCode;
            cmnLead.ProductName = cmn.ProductName;
            cmnLead.SiteId = cmn.SiteId;
            cmnLead.Source = cmn.Source ?? string.Empty;
            cmnLead.Status = cmn.Status;
            cmnLead.Title = cmn.Title;
            cmnLead.Email = cmn.Email;
            cmnLead.HomePhone = cmn.HomePhone;
            cmnLead.WorkPhone = cmn.WorkPhone;
            cmnLead.City = cmn.City ?? string.Empty;
            cmnLead.Address = cmn.Address;
            cmnLead.UpdatedDateTime = DateTime.Now;

           
            _dbContext.Entry(cmnLead).State = EntityState.Modified;
           var v= _dbContext.SaveChanges();
            if (v > 0)
                return RedirectToAction("LeadTransferDetails", cmnLead);
            return View();
        }

        public ActionResult LeadDetailsByDate()
        {
            DateTime baseDate = DateTime.Today;

            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

            DateTime startDate = thisWeekStart;
            DateTime endDate = thisWeekEnd;
            List<CommonLead> cmnLaed = _dbContext.CommonLeads
                           .Where(m => m.GeneratedDateTime >= startDate.Date && m.GeneratedDateTime <= endDate.Date).ToList();
            ViewBag.SelectedDateRange = 3;
            return View(cmnLaed);
        }

        [HttpPost]
        public ActionResult LeadDetailsByDate(LeadDetailsDateTypeEnum dateRange,int leadId=0)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
           DateTime baseDate = DateTime.Today;

            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);
            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            var lastWeekStart = thisWeekStart.AddDays(-7);
            var lastWeekEnd = thisWeekStart.AddSeconds(-1);
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            var lastMonthStart = thisMonthStart.AddMonths(-1);
            var lastMonthEnd = thisMonthStart.AddSeconds(-1);

            switch (dateRange)
            {
                case LeadDetailsDateTypeEnum.today:
                    {
                        startDate = today;
                        endDate = today;

                    }
                    break;
                case LeadDetailsDateTypeEnum.yesterday:
                    {
                        startDate = yesterday;
                        endDate = today;

                    }
                    break;
                case LeadDetailsDateTypeEnum.thisweek:
                    {
                        startDate = thisWeekStart;
                        endDate = thisWeekEnd;

                    }
                    break;
                case LeadDetailsDateTypeEnum.lastweek:
                    {
                        startDate = lastWeekStart;
                        endDate = lastWeekEnd;

                    }
                    break;
                case LeadDetailsDateTypeEnum.thismonth:
                    {
                        startDate = thisMonthStart;
                        endDate = thisMonthEnd;

                    }
                    break;

                case LeadDetailsDateTypeEnum.lastmonth:
                    {
                        startDate = lastMonthStart;
                        endDate = lastMonthEnd;

                    }
                    break;
            }




            List<CommonLead> cmnLaed = _dbContext.CommonLeads
                                .Where(m => m.GeneratedDateTime >= startDate.Date && m.GeneratedDateTime <= endDate.Date).ToList();

            return View(cmnLaed);
        }

        public ActionResult LeadTransferDetailsByDate()
        {
            DateTime baseDate = DateTime.Today;

            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

            DateTime startDate = thisWeekStart;
            DateTime endDate = thisWeekEnd;
            List<TransferLog> cmnLaed = _dbContext.TransferLogs
                           .Where(m => m.GeneratedDatetime >= startDate.Date && m.GeneratedDatetime <= endDate.Date).ToList();
            ViewBag.SelectedDateRange = 3;

            return View(cmnLaed);
        }

        [HttpPost]
        public ActionResult LeadTransferDetailsByDate(LeadDetailsDateTypeEnum dateRange, int leadId = 0)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            DateTime baseDate = DateTime.Today;

            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);
            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            var lastWeekStart = thisWeekStart.AddDays(-7);
            var lastWeekEnd = thisWeekStart.AddSeconds(-1);
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            var lastMonthStart = thisMonthStart.AddMonths(-1);
            var lastMonthEnd = thisMonthStart.AddSeconds(-1);

            switch (dateRange)
            {
                case LeadDetailsDateTypeEnum.today:
                    {
                        startDate = today;
                        endDate = today;

                    }
                    break;
                case LeadDetailsDateTypeEnum.yesterday:
                    {
                        startDate = yesterday;
                        endDate = today;

                    }
                    break;
                case LeadDetailsDateTypeEnum.thisweek:
                    {
                        startDate = thisWeekStart;
                        endDate = thisWeekEnd;

                    }
                    break;
                case LeadDetailsDateTypeEnum.lastweek:
                    {
                        startDate = lastWeekStart;
                        endDate = lastWeekEnd;

                    }
                    break;
                case LeadDetailsDateTypeEnum.thismonth:
                    {
                        startDate = thisMonthStart;
                        endDate = thisMonthEnd;

                    }
                    break;

                case LeadDetailsDateTypeEnum.lastmonth:
                    {
                        startDate = lastMonthStart;
                        endDate = lastMonthEnd;

                    }
                    break;
            }




            List<TransferLog> tlogs = _dbContext.TransferLogs
                                .Where(m => m.GeneratedDatetime >= startDate.Date && m.GeneratedDatetime <= endDate.Date).ToList();
            
            return View(tlogs);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["user"] != null)
                base.OnActionExecuting(filterContext);
            else
                filterContext.Result = new RedirectResult("Login");
        }
        public enum LeadDetailsDateTypeEnum
        {
            today =1,
            yesterday,
            thisweek,
            lastweek,
            thismonth,
            lastmonth

        }
    }
}