using MyUILib.Components.Interfaces;

namespace MyUILib.Components;

public abstract class BaseContainer : Component, IContainer
{
    public List<Component> Children { get; set; } = new List<Component>();
    public Component? ActiveComponent { get; set; }
    public ISwitchableComponent? LastSwitchableComponent { get; set; }

    public BaseContainer() : base()
    {
    }

    public BaseContainer(string? id) : base(id)
    {
    }

    public void SetNextComponent()
    {
        if (this.ActiveComponent is null)
        {
            this.ActiveComponent = (Component)this.LastSwitchableComponent.Next;
        }
        else
        {
            this.ActiveComponent = (Component)((ISwitchableComponent)this.ActiveComponent).Next;
        }
    }

    public void SetPreviousComponent()
    {
        if (this.ActiveComponent is null)
        {
            this.ActiveComponent = (Component)this.LastSwitchableComponent.Previous;
        }
        else
        {
            this.ActiveComponent = (Component)((ISwitchableComponent)this.ActiveComponent).Previous;
        }
    }
}
