using Cassandra;
using System;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public class CassandraDB : ICassandraDB
    {
        public string GetConnection()
        {
            throw new NotImplementedException();
        }

        private static Cluster Cluster;
        private static ISession Session;

        public CassandraDB()
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
