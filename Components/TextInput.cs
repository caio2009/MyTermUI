using MyUILib.Components.Interfaces;

namespace MyUILib.Components;

public class TextInput : Component, ISwitchableComponent
{
    public string Value { get; set; } = "";
    public Component? Next { get; set; }
    public Component? Previous { get; set; }

    public TextInput() : base()
    {
    }

    public TextInput(string value, string? id) : base(id)
    {
        this.Value = value;
    }

    public override void Render()
    {
        var cursorPosition = Console.GetCursorPosition();
        this.PosX = cursorPosition.Left;
        this.PosY = cursorPosition.Top;

        if (String.IsNullOrEmpty(this.Value)) 
        {
            Console.Write("");
            return;
        }

        Console.Write(this.Value);
    }

    public void Backspace()
    {
        if (this.Value.Length > 0) this.Value = this.Value.Substring(0, this.Value.Length - 1);
    }

    public void AddChar(char ch)
    {
        this.Value += ch;
    }
}
