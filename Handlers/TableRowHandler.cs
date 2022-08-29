using MyUILib.Components;

namespace MyUILib.Handlers;

public class TableRowHandler
{
    public static void Handle(ConsoleKeyInfo keyInfo, Component component, Page page)
    {
        var tableRow = (TableRow)component;

        if (keyInfo.Key == ConsoleKey.Enter)
        {
            tableRow.EmitClickEvent();
            return;
        }

        if (keyInfo.Key == ConsoleKey.Tab & keyInfo.Modifiers == ConsoleModifiers.Shift)
        {
            tableRow.IsSelected = false;
            page.SwitchToPreviousComponent();
            return;
        }

        if (keyInfo.Key == ConsoleKey.Tab)
        {
            tableRow.IsSelected = false;
            page.SwitchToNextComponent();
            return;
        }

        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            tableRow.IsSelected = false;
            page.SwitchToPreviousComponent(keepInContainer: true);
        }

        if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            tableRow.IsSelected = false;
            page.SwitchToNextComponent(keepInContainer: true);
        }
    }
}
