using MyUILib.Components.Interfaces;

namespace MyUILib.Components;

public class TableRow : Component, ISwitchableComponent, IIsSelectableComponent
{
    public int Index { get; set; }
    public CellData[]? Value { get; set; }
    public bool IsSelected { get; set; }
    public Action<int>? OnClickCb { get; set; }
    public ISwitchableComponent? Next { get; set; }
    public ISwitchableComponent? Previous { get; set; }

    public TableRow() : base()
    {
    }

    public TableRow(int index, CellData[] value, Action<int> cb) : base()
    {
        this.Index = index;
        this.Value = value;
        this.OnClickCb = cb;
    }

    public override void Render()
    {
        var cursorPosition = Console.GetCursorPosition();
        this.PosX = cursorPosition.Left;
        this.PosY = cursorPosition.Top;

        if (this.IsSelected)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        foreach (var item in this.Value.Select((value, index) => new { Value = value, Index = index }).ToList())
        {
            var cd = item.Value;
            var index = item.Index;

            Console.Write(cd.Value);

            if (index < this.Value.Length - 1)
            {
                var offset = cd.Size is null ? cd.Value.Length : (int)cd.Size - cd.Value.Length;
                Console.Write(new string(' ', offset));
                Console.Write(" | ");
            }
            else
            {
                var offset = Console.WindowWidth - Console.GetCursorPosition().Left;
                Console.Write(new string(' ', offset));
            }
        }

        if (this.IsSelected) Console.ResetColor();
    }

    public void EmitClickEvent() 
    {
        if (this.OnClickCb is not null) this.OnClickCb(this.Index);
    }
}
