using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseWpfMvvm_App.Common.BaseClass
{
    [DataContract]
    public class ModelBase
    {
        public ExtensionDataObject ExtensionData { get; set; }
    }
}
