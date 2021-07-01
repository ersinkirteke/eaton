namespace kafka.eaton.common.infrastructure.dataaccess
{
    public interface IDBFactory
    {
        INoSQLDB CreateNoSQLDB(string dbName);
    }
}
