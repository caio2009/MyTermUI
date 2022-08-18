using MyUILib.Components.Interfaces;

namespace MyUILib.Components;

public class Button : Component, ISwitchableComponent
{
    public string Text { get; set; } = "";
    public Action<Page>? OnClickCb { get; set; }
    public Component? Next { get; set; }
    public Component? Previous { get; set; }

    public Button() : base()
    {
    }

    public Button(string text, string? id, Action<Page>? cb) : base(id)
    {
        this.Text = text;
        this.OnClickCb = cb;
    }

    public override void Render()
    {
        var cursorPosition = Console.GetCursorPosition();
        this.PosX = cursorPosition.Left;
        this.PosY = cursorPosition.Top;

        if (String.IsNullOrEmpty(this.Text))
        {
            Console.Write("");
        }
        else
        {
            Console.Write($"[{this.Text.ToUpper()}]");
            Console.Write(" ");
        }
    }

    public void EmitClickEvent(Page page)
    {
        if (this.OnClickCb is not null) this.OnClickCb(page);
    }
}
