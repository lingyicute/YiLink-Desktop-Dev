using System.Reactive.Disposables;
using Avalonia;
using Avalonia.ReactiveUI;
using ReactiveUI;
using YiLink.Desktop.ViewModels;

namespace YiLink.Desktop.Views;

/// <summary>
/// ThemeSettingView.xaml
/// </summary>
public partial class ThemeSettingView : ReactiveUserControl<ThemeSettingViewModel>
{
    public ThemeSettingView()
    {
        InitializeComponent();
        ViewModel = new ThemeSettingViewModel();

        foreach (ETheme it in Enum.GetValues(typeof(ETheme)))
        {
            cmbCurrentTheme.Items.Add(it.ToString());
        }

        for (int i = Global.MinFontSize; i <= Global.MinFontSize + 10; i++)
        {
            cmbCurrentFontSize.Items.Add(i);
        }

        Global.Languages.ForEach(it =>
        {
            cmbCurrentLanguage.Items.Add(it);
        });

        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.CurrentTheme, v => v.cmbCurrentTheme.SelectedValue).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.CurrentFontSize, v => v.cmbCurrentFontSize.SelectedValue).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.CurrentLanguage, v => v.cmbCurrentLanguage.SelectedValue).DisposeWith(disposables);
        });
    }
}
