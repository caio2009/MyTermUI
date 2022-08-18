using MyUILib.Components;
using MyUILib.Pages;

namespace MyUILib;

public class Router
{
    public static string InitialRoute = "HomePage";

    public static Dictionary<string, Action<Page>> LoadRoutes()
    {
        var routes = new Dictionary<string, Action<Page>>();
        
        routes.Add("HomePage", HomePage.Load);
        routes.Add("FooPage", FooPage.Load);
        routes.Add("BarPage", BarPage.Load);

        return routes;
    }
}
