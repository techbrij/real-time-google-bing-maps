using System.Web;
using System.Web.Mvc;

namespace Real_Time_Map_TechBrij
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}