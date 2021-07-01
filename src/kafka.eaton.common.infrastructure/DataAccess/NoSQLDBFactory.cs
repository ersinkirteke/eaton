using System;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public class NoSQLDBFactory : INoSQLDBFactory
    {
        public INoSQLDB Create(string dbName)
        {
            if (dbName == "CassandraDB")
                return new CassandraDB();
            throw new ArgumentException("dbName is invalid");
        }
    }
}
