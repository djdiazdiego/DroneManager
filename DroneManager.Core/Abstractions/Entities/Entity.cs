using DroneManager.Core.Abstractions.Entities.Interfaces;
using Force.DeepCloner;
using System;

namespace DroneManager.Core.Abstractions.Entities
{
    public abstract class Entity<TKey> : IEntity
    {
        private int? _requestedHashCode;
        private DateTimeOffset _created;
        private DateTimeOffset _lastModified;

        protected Entity()
        {
        }

        protected Entity(TKey id)
        {
            Id = id;
        }

        /// <summary>
        /// Entity identifier
        /// </summary>
        public TKey Id { get; }

        /// <inheritdoc />
        object IEntity.Id => Id;

        /// <inheritdoc />
        public DateTimeOffset Created => _created;

        /// <inheritdoc />
        public DateTimeOffset? LastModified => _lastModified;

        /// <inheritdoc />
        public void SetCreatedDate(DateTimeOffset date) => _created = date;

        /// <inheritdoc />
        public void SetLastModified(DateTimeOffset date) => _lastModified = date;

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

        /// <inheritdoc />
        public bool IsTransient() =>
            (typeof(TKey) == typeof(long) || typeof(TKey) == typeof(int) ||
                typeof(TKey) == typeof(Guid)) && Id.Equals(default(TKey));

        /// <inheritdoc />
        public override bool Equals(object obj) =>
            obj != null && obj is Entity<TKey> entity &&
            (ReferenceEquals(this, obj) || (GetType() == obj.GetType() && !entity.IsTransient() &&
                !IsTransient() && entity.Id.Equals(Id)));

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (IsTransient())
                return base.GetHashCode();

            if (!_requestedHashCode.HasValue)
                _requestedHashCode = new int?(Id.GetHashCode() ^ 31);

            return _requestedHashCode.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Entity<TKey> left, Entity<TKey> right) =>
            Object.Equals(left, null) ? Object.Equals(right, null) : left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Entity<TKey> left, Entity<TKey> right) =>
            !(left == right);
    }
}
