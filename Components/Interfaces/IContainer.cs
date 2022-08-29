namespace MyUILib.Components.Interfaces;

public interface IContainer
{
    public List<Component> Children { get; set; }
    public Component? ActiveComponent { get; set; }
    public ISwitchableComponent? LastSwitchableComponent { get; set; }
}
