using CabBooking.DbRepository;
using CabBooking.Models;
using CabBooking.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PharmacyApplication.Filters
{
    public class AuthorizeAdmin : AuthorizeAttribute
    {
        SessionManager SM = new SessionManager();
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var rd = HttpContext.Current.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            bool isValidUser = false;

            if (filterContext.HttpContext.Request.UrlReferrer == null || filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
            {
                isValidUser = true;

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {

                }
                //else
                //{
                //    SM.UserExceptionSession = "URL Tempering is not allowed Please do not refresh or try to edit Url.";
                //    filterContext.Result = new RedirectToRouteResult(
                //                       new RouteValueDictionary
                //           {
                //                       { "action", "Logout" },
                //                       { "controller", "Account" }
                //           });
                //}
            }

            if (SM.userId != 0 || SM.roleName == "Admin")
            {
                isValidUser = true;
            }
            else
            {
                isValidUser = true;

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new EmptyResult();
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.End();
                }
                else
                { 
                    SM.UserExceptionSession = "Your session has expired and you have been Logged Out.";
                    filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                           {
                                       { "action", "index" },
                                       { "controller", "home" }
                           });
                }
            }

            if (!isValidUser)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new EmptyResult();
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.End();
                }
                else
                { 
                    SM.UserExceptionSession = "Your are not authorized.";
                    filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                           {
                                       { "action", "index" },
                                       { "controller", "home" }
                           });
                }
            }
        }
    }
}