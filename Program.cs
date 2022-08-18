using MyUILib.Components;

namespace MyUILib;

class Program
{
    static void Main(string[] args)
    {
        var app = new Application(new Page());
        app.Run();
    }
}
