using System;

namespace kafka.eaton.common.domain.models
{
    /// <summary>
    /// Telemetry Dto
    /// </summary>
    public class TelemetryDto
    {
        public string DeviceName { get; set; }
        public int Temperature { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
