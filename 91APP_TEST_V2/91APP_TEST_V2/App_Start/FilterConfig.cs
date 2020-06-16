using System.Web;
using System.Web.Mvc;
using _91APP_TEST_V2.Filter;

namespace _91APP_TEST_V2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new LoginAuthorizeAttribute());
        }
    }
}
