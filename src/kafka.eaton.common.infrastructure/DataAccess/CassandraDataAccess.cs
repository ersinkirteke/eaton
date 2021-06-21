using Cassandra;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    /// <summary>
    /// Cassandra Data Access Layer
    /// </summary>
    public class CassandraDataAccess : ICassandraDataAccess
    {
        private static Cluster Cluster;
        private static ISession Session;

        public CassandraDataAccess()
        {
            SetCluster();
        }
        private void SetCluster()
        {
            if (Cluster == null)
            {
                Cluster = Connect();
            }
        }

        public ISession GetSession(string keyspace)
        {
            if (Cluster == null)
            {
                SetCluster();
                Session = Cluster.Connect(keyspace);
            }
            else if (Session == null)
            {
                Session = Cluster.Connect(keyspace);
            }

            return Session;
        }

        private Cluster Connect()
        {
            Cluster cluster = Cluster.Builder()
                .AddContactPoint("localhost")
                .WithPort(9042)
                .Build();

            return cluster;
        }
    }
}
