using MyUILib.Components.Interfaces;

namespace MyUILib.Components;

public class Page : IContainer
{
    public List<Component> Children { get; set; } = new List<Component>();
    public List<ISwitchableComponent> SwitchableComponents { get; set; } = new List<ISwitchableComponent>();
    public Component? ActiveComponent;

    /* public List<ISwitchableComponent> SwitchableComponents */
    /* { */
    /*     get */
    /*     { */
    /*         var components = new List<ISwitchableComponent>(); */

    /*         foreach (var child in this.Children) */
    /*         { */
    /*             if (child is ISwitchableComponent) */
    /*             { */
    /*                 if (child is IContainer) */
    /*                 { */
    /*                     var container = (IContainer)child; */
    /*                     foreach (var _child in container.Children) */
    /*                     { */
    /*                         components.Add((ISwitchableComponent)_child); */
    /*                     } */
    /*                 } */
    /*                 else */
    /*                 { */
    /*                     components.Add((ISwitchableComponent)child); */
    /*                 } */
    /*             } */
    /*         } */

    /*         return components; */
    /*     } */
    /* } */

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
        this.SwitchableComponents.Clear();
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

    public void AddSwitchableComponent(ISwitchableComponent component)
    {
        if (component is IContainer)
        {
            var container = (IContainer)component;
            foreach (var child in container.Children)
            {
                this.SwitchableComponents.Add((ISwitchableComponent)child);
            }
        }
        else
        {
            this.SwitchableComponents.Add((ISwitchableComponent)component);
        }
    }

    public void Remove(string componentId)
    {
        this.Children.Remove(this.Children.First(c => c.Id.Equals(componentId)));
    }

    public void SwitchToNextComponent()
    {
        if (this.ActiveComponent is null) return;

        var ActiveComponentIndex = this.SwitchableComponents.IndexOf((ISwitchableComponent)this.ActiveComponent);

        if (ActiveComponentIndex + 1 > this.SwitchableComponents.Count - 1)
        {
            ActiveComponent = (Component)this.SwitchableComponents[0];
        }
        else
        {
            ActiveComponent = (Component)this.SwitchableComponents[ActiveComponentIndex + 1];
        }

        if (this.ActiveComponent is IIsSelectableComponent)
        {
            ((IIsSelectableComponent)this.ActiveComponent).IsSelected = true;
        }
    }

    public void SwitchToPreviousComponent()
    {
        if (this.ActiveComponent is null) return;

        var ActiveComponentIndex = this.SwitchableComponents.IndexOf((ISwitchableComponent)this.ActiveComponent);

        if (ActiveComponentIndex - 1 < 0)
        {
            ActiveComponent = (Component)this.SwitchableComponents[this.SwitchableComponents.Count - 1];
        }
        else
        {
            ActiveComponent = (Component)this.SwitchableComponents[ActiveComponentIndex - 1];
        }

        if (this.ActiveComponent is IIsSelectableComponent)
        {
            ((IIsSelectableComponent)this.ActiveComponent).IsSelected = true;
        }
    }

    public void SetDefaultActiveComponent()
    {
        if (this.SwitchableComponents.Count > 0 & this.ActiveComponent is null)
        {
            this.ActiveComponent = (Component)this.SwitchableComponents[0];

            if (this.ActiveComponent is IIsSelectableComponent)
            {
                ((IIsSelectableComponent)this.ActiveComponent).IsSelected = true;
            }
        }
    }
}
