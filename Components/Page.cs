using MyUILib.Components.Interfaces;

namespace MyUILib.Components;

public class Page : IContainer
{
    public List<Component> Children { get; set; } = new List<Component>();
    public Component? ActiveComponent { get; set; }

    // Navigation Linked List
    public ISwitchableComponent? LastSwitchableComponent { get; set; } = null;

    // --------------------------------------------------------------------------------

    public void Render()
    {
        Console.Clear();

        foreach (var child in this.Children)
        {
            child.Render();
        }
    }

    public void Clear()
    {
        this.Children.Clear();
        this.LastSwitchableComponent = null;
        this.ActiveComponent = null;
        Console.SetCursorPosition(0, 0);
    }

    public void SetCursorPosition()
    {
        if (this.ActiveComponent is not null)
        {
            if (this.ActiveComponent is TextInput)
            {
                var activeComponent = (TextInput)this.ActiveComponent;
                Console.SetCursorPosition(activeComponent.PosX + activeComponent.Value.Length, activeComponent.PosY);
            }
            else
            {
                var activeComponent = this.ActiveComponent;
                Console.SetCursorPosition(activeComponent.PosX, activeComponent.PosY);
            }
        }
    }

    public Page Add(Component component)
    {
        this.Children.Add(component);
        if (component is ISwitchableComponent) AddSwitchableComponent((ISwitchableComponent)component);
        return this;
    }

    private void AddSwitchableComponent(ISwitchableComponent component)
    {
        if (this.LastSwitchableComponent is null)
        {
            this.LastSwitchableComponent = component;
            component.Next = this.LastSwitchableComponent;
            component.Previous = this.LastSwitchableComponent;
        }
        else
        {
            component.Next = this.LastSwitchableComponent.Next;
            component.Previous = this.LastSwitchableComponent;
            this.LastSwitchableComponent.Next.Previous = component;
            this.LastSwitchableComponent.Next = component;
            this.LastSwitchableComponent = component;
        }
    }

    public void Remove(string componentId)
    {
        this.Children.Remove(this.Children.First(c => c.Id.Equals(componentId)));
    }

    public void SwitchToNextComponent(BaseContainer? container = null, bool keepInContainer = false)
    {
        if (this.ActiveComponent is null) return;

        Component? nextComponent = null;

        if (container is not null)
        {
            container.SetNextComponent();
            nextComponent = (Component)container.ActiveComponent;
        }
        else
        {
            var activeComponent = this.ActiveComponent;
            if (activeComponent is IChildComponent & keepInContainer == false)
            {
                var parent = ((IChildComponent)activeComponent).Parent;
                nextComponent = (Component)((ISwitchableComponent)parent).Next;
                parent.ActiveComponent = null;
            }
            else
            {
                nextComponent = (Component)((ISwitchableComponent)activeComponent).Next;
            }
        }

        if (nextComponent is BaseContainer)
        {
            SwitchToNextComponent((BaseContainer)nextComponent);
        }
        else
        {
            this.ActiveComponent = nextComponent;

            if (this.ActiveComponent is IIsSelectableComponent)
            {
                ((IIsSelectableComponent)this.ActiveComponent).IsSelected = true;
            }
        }
    }

    public void SwitchToPreviousComponent(BaseContainer? container = null, bool keepInContainer = false)
    {
        if (this.ActiveComponent is null) return;

        Component? previousComponent = null;

        if (container is not null)
        {
            container.SetPreviousComponent();
            previousComponent = (Component)container.ActiveComponent;
        }
        else
        {
            var activeComponent = this.ActiveComponent;
            if (activeComponent is IChildComponent & keepInContainer == false)
            {
                var parent = ((IChildComponent)activeComponent).Parent;
                previousComponent = (Component)((ISwitchableComponent)parent).Previous;
                parent.ActiveComponent = null;
            }
            else
            {
                previousComponent = (Component)((ISwitchableComponent)activeComponent).Previous;
            }
        }

        if (previousComponent is BaseContainer)
        {
            SwitchToPreviousComponent((BaseContainer)previousComponent);
        }
        else
        {
            this.ActiveComponent = previousComponent;

            if (this.ActiveComponent is IIsSelectableComponent)
            {
                ((IIsSelectableComponent)this.ActiveComponent).IsSelected = true;
            }
        }
    }

    public void SetDefaultActiveComponent()
    {
        if (this.ActiveComponent is null & this.LastSwitchableComponent.Next is not null)
        {
            this.ActiveComponent = (Component)this.LastSwitchableComponent.Next;

            if (this.ActiveComponent is IIsSelectableComponent)
            {
                ((IIsSelectableComponent)this.ActiveComponent).IsSelected = true;
            }
        }
    }
}
