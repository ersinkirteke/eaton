using Cassandra;
using Cassandra.Mapping;
using kafka.eaton.common.domain.entities;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    /// <summary>
    /// 
    /// </summary>
    public class TelemetryDataAccess : ITelemetryDataAccess
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        private string _keyspace;
        public TelemetryDataAccess(IOptions<TelemetryDataAccessOptions> options)
        {
            _keyspace = options.Value.KeySpace;
            ICassandraDataAccess cassandraDAO = new CassandraDataAccess();
            session = cassandraDAO.GetSession(_keyspace);
            mapper = new Mapper(session);
        }

        public async Task InsertTelemetry(Telemetry telemetry)
        {
            await mapper.InsertAsync<Telemetry>(telemetry);
        }
    }
}
