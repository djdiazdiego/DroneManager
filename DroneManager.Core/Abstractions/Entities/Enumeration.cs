using DroneManager.Core.Abstractions.Entities.Interfaces;
using System;

namespace DroneManager.Core.Abstractions.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Enumeration<TKey> : Entity<TKey>, IEnumeration, INotRepository
        where TKey : Enum
    {
        private string _name;

        /// <summary>
        /// 
        /// </summary>
        protected Enumeration() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected Enumeration(TKey id, string name) : base(id)
        {
            _name = name;
        }

        /// <inheritdoc />
        public string Name => _name;

        /// <inheritdoc />
        public void SetName(string name) => _name = name;

        /// <inheritdoc />
        public override bool Equals(object obj) =>
            obj is Enumeration<TKey> otherValue && GetType().Equals(obj.GetType()) && Id.Equals(otherValue.Id);


        /// <inheritdoc />
        public override int GetHashCode() =>
            Id.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <inheritdoc />
        public int CompareTo(object other) => Id.CompareTo(((Enumeration<TKey>)other).Id);
    }
}
