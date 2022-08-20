using MyUILib.Components;
using MyUILib.Components.Interfaces;

namespace MyUILib;

public class MenuItem : Component, ISwitchableComponent, IIsSelectableComponent
{
    public string? Text { get; set; }
    public int Index { get; set; }
    public bool IsSelected { get; set; }
    public Action<int>? OnClickCb { get; set; }
    public ISwitchableComponent? Next { get; set; }
    public ISwitchableComponent? Previous { get; set; }

    public MenuItem() : base()
    {
    }

    public MenuItem(int index, string text, Action<int> onClickCb) : base()
    {
        this.Index = index;
        this.Text = text;
        this.OnClickCb = onClickCb;
    }

    public override void Render()
    {
        var cursorPosition = Console.GetCursorPosition();
        this.PosX = cursorPosition.Left;
        this.PosY = cursorPosition.Top;

        if (this.IsSelected)
        {
            Console.WriteLine($"> {this.Text}");
        }
        else
        {
            Console.WriteLine($"  {this.Text}");
        }
    }

    public void EmitClickEvent() 
    {
        if (this.OnClickCb is not null) this.OnClickCb(this.Index);
    }
}
