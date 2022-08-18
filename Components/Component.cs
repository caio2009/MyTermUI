namespace MyUILib.Components;

public abstract class Component
{
    public string? Id = "";
    public int PosX;
    public int PosY;

    public Component()
    {
    }

    public Component(string? id)
    {
        this.Id = id;
    }

    public abstract void Render();
}
