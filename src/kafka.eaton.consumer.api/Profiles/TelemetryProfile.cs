using AutoMapper;
using kafka.eaton.common.domain.entities;
using kafka.eaton.common.domain.models;

namespace kafka.eaton.consumer.api.profiles
{
    public class TelemetryProfile:Profile
    {
        public TelemetryProfile()
        {
            CreateMap<TelemetryDto, Telemetry>();
        }
    }
}
