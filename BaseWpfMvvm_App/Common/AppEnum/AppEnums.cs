using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseWpfMvvm_App.Common.AppEnum
{
    public static class AppEnums
    {
        public enum AppCommunicationType
        {
            None,
            Message,
            Action,
            AppStateUpdate,
            Validation
        }

    }
}
