namespace MyUILib.Components.Interfaces;

public interface ISwitchableComponent
{
    public Component? Next { get; set; }
    public Component? Previous { get; set; }
}
