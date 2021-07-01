using Cassandra;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public interface ICassandraDB : INoSQLDB
    {
        ISession GetSession(string keyspace);
    }
}
