using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWpfMvvm_App.Common.BaseClass;

namespace BaseWpfMvvm_App.Common.AppModel
{
    public class AppMenuItem : ViewModelBase
    {
        private int _displayOrder;
        private bool _hasSetupError;
        private int _id;
        private bool _isCheckable;
        private string _name;
        private int _parentId;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int ParentId
        {
            get => _parentId;
            set => SetProperty(ref _parentId, value);
        }

        public new string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public bool IsCheckable
        {
            get => _isCheckable;
            set => SetProperty(ref _isCheckable, value);
        }

        public int DisplayOrder
        {
            get => _displayOrder;
            set => SetProperty(ref _displayOrder, value);
        }

        public bool HasSetupError
        {
            get => _hasSetupError;
            set => SetProperty(ref _hasSetupError, value);
        }
    }
}
