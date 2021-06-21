using System;

namespace kafka.eaton.common.domain.entities
{
    /// <summary>
    /// Telemetry Entity
    /// </summary>
    public class Telemetry
    {
        public string Id { get; set; }
        public string DeviceName { get; set; }
        public int Temperature { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
