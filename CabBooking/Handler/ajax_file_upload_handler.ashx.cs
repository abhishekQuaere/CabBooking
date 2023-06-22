using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CabBooking.Handler
{
    /// <summary>
    /// Summary description for ajax_file_upload_handler
    /// </summary>
    public class ajax_file_upload_handler : IHttpHandler
    {
        Common Obj = new Common();

        [HttpPost]
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        string fname;
                        string flname = "";
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            flname = Obj.GetUniqueFileName(file.FileName);
                            fname = flname;
                        }
                        fname = Path.Combine(context.Server.MapPath("/assets/FileUpload/AdharImageBack/"), fname);
                        file.SaveAs(fname);
                        context.Response.ContentType = "text/plain";
                        string path = Obj.getImagePath();
                        context.Response.Write(path + "AdharImageBack/" + flname);

                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}