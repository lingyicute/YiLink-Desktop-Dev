using Avalonia;
using Avalonia.ReactiveUI;
using YiLink.Desktop.Common;

namespace YiLink.Desktop;

internal class Program
{
    public static EventWaitHandle ProgramStarted;

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        if (OnStartup(args) == false)
        {
            Environment.Exit(0);
            return;
        }

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    private static bool OnStartup(string[]? Args)
    {
        if (Utils.IsWindows())
        {
            var exePathKey = Utils.GetMd5(Utils.GetExePath());
            var rebootas = (Args ?? []).Any(t => t == Global.RebootAs);
            ProgramStarted = new EventWaitHandle(false, EventResetMode.AutoReset, exePathKey, out var bCreatedNew);
            if (!rebootas && !bCreatedNew)
            {
                ProgramStarted.Set();
                return false;
            }
        }
        else
        {
            _ = new Mutex(true, "YiLink", out var bOnlyOneInstance);
            if (!bOnlyOneInstance)
            {
                return false;
            }
        }

        if (!AppHandler.Instance.InitApp())
        {
            return false;
        }
        return true;
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            //.WithInterFont()
            .WithFontByDefault()
            .LogToTrace()
#if OS_OSX
            .UseReactiveUI()
            .With(new MacOSPlatformOptions { ShowInDock = AppHandler.Instance.Config.UiItem.MacOSShowInDock });
#else
            .UseReactiveUI();
#endif
    }
}
