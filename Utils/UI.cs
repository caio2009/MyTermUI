using MyUILib.Components;

namespace MyUILib.Utils;

public class UI
{
    public static BR BR()
    {
        return new BR();
    }

    public static TextLabel TextLabel(string text, bool breakline = false)
    {
        return new TextLabel(text, breakline);
    }

    public static TextInput TextInput(string value, string? id = null)
    {
        return new TextInput(value, id);
    }

    public static Button Button(string text, string? id = null, Action<Page>? cb = null)
    {
        return new Button(text, id, cb);
    }

    public static Menu Menu(string[] menuItems, string? id = null, Action<int>? cb = null)
    {
        return new Menu(menuItems, id, cb);
    }

    public static Table Table(CellData[]? header = null, List<CellData[]>? rows = null, string? id = null, Action<int>? cb = null)
    {
        return new Table(header, rows, id, cb);
    }

    // Horizontal Row
    public static TextLabel HR()
    {
        return new TextLabel(new string('-', Console.WindowWidth));
    }
}
