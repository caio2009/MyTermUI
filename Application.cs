using MyUILib.Components;
using MyUILib.Handlers;

namespace MyUILib;

public class Application
{
    public Page Page { get; set; } = new Page();
    public bool IsRunning { get; set; } = true;
    public Dictionary<string, Action<Page>> Routes { get; set; }
    public Dictionary<Type, Action<ConsoleKeyInfo, Component, Page>> Handlers { get; set; } = new Dictionary<Type, Action<ConsoleKeyInfo, Component, Page>>();

    public static string? CurrentRoute;
    public static string? PreviousRoute;

    // --------------------------------------------------------------------------------

    public Application(Page page)
    {
        this.Page = page;
        this.Routes = Router.LoadRoutes();

        this.Handlers.Add(typeof(TextInput), TextInputHandler.Handle);
        this.Handlers.Add(typeof(Button), ButtonHandler.Handle);
        this.Handlers.Add(typeof(MenuItem), MenuItemHandler.Handle);
        this.Handlers.Add(typeof(TableRow), TableRowHandler.Handle);

        Application.CurrentRoute = Router.InitialRoute;
        Application.PreviousRoute = Router.InitialRoute;
    }

    // --------------------------------------------------------------------------------

    public void Run()
    {
        LoadPageStructure(this.Page);

        while (this.IsRunning)
        {
            if (CheckIfCurrentRouteHasBeenChanged())
            {
                Application.PreviousRoute = Application.CurrentRoute;
                this.Page.Clear();
                LoadPageStructure(this.Page);
            }

            this.Page.SetDefaultActiveComponent();
            this.Page.Render();
            this.Page.SetCursorPosition();

            HandleKeyPress();
        }

        Console.Clear();
    }

    private void LoadPageStructure(Page page)
    {
        if (Application.CurrentRoute is not null)
        {
            if (this.Routes.Keys.FirstOrDefault(k => k.Equals(Application.CurrentRoute)) is null) 
                throw new Exception("Route not found.");

            this.Routes[Application.CurrentRoute](page);
        }
    }

    private bool CheckIfCurrentRouteHasBeenChanged()
    {
        if (Application.CurrentRoute is null) 
            throw new Exception("Application.CurrentRoute must be not null.");

        return !Application.CurrentRoute.Equals(Application.PreviousRoute);
    }

    private void HandleKeyPress()
    {
        var keyInfo = Console.ReadKey(true);

        if (keyInfo.Key == ConsoleKey.Escape)
        {
            this.IsRunning = false;
            return;
        }

        if (this.Page.ActiveComponent is not null)
        {
            var handlerFunc = this.Handlers[this.Page.ActiveComponent.GetType()];
            handlerFunc(keyInfo, this.Page.ActiveComponent, this.Page);
        }
    }
}
