using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ExamenWeb.Helper
{
    public static class MyHelper
    {
        public static string UploadFile(HttpPostedFileBase file, int count)
        {
            string filename = count + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
                   + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString()
                   + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".png";
            string path = Path.Combine(HostingEnvironment.MapPath("~/Uploads"), filename);

            file.SaveAs(path);
            return filename;
        }
    }

}