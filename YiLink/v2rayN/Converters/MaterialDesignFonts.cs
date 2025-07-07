using System.Windows.Media;

namespace YiLink.Converters;

public class MaterialDesignFonts
{
    public static FontFamily MyFont { get; }

    static MaterialDesignFonts()
    {
        try
        {
            var fontFamily = AppHandler.Instance.Config.UiItem.CurrentFontFamily;
            if (fontFamily.IsNotEmpty())
            {
                var fontPath = Utils.GetFontsPath();
                MyFont = new FontFamily(new Uri(@$"file:///{fontPath}\"), $"./#{fontFamily}");
            }
        }
        catch
        {
        }
        MyFont ??= new FontFamily("Microsoft YaHei");
    }
}
