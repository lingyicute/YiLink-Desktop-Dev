using Avalonia;
using Avalonia.Media;

namespace YiLink.Desktop.Common;

public static class AppBuilderExtension
{
    public static AppBuilder WithFontByDefault(this AppBuilder appBuilder)
    {
        var uri = Path.Combine(Global.AvaAssets, "Fonts#Noto Sans SC");
        return appBuilder.With(new FontManagerOptions()
        {
            //DefaultFamilyName = uri,
            FontFallbacks = new[] { new FontFallback { FontFamily = new FontFamily(uri) } }
        });
    }
}
