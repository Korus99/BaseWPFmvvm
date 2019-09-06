using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWpfMvvm_App.Common.AppEnum;
using BaseWpfMvvm_App.Common.BaseClass;
using static BaseWpfMvvm_App.Common.AppConstant.AppConstant;

namespace BaseWpfMvvm_App.Common.Framework
{
    public class FrameworkVm: ViewModelBase

    {
        private ViewModelBase _selectedView;

        public ViewModelBase SelectedView
        {
            get => _selectedView;
            set => SetProperty(ref _selectedView, value);
        }

        public void Navigate(string menuName)
        {
            // navigate only if menuName is not null or empty.
            if (string.IsNullOrWhiteSpace(menuName))
            {
            }

            if (menuName == MenuHelp)
            {
            }

        }

    }
}
