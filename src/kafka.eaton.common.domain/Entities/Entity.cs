using System;

namespace kafka.eaton.common.domain.entities
{
    public class Entity: IEntity
    {
        public string Id { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
