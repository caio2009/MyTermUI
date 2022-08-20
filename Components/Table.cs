using MyUILib.Components.Interfaces;

namespace MyUILib.Components;

public struct CellData
{
    public string Value;
    public int? Size;

    public CellData(string value, int? size = null)
    {
        this.Value = value;
        this.Size = size;
    }
}

public class Table : Component, IContainer, ISwitchableComponent
{
    public List<Component> Children { get; set; } = new List<Component>();
    public CellData[]? Header { get; set; }
    public Action<int>? OnSelectedCb { get; set; }
    public ISwitchableComponent? Next { get; set; }
    public ISwitchableComponent? Previous { get; set; }

    public Table() : base()
    {
    }

    public Table(CellData[]? header, List<CellData[]>? rows, string? id, Action<int>? cb) : base(id)
    {
        this.Header = header;
        this.OnSelectedCb = cb;

        var index = 0;
        foreach (var row in rows)
        {
            this.Children.Add(new TableRow(
                index, 
                row, 
                (int index) => { if (this.OnSelectedCb is not null) this.OnSelectedCb(index); }
            ));
            index++;
        }
    }

    public override void Render()
    {
        if (this.Header is not null)
        {
            PrintRow(this.Header);
            PrintLine();
        }

        foreach (var child in this.Children)
        {
            child.Render();
        }
    }

    private void PrintRow(CellData[] row)
    {
        foreach (var item in row.Select((value, index) => new { Value = value, Index = index }).ToList())
        {
            var cd = item.Value;
            var index = item.Index;

            Console.Write(cd.Value);

            if (index < row.Length - 1)
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
    }

    private void PrintLine()
    {
        Console.Write(new string('-', Console.WindowWidth));
    }
}
