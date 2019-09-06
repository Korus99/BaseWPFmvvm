using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWpfMvvm_App.Common.BaseClass;

namespace BaseWpfMvvm_App.Common.AppModel
{
    public class AppMenuData :ViewModelBase
    {
        private bool _isHorizontal;
        private List<AppMenuItem> _appMenuItems;

        public AppMenuData()
        {
            AppMenuItems = new List<AppMenuItem>();
            IsHorizontal = true;
        }
        public bool IsHorizontal
        {
            get => _isHorizontal;
            set => SetProperty(ref _isHorizontal, value);
        }

        public List<AppMenuItem> AppMenuItems
        {
            get => _appMenuItems;
            set => SetProperty(ref _appMenuItems, value);
        }

        public void Initialize(List<AppMenuItem> menuItems)
        {
            AppMenuItems.Clear();
            foreach (var appMenuItem in menuItems)
            {
                AppMenuItems.Add(new AppMenuItem
                {
                    Id = appMenuItem.Id,
                    DisplayOrder = appMenuItem.DisplayOrder,
                    HasSetupError = appMenuItem.HasSetupError,
                    IsCheckable = appMenuItem.IsCheckable,
                    Name = appMenuItem.Name,
                    ParentId = appMenuItem.ParentId

                });
            }

        }
    }
}
