using System.Diagnostics;

namespace AmazTool;

internal class Utils
{
    public static string GetExePath()
    {
        return Environment.ProcessPath ?? Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty;
    }

    public static string StartupPath()
    {
        return AppDomain.CurrentDomain.BaseDirectory;
    }

    public static string GetPath(string fileName)
    {
        var startupPath = StartupPath();
        if (string.IsNullOrEmpty(fileName))
        {
            return startupPath;
        }
        return Path.Combine(startupPath, fileName);
    }

    public static string YiLink => "YiLink";

    public static void StartYiLink()
    {
        Process process = new()
        {
            StartInfo = new()
            {
                UseShellExecute = true,
                FileName = YiLink,
                WorkingDirectory = StartupPath()
            }
        };
        process.Start();
    }

    public static void Waiting(int second)
    {
        for (var i = second; i > 0; i--)
        {
            Console.WriteLine(i);
            Thread.Sleep(1000);
        }
    }
}
