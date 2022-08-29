using MyUILib.Components;

namespace MyUILib.Handlers;

public class MenuItemHandler
{
    public static void Handle(ConsoleKeyInfo keyInfo, Component component, Page page)
    {
        var menuItem = (MenuItem)component;

        if (keyInfo.Key == ConsoleKey.Enter)
        {
            menuItem.EmitClickEvent();
            return;
        }

        if (keyInfo.Key == ConsoleKey.Tab & keyInfo.Modifiers == ConsoleModifiers.Shift)
        {
            menuItem.IsSelected = false;
            page.SwitchToPreviousComponent();
            return;
        }

        if (keyInfo.Key == ConsoleKey.Tab)
        {
            menuItem.IsSelected = false;
            page.SwitchToNextComponent();
            return;
        }

        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            menuItem.IsSelected = false;
            page.SwitchToPreviousComponent(keepInContainer: true);
            return;
        }

        if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            menuItem.IsSelected = false;
            page.SwitchToNextComponent(keepInContainer: true);
            return;
        }
    }
}
