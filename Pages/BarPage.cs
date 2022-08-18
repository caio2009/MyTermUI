using MyUILib.Components;
using MyUILib.Utils;

namespace MyUILib.Pages;

public class BarPage
{
    public static void Load(Page page)
    {
        page.Add(UI.TextLabel("Bar Page"))
            .Add(UI.BR())
            .Add(UI.BR())
            .Add(UI.Button("Previous", cb: (Page page) => { Application.CurrentRoute = "FooPage"; }));
    }
}
