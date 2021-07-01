using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kafka.eaton.producer.api.settings
{
    public interface IProducerSettings
    {
        string AnySetting { get; set; }
        string Topic { get; set; }
    }
}
