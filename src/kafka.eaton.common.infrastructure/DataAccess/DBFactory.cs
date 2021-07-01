namespace kafka.eaton.common.infrastructure.dataaccess
{
    public class DBFactory : IDBFactory
    {
        public INoSQLDB CreateNoSQLDB(string dbName)
        {
            NoSQLDBFactory factory = new NoSQLDBFactory();

            return factory.Create(dbName);
        }
    }
}
