using System;

namespace DroneManager.Core.Abstractions.Dtos
{
    public interface IDTO
    {
        
    }

    public interface IDTO<TKey> : IDTO
    {
        public TKey Id { get; set; }
    }
}
