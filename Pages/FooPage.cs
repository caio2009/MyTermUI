using MyUILib.Components;
using MyUILib.Utils;

namespace MyUILib.Pages;

public class FooPage
{
    public static void Load(Page page)
    {
        page.Add(UI.TextLabel("Foo Page"))
            .Add(UI.BR())
            .Add(UI.BR())
            .Add(UI.Button("Previous", cb: (Page page) => { Application.CurrentRoute = "HomePage"; }))
            .Add(UI.Button("Next", cb: (Page page) => { Application.CurrentRoute = "BarPage"; }));
    }
}
