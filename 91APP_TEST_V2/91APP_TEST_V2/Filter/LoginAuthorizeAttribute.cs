using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _91APP_TEST_V2.Filter
{
    /// <summary>
    /// 授權過濾器
    /// </summary>
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //判斷是否跳過授權過濾器
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
               || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            //判斷登入情況
            if (filterContext.HttpContext.Session["account"] == null || filterContext.HttpContext.Session["account"].ToString() == "")
            {
                //HttpContext.Current.Response.Write("認證不通過");
                //HttpContext.Current.Response.End();

                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}