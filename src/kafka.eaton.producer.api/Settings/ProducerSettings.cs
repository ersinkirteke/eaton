using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kafka.eaton.producer.api.settings
{
    public class ProducerSettings : IProducerSettings
    {
        public string AnySetting { get; set; }
        public string Topic { get; set; }
    }
}
