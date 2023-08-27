using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Event
{
    public class UpDateModel
    {
        public bool IsOpen { get; set; }
    }
    public class UpDateLodingEvent:PubSubEvent<UpDateModel>
    {

    }
}
