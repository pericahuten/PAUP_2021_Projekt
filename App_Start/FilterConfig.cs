using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
