using kafka.eaton.common.domain.entities;
using Microsoft.Extensions.Options;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public class TelemetryDataAccess : DataAccess<Telemetry>
    {
        public TelemetryDataAccess(IOptions<DataAccessOptions> options) : base(options)
        {

        }
    }
}
