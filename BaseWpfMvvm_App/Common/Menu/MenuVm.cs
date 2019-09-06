using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWpfMvvm_App.Common.AppModel;
using BaseWpfMvvm_App.Common.AppService;
using BaseWpfMvvm_App.Common.BaseClass;
using BaseWpfMvvm_App.Common.Event;

namespace BaseWpfMvvm_App.Common.Menu
{
    public class MenuVm: ViewModelBase
    {
        private AppMenuData _appMenuData;
        private List<MenuItemVm> _menuItems;
        private readonly List<Action<object, NavigateEventArgs>> _menuNavigateSubscribers =
            new List<Action<object, NavigateEventArgs>>();
        private event Action<object, NavigateEventArgs> ExecuteNavigation = delegate { };

        public MenuVm()
        {
            AppMenuData = new AppMenuData();
            MenuItems = new List<MenuItemVm>();
        }

        public AppMenuData AppMenuData
        {
            get => _appMenuData;
            set => SetProperty(ref _appMenuData, value);
        }

        public List<MenuItemVm> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public void SetMenuData()
        {
            // Configure the ERT Menu data - using configured readers.
            //AppMenuData.Initialize( kioskMode);
            AppMenuData.Initialize(AppMenuService.LoadMenuData()); 
            // Setup the Menu using Menu Data.
            var tempMenuItemVms = new List<MenuItemVm>();
            // setup for 3 levels of menu items.
            foreach (var menuItemLevel1 in AppMenuData.AppMenuItems.Where(x => x.ParentId == 0).OrderBy(x => x.DisplayOrder))
            {
                var miLevel1 = new MenuItemVm(menuItemLevel1.Name);
                foreach (var menuItemLevel2 in AppMenuData.AppMenuItems.Where(x => x.ParentId == menuItemLevel1.Id)
                    .OrderBy(x => x.DisplayOrder))
                {
                    var miLevel2 = new MenuItemVm(menuItemLevel2.Name);
                    foreach (var menuItemLevel3 in AppMenuData.AppMenuItems.Where(x => x.ParentId == menuItemLevel2.Id)
                        .OrderBy(x => x.DisplayOrder))
                    {
                        var miLevel3 = new MenuItemVm(menuItemLevel3.Name);
                        miLevel3.ExecuteMenuNavigate += ExecuteMenuItemNavigation;
                        miLevel2.MenuItems.Add(miLevel3);
                    }

                    miLevel2.ExecuteMenuNavigate += ExecuteMenuItemNavigation;
                    miLevel2.HasSetupError = menuItemLevel2.HasSetupError;
                    miLevel1.MenuItems.Add(miLevel2);
                }

                miLevel1.ExecuteMenuNavigate += ExecuteMenuItemNavigation;
                tempMenuItemVms.Add(miLevel1);
            }

            MenuItems.Clear();
            MenuItems = tempMenuItemVms;
        }

        private void ExecuteMenuItemNavigation(object sender, NavigateEventArgs e)
        {
            ExecuteNavigation(sender, e);
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
