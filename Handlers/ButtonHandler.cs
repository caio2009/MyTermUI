using MyUILib.Components;

namespace MyUILib.Handlers;

public class ButtonHandler
{
    public static void Handle(ConsoleKeyInfo keyInfo, Component component, Page page)
    {
        var button = (Button)component;

        if (keyInfo.Key == ConsoleKey.Enter)
        {
            button.EmitClickEvent(page);
            return;
        }

        if (keyInfo.Key == ConsoleKey.Tab & keyInfo.Modifiers == ConsoleModifiers.Shift)
        {
            page.SwitchToPreviousComponent();
            return;
        }

        if (keyInfo.Key == ConsoleKey.Tab)
        {
            page.SwitchToNextComponent();
            return;
        }
    }
}
