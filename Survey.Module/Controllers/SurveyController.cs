using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Environment.Shell.Scope;
using OrchardCore.Users.Services;
using Microsoft.AspNetCore.Http;
using OrchardCore.Admin;
using Survey.Module.Indexes;
using Survey.Module.Models;
using System.Data;
using System;
using System.Threading.Tasks;
using YesSql;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Users.Models;
using OrchardCore.Users.Indexes;
using System.Collections.Generic;

namespace Survey.Module.Controllers
{
    [Admin]
    public class SurveyController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly YesSql.ISession _session;
        public SurveyController(IAuthorizationService authorizationService, YesSql.ISession session)
        {
            _authorizationService = authorizationService;
            _session = session;
        }
        public async Task<ActionResult> Index()
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.SurveyAPIAccess))
            {
                return Unauthorized();
            }
            return View();
        }
        public async Task<ActionResult> Management()
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.SurveyAPIAccess))
            {
                return Unauthorized();
            }
            ModelState.Clear();
            var ipQuery = _session.Query<IpModel, IpIndex>();
            var list =await ipQuery.ListAsync();
            return View(list);
        }
        // GET: Survey/AddStation    
        public ActionResult AddStation()
        {
            return View();
        }

        // POST: Survey/AddStation    
        [HttpPost]
        public ActionResult AddStation(IpModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    model.Id = 0;
                    model.CreateDate = DateTime.Now;
                    _session.Save(new IpModel { Ip=model.Ip, Station=model.Station,CreateDate = model.CreateDate , CreateBy= User.Identity.Name});

                    //if (ipQuery.(Emp))
                    //{
                    ViewBag.Message = "Station added successfully";
                    //}
                //}

                return View();
            }
            catch
            {
                return View();
            }
        }
        // GET: Survey/EditStation    
        public async Task<ActionResult> EditStation(string ip)
        {
            var ipQuery = _session.Query<IpModel, IpIndex>();
            if (ipQuery != null)
            {
               var model = await ipQuery.Where(c=>c.Ip == ip).ListAsync();
                if (model != null && model.Count() > 0)
                {
                    return View(model.First());
                }
            }    
            return View();
        }

        // POST: Survey/EditStation    
        [HttpPost]
        public ActionResult EditStation(IpModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    model.CreateDate = DateTime.Now;
                    _session.Save(model);

                    //if (ipQuery.(Emp))
                    //{
                    ViewBag.Message = "Station added successfully";
                    //}
                //}

                return View();
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> All(string fromDte, string toDte,string fromTm ,string toTm, string User)
        {
            //var userService = ShellScope.Services.GetService<IUserService>();

            DateTime? FromDate = string.IsNullOrEmpty(fromDte) ? null : DateTime.ParseExact(fromDte, "yyyy-MM-dd", null);
            DateTime? ToDate = string.IsNullOrEmpty(toDte) ? null : DateTime.ParseExact(toDte, "yyyy-MM-dd", null);
            TimeSpan time;
            TimeSpan timeTo;
            if (TimeSpan.TryParse(fromTm, out time))
            {
                // handle validation error
                if(FromDate.HasValue)
                    FromDate = FromDate.Value.Add(time);
            }
            if (TimeSpan.TryParse(toTm, out timeTo))
            {
                // handle validation error
                if (ToDate.HasValue)
                    ToDate = ToDate.Value.Add(timeTo);
            }
            var surveyQuery = _session.Query<SurveyModel, SurveyIndex>();
           var dataList = await surveyQuery.ListAsync();
           if (FromDate != null & FromDate.HasValue )
               dataList = dataList.Where(x => x.CreateDate >= FromDate.Value);
            if (ToDate != null & ToDate.HasValue)
                dataList = dataList.Where(x => x.CreateDate <= ToDate.Value);
            if (User != null) dataList = dataList.Where(x => x.User == User);
            return Json(new { result = dataList });
        }

        public async Task<ActionResult> Export(string fromDte, string toDte, string fromTm, string toTm, string User)
        {
            //var userService = ShellScope.Services.GetService<IUserService>();

            DateTime? FromDate = string.IsNullOrEmpty(fromDte) ? null : DateTime.ParseExact(fromDte, "yyyy-MM-dd", null);
            DateTime? ToDate = string.IsNullOrEmpty(toDte) ? null : DateTime.ParseExact(toDte, "yyyy-MM-dd", null);
            TimeSpan time;
            TimeSpan timeTo;
            if (TimeSpan.TryParse(fromTm, out time))
            {
                // handle validation error
                if (FromDate.HasValue)
                    FromDate = FromDate.Value.Add(time);
            }
            if (TimeSpan.TryParse(toTm, out timeTo))
            {
                // handle validation error
                if (ToDate.HasValue)
                    ToDate = ToDate.Value.Add(timeTo);
            }
            var surveyQuery = _session.Query<SurveyModel, SurveyIndex>();
            DataTable tb = new DataTable();
            tb.Columns.Add("ลำดับ");
            tb.Columns.Add("ดี");
            tb.Columns.Add("ปานกลาง");
            tb.Columns.Add("ควรปรับปรุง");
            tb.Columns.Add("จุดบริการ");
            tb.Columns.Add("รหัสพนักงาน");
            tb.Columns.Add("วันที่ประเมิน",typeof(DateTime));
            tb.Columns.Add("เวลาประเมิน");
            var dataList = await surveyQuery.ListAsync();
            if (FromDate != null)
                dataList = dataList.Where(x => x.CreateDate >= FromDate);
            if (ToDate != null)
                dataList = dataList.Where(x => x.CreateDate <= ToDate);
            if (User != null) dataList = dataList.Where(x => x.User == User);
            dataList = dataList.OrderByDescending(c => c.CreateDate);
        
            foreach (var item in dataList)
            {
                var rw = tb.NewRow();
                rw[0] = item.Id;
                rw[1] = item.Good;
                rw[2] = item.Fair;
                rw[3] = item.Unsatisfy;
                rw[4] = item.Station;
                rw[5] = item.User;
                rw[6] = item.CreateDate.Date;
                rw[7] = item.CreateDate.ToString("HH:mm");
                tb.Rows.Add(rw);
            }
            return File(ExcelHelper.Export(tb, "แบบสอบถาม"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Export_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xlsx");
        }

        public async Task<ActionResult> ExportSummary(string fromDte, string toDte, string fromTm, string toTm, string User)
        {
            DateTime? FromDate = string.IsNullOrEmpty(fromDte) ? null : DateTime.ParseExact(fromDte, "yyyy-MM-dd", null);
            DateTime? ToDate = string.IsNullOrEmpty(toDte) ? null : DateTime.ParseExact(toDte, "yyyy-MM-dd", null);
            TimeSpan time;
            TimeSpan timeTo;
            if (TimeSpan.TryParse(fromTm, out time))
            {
                // handle validation error
                if (FromDate.HasValue)
                    FromDate = FromDate.Value.Add(time);
            }
            if (TimeSpan.TryParse(toTm, out timeTo))
            {
                // handle validation error
                if (ToDate.HasValue)
                    ToDate = ToDate.Value.Add(timeTo);
            }
            var surveyQuery = _session.Query<SurveyModel, SurveyIndex>();

            try
            {
                DataTable tb = new DataTable();

                tb.Columns.Add("ดี");
                tb.Columns.Add("ปานกลาง");
                tb.Columns.Add("ควรปรับปรุง");
                tb.Columns.Add("จุดบริการ");
                tb.Columns.Add("รหัสพนักงาน");
                tb.Columns.Add("เวลาประเมิน", typeof(DateTime));
                var dataList = await surveyQuery.ListAsync();
                if (FromDate != null)
                    dataList = dataList.Where(x => x.CreateDate >= FromDate);
                if (ToDate != null)
                    dataList = dataList.Where(x => x.CreateDate <= ToDate);
                if (!string.IsNullOrEmpty(User)) dataList = dataList.Where(x => x.User == User);
                dataList = dataList.OrderByDescending(c => c.CreateDate);
                var result = dataList.Select(x => new SurveyGroupIndex { CreateDte = x.CreateDate.Date, Fair = x.Fair, Good = x.Good, Unsatisfy = x.Unsatisfy, User = x.User, Station = x.Station }).GroupBy(x => new { x.CreateDte, x.User, x.Station }).Select(c => new { CreateDte = c.Key.CreateDte, c.Key.User, Station = c.Key.Station, Good = c.Sum(t => t.Good ? 1 : 0), Fair = c.Sum(t => t.Fair ? 1 : 0), Unsatisfy = c.Sum(t => t.Unsatisfy ? 1 : 0) });

                foreach (var item in result)
                {
                    var rw = tb.NewRow();
                    //   rw[0] = item.Id;
                    rw[0] = item.Good;
                    rw[1] = item.Fair;
                    rw[2] = item.Unsatisfy;
                    rw[3] = item.Station;
                    rw[4] = item.User;
                    rw[5] = item.CreateDte;
                    tb.Rows.Add(rw);
                }
                return File(ExcelHelper.Export(tb, "แบบสอบถาม สรุป"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportSummary_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xlsx");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<ActionResult> UserAll()
        {
            var users = await _session.Query<User, UserIndex>().ListAsync();
            List<string> list = new List<string>();//=  users.Result.Select(c => c.UserName).ToList();
            if (users != null && users.Count() > 0)
            {
                list = users.Select(c => c.UserName).ToList();
            }
            return Json(new { result = list });
        }

        public async Task<ActionResult> DeleteData()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var surveyQuery = _session.Query<SurveyModel, SurveyIndex>();
            var dataQuery = await surveyQuery.Where(c=>c.CreateDate < firstDayOfMonth).ListAsync();
            int record =dataQuery.Count();
            if (record > 0)
            {
                foreach (var item in dataQuery)
                {
                    _session.Delete(item);
                }

                return Json(new { result = "ลบข้อมูลสำเร็จ!",record });
            }
            return Json(new { result = "ไม่ได้ลบข้อมูล", record });
        }
    }
}
