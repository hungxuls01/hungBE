using Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Service.Controllers
{
    public class BasePage : Page
    {
        protected override void OnInit(EventArgs e)
        {
            //if (comm.GetMemberID() < 0)
            //    Response.Redirect("/home/logintErp.aspx");

            //var url = HttpContext.Current.Request.Url.AbsolutePath;

            //List<string> listModuleForVender = new List<string>
            //{
            //    "/services/admin/Store_list.aspx",
            //    "/services/admin/Products_list.aspx",
            //    "/services/sale/orderpro_list.aspx",
            //    "/services/sale/orderprogeneralreport_list.aspx",
            //    "/services/reports/PaymentReport.aspx",
            //    "/services/admin/News_list.aspx",
            //     "/services/admin/Company_list.aspx",
            //};

            //if (comm.isSupervisor() && !comm.isSuperAdmin())
            //{
            //    if (!listModuleForVender.Contains(url))
            //    {
            //        Response.Redirect("/home/index.aspx");
            //    }

            //}
        }

        public static string Parameter
        {
            get { return HttpContext.Current.Request["extra_search"]; }
        }

        public static string Fillter
        {
            get
            {
                var requestParam = HttpUtility.ParseQueryString(Parameter);

                string json = JsonConvert.SerializeObject(requestParam.Cast<string>().ToDictionary(k => k, v => requestParam[v]));
                return json;
            }
        }

        public static int Draw
        {
            get { return ConvertUtil.ToInt32(HttpContext.Current.Request["draw"]); }
        }

        public static int Start
        {
            get { return ConvertUtil.ToInt32(HttpContext.Current.Request["start"]); }
        }

        public static int Length
        {
            get { return ConvertUtil.ToInt32(HttpContext.Current.Request["length"]); }
        }

        public static int CurrentPage
        {
            get { return Start / Length; }
        }

        public static int Order
        {
            get { return ConvertUtil.ToInt32(HttpContext.Current.Request.Params["order[0][column]"]); }
        }

        public static string OrderDir
        {
            get { return HttpContext.Current.Request.Params["order[0][dir]"]; }
        }


        public static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
    }
}