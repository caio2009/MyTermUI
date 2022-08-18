namespace MyUILib.Components;

public class TextLabel : Component
{
    public string Text { get; set; } = "";
    public bool Breakline { get; set; } = false;

    public TextLabel() : base()
    {
    }

    public TextLabel(string text, bool breakline = false) : base()
    {
        this.Text = text;
        this.Breakline = breakline;
    }

    public override void Render()
    {
        var cursorPosition = Console.GetCursorPosition();
        this.PosX = cursorPosition.Left;
        this.PosY = cursorPosition.Top;

        if (this.Breakline)
        {
            Console.WriteLine(this.Text);
        }
        else
        {
            Console.Write(this.Text);
        }
    }
}
