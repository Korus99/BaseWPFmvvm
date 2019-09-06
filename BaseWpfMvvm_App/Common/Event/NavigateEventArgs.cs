using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseWpfMvvm_App.Common.Event
{
    public class NavigateEventArgs
    {
        public NavigateEventArgs()
        {
        }

        public NavigateEventArgs(string navString)
        {
            NavigationString = navString;
        }

        public string NavigationString { get; set; }
    }
}
