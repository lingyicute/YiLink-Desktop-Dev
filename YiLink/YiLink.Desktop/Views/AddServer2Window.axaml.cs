using System.Reactive.Disposables;
using Avalonia.Interactivity;
using ReactiveUI;
using YiLink.Desktop.Base;
using YiLink.Desktop.Common;

namespace YiLink.Desktop.Views;

public partial class AddServer2Window : WindowBase<AddServer2ViewModel>
{
    public AddServer2Window()
    {
        InitializeComponent();
    }

    public AddServer2Window(ProfileItem profileItem)
    {
        InitializeComponent();

        this.Loaded += Window_Loaded;
        btnCancel.Click += (s, e) => this.Close();
        ViewModel = new AddServer2ViewModel(profileItem, UpdateViewHandler);

        foreach (ECoreType it in Enum.GetValues(typeof(ECoreType)))
        {
            if (it == ECoreType.YiLink)
                continue;
            cmbCoreType.Items.Add(it.ToString());
        }
        cmbCoreType.Items.Add(string.Empty);

        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.SelectedSource.Remarks, v => v.txtRemarks.Text).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.SelectedSource.Address, v => v.txtAddress.Text).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.CoreType, v => v.cmbCoreType.SelectedValue).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.SelectedSource.DisplayLog, v => v.togDisplayLog.IsChecked).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.SelectedSource.PreSocksPort, v => v.txtPreSocksPort.Text).DisposeWith(disposables);

            this.BindCommand(ViewModel, vm => vm.BrowseServerCmd, v => v.btnBrowse).DisposeWith(disposables);
            this.BindCommand(ViewModel, vm => vm.EditServerCmd, v => v.btnEdit).DisposeWith(disposables);
            this.BindCommand(ViewModel, vm => vm.SaveServerCmd, v => v.btnSave).DisposeWith(disposables);
        });
    }

    private async Task<bool> UpdateViewHandler(EViewAction action, object? obj)
    {
        switch (action)
        {
            case EViewAction.CloseWindow:
                this.Close(true);
                break;

            case EViewAction.BrowseServer:
                var fileName = await UI.OpenFileDialog(this, null);
                if (fileName.IsNullOrEmpty())
                {
                    return false;
                }
                ViewModel?.BrowseServer(fileName);
                break;
        }

        return await Task.FromResult(true);
    }

    private void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        txtRemarks.Focus();
    }
}
