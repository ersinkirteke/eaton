using Cassandra;
using Cassandra.Mapping;
using kafka.eaton.common.domain.entities;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public class DataAccess<T> : IDataAccess<T> where T : IEntity
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        private string _keyspace;

        public DataAccess(IOptions<DataAccessOptions> options)
        {
            _keyspace = options.Value.KeySpace;
            IDBFactory dBFactory = new DBFactory();
            ICassandraDB cassandraDB = dBFactory.CreateNoSQLDB("CassandraDB") as CassandraDB;
            session = cassandraDB.GetSession(_keyspace);
            mapper = new Mapper(session);
        }

        public async Task Insert(T entity)
        {
            await mapper.InsertAsync(entity);
        }
    }
}
