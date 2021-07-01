namespace kafka.eaton.common.domain.entities
{
    /// <summary>
    /// Telemetry Entity
    /// </summary>
    public class Telemetry : Entity
    {
        public string DeviceName { get; set; }
        public int Temperature { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
