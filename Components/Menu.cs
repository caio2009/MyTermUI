using MyUILib.Components;
using MyUILib.Components.Interfaces;

namespace MyUILib;

public class Menu : Component, IContainer, ISwitchableComponent
{
    public List<Component> Children { get; set; } = new List<Component>();
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
            this.Children.Add(new MenuItem(
                index, 
                menuItem, 
                (int index) => { if (onSelectedCb is not null) onSelectedCb(index); }
            ));
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
}
