using MyUILib.Components;

namespace MyUILib.Handlers;

public class TextInputHandler
{
    public static void Handle(ConsoleKeyInfo keyInfo, Component component, Page page)
    {
        var input = (TextInput)component;

        if (keyInfo.Key == ConsoleKey.Backspace)
        {
            input.Backspace();
            return;
        }

        if ( keyInfo.Key == ConsoleKey.Tab & keyInfo.Modifiers == ConsoleModifiers.Shift )
        {
            page.SwitchToPreviousComponent();
            return;
        }

        if (keyInfo.Key == ConsoleKey.Enter | keyInfo.Key == ConsoleKey.Tab)
        {
            page.SwitchToNextComponent();
            return;
        }

        input.AddChar(keyInfo.KeyChar);
    }
}
