using Cassandra;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public interface ICassandraDataAccess
    {
        ISession GetSession(string keyspace);
    }
}
