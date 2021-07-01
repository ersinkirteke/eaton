using kafka.eaton.common.domain.entities;
using System.Threading.Tasks;

namespace kafka.eaton.common.infrastructure.dataaccess
{
    public interface IDataAccess<T> where T : IEntity
    {
        Task Insert(T entity);
    }
}
