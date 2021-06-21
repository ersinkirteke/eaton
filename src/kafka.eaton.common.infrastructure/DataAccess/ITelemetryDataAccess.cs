using kafka.eaton.common.domain.entities;
using System.Threading.Tasks;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public interface ITelemetryDataAccess
    {
        Task InsertTelemetry(Telemetry telemetry);
    }
}
