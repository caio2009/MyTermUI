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

public class Table : BaseContainer, ISwitchableComponent
{
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
            var child = new TableRow();
            child.Parent = this;
            child.Index = index;
            child.Value = row;
            child.OnClickCb = (int index) => { if (this.OnSelectedCb is not null) this.OnSelectedCb(index); };

            AddTableRow(child);
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

    private void AddTableRow(TableRow tableRow)
    {
        this.Children.Add(tableRow);
        AddSwitchableComponent(tableRow);
    }

    private void AddSwitchableComponent(TableRow tableRow)
    {
        if (this.LastSwitchableComponent is null)
        {
            this.LastSwitchableComponent = tableRow;
            tableRow.Next = this.LastSwitchableComponent;
            tableRow.Previous = this.LastSwitchableComponent;
        }
        else
        {
            tableRow.Next = this.LastSwitchableComponent.Next;
            tableRow.Previous = this.LastSwitchableComponent;
            this.LastSwitchableComponent.Next.Previous = tableRow;
            this.LastSwitchableComponent.Next = tableRow;
            this.LastSwitchableComponent = tableRow;
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
