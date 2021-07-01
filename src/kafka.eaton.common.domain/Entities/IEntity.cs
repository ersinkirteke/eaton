using System;

namespace kafka.eaton.common.domain.entities
{
    public interface IEntity
    {
        string Id { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}
