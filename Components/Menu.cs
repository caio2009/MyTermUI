using MyUILib.Components;
using MyUILib.Components.Interfaces;

namespace MyUILib;

public class Menu : BaseContainer, ISwitchableComponent
{
    public ISwitchableComponent? Next { get; set; }
    public ISwitchableComponent? Previous { get; set; }

    public Menu() : base()
    {
    }

    public Menu(string[] menuItems, string? id, Action<int>? onSelectedCb) : base(id)
    {
        var index = 0;
        foreach (var menuItem in menuItems) 
        {
            var child = new MenuItem(this);
            child.Index = index;
            child.Text = menuItem;
            child.OnClickCb = (int index) => { if (onSelectedCb is not null) { onSelectedCb(index); } };

            AddMenuItem(child);
            index++;
        }
    }

    public override void Render()
    {
        foreach(var child in this.Children)
        {
            child.Render();
        }
    }

    private void AddMenuItem(MenuItem menuItem)
    {
        this.Children.Add(menuItem);
        AddSwitchableComponent(menuItem);
    }

    private void AddSwitchableComponent(MenuItem menuItem)
    {
        if (this.LastSwitchableComponent is null)
        {
            this.LastSwitchableComponent = menuItem;
            menuItem.Next = this.LastSwitchableComponent;
            menuItem.Previous = this.LastSwitchableComponent;
        }
        else
        {
            menuItem.Next = this.LastSwitchableComponent.Next;
            menuItem.Previous = this.LastSwitchableComponent;
            this.LastSwitchableComponent.Next.Previous = menuItem;
            this.LastSwitchableComponent.Next = menuItem;
            this.LastSwitchableComponent = menuItem;
        }
    }
}
