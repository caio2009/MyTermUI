using MyUILib.Components;
using MyUILib.Utils;

namespace MyUILib.Pages;

public class HomePage
{
    public static void Load(Page page)
    {
        var rows = new List<CellData[]>();

        rows.Add(new CellData[] { new CellData("Value 1", 10), new CellData("Value 2") });
        rows.Add(new CellData[] { new CellData("Value 3", 10), new CellData("Value 4") });

        page.Add(UI.TextLabel("Home Page", true))
            .Add(UI.BR())
            .Add(UI.Menu(new string[] { "Item 1", "Item 2", "Item 3" }, cb: (int index) => { Application.CurrentRoute = "BarPage"; }))
            .Add(UI.BR())
            .Add(UI.Button("Next", cb: (Page page) => { Application.CurrentRoute = "FooPage"; }))
            .Add(UI.Button("Bar Page", cb: (Page page) => { Application.CurrentRoute = "BarPage"; }))
            .Add(UI.BR())
            .Add(UI.BR())
            .Add(UI.Table(
                new CellData[] { new CellData("Header 1", 10), new CellData("Header 2", null) },
                rows,
                cb: (int index) => { Application.CurrentRoute = "FooPage"; } 
            ));
    }
}
