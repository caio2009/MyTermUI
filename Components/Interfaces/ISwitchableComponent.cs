namespace MyUILib.Components.Interfaces;

public interface ISwitchableComponent
{
    public ISwitchableComponent? Next { get; set; }
    public ISwitchableComponent? Previous { get; set; }
}
