using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWpfMvvm_App.Common.AppModel;
using static BaseWpfMvvm_App.Common.AppConstant.AppConstant;

namespace BaseWpfMvvm_App.Common.AppService
{
    public static class AppMenuService
    {
        public static List<AppMenuItem> LoadMenuData()
        {
            List<AppMenuItem> menuItems = new List<AppMenuItem>();
            // File
            menuItems.Add(new AppMenuItem
            {
                Id = 1,
                ParentId = 0,
                IsCheckable = false,
                HasSetupError = false,
                Name = MenuFile,
                DisplayOrder = 1
            });
            // File -> Exit
            menuItems.Add(new AppMenuItem
            {
                Id = 2,
                ParentId = 1,
                IsCheckable = false,
                HasSetupError = false,
                Name = MenuExit,
                DisplayOrder = 2
            });
            // Help
            menuItems.Add(new AppMenuItem
            {
                Id = 7,
                ParentId = 0,
                IsCheckable = false,
                HasSetupError = false,
                Name = MenuHelp,
                DisplayOrder = 7
            });

            return menuItems;
        }
    }
}
