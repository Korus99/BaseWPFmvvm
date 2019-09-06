using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWpfMvvm_App.Common.BaseClass;
using BaseWpfMvvm_App.Common.Command;
using BaseWpfMvvm_App.Common.Event;

namespace BaseWpfMvvm_App.Common.Menu
{
    public class MenuItemVm : ViewModelBase
    {
        private readonly List<Action<object, NavigateEventArgs>> _menuNavigateSubscribers =
            new List<Action<object, NavigateEventArgs>>();

        private bool _hasSetupError;
        private List<MenuItemVm> _menuItems;
        private string _name;

        public MenuItemVm(string name) : this()
        {
            Name = name;
        }

        public MenuItemVm()
        {
            MenuItems = new List<MenuItemVm>();
            MenuButtonClickCommand = new RelayCommand<string>(OnMenuButtonClick, CanMenuButtonClick);
        }

        public RelayCommand<string> MenuButtonClickCommand { get; set; }

        public new string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public List<MenuItemVm> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public bool HasSetupError
        {
            get => _hasSetupError;
            set => SetProperty(ref _hasSetupError, value);
        }

        private event Action<object, NavigateEventArgs> ExecuteNavigation = delegate { };

        private static bool CanMenuButtonClick(string arg)
        {
            return true;
        }

        private void OnMenuButtonClick(string menuItem)
        {
            var e = new NavigateEventArgs(menuItem);
            ExecuteNavigation(this, e);
        }

        public event Action<object, NavigateEventArgs> ExecuteMenuNavigate
        {
            add
            {
                if (_menuNavigateSubscribers.Contains(value)) return;

                ExecuteNavigation += value;
                _menuNavigateSubscribers.Add(value);
            }
            remove
            {
                ExecuteNavigation -= value;
                _menuNavigateSubscribers.Remove(value);
            }
        }
    }
}
