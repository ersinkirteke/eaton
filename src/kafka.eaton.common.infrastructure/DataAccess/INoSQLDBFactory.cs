
namespace kafka.eaton.common.infrastructure.dataaccess
{
    public interface INoSQLDBFactory
    {
        INoSQLDB Create(string dbName);
    }
}
