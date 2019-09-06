using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWpfMvvm_App.Common.BaseClass;
using BaseWpfMvvm_App.Common.Event;
using BaseWpfMvvm_App.Common.Framework;
using BaseWpfMvvm_App.Common.Menu;
using static  BaseWpfMvvm_App.Common.AppConstant.AppConstant;

namespace BaseWpfMvvm_App
{
    public class MainWindowVm :ViewModelBase
    {
        private MenuVm _menuVm;
        private MenuVm _selectedMenu;
        private bool _controlsEnabled;
        private FrameworkVm _frameworkVm;
        private ViewModelBase _selectedFramework;
        private bool _helpDialogVisible;
        private string _helpMessage;

        public MainWindowVm()
        {
            _frameworkVm = new FrameworkVm();
            SelectedFramework = _frameworkVm;
            MenuVm = new MenuVm();
            MenuVm.SetMenuData();

            SelectedMenu = MenuVm;
            ControlsEnabled = true;
            _menuVm.ExecuteMenuNavigate += MenuVmNavigate;

            HelpMessage = "App help message!";


        }

        public bool ControlsEnabled
        {
            get => _controlsEnabled;
            set => SetProperty(ref _controlsEnabled, value);
        }

        public FrameworkVm FrameworkVm
        {
            get => _frameworkVm;
            set => SetProperty(ref _frameworkVm, value);
        }

        public ViewModelBase SelectedFramework
        {
            get => _selectedFramework;
            set => SetProperty(ref _selectedFramework, value);
        }
        public MenuVm MenuVm
        {
            get => _menuVm;
            set => SetProperty(ref _menuVm, value);
        }

        public MenuVm SelectedMenu
        {
            get => _selectedMenu;
            set => SetProperty(ref _selectedMenu, value);
        }

        public bool HelpDialogVisible
        {
            get => _helpDialogVisible;
            set => SetProperty(ref _helpDialogVisible, value);
        }

        public string HelpMessage
        {
            get => _helpMessage;
            set => SetProperty(ref _helpMessage, value);
        }

        public void Navigate(NavigateEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NavigationString))
            {
            }
            else
            {
                switch (e.NavigationString)
                {
                    case MenuExit:
                        Environment.Exit(0);
                        break;
                    case MenuHelp:
                        ShowHelp();
                        break;
                    default:

                        FrameworkVm.Navigate(e.NavigationString);
                        break;
                }
            }

        }

        private void ShowHelp()
        {
            HelpDialogVisible = false;
            HelpDialogVisible = true;
        }
        private void MenuVmNavigate(object sender, NavigateEventArgs e)
        {
            Navigate(e);
        }

    }
}
