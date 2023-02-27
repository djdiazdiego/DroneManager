using System;

namespace DroneManager.Core.Abstractions.Dtos
{
    public interface IDTO<TKey>
    {
        public TKey Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? LastModified { get; set; }
    }
}
