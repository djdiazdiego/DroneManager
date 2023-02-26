using Force.DeepCloner;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DroneManager.Core.Abstractions.Entities
{
    public abstract class ValueObject : ICloneable
    {
        /// <summary>
        /// Compare Value Object: Equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right) =>
            !(left is null ^ right is null) && (left is null || left.Equals(right));

        /// <summary>
        /// Compare Value Object: Not Equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right) =>
            !(EqualOperator(left, right));

        /// <summary>
        /// Personalized for each Value Object
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <inheritdoc />
        public override bool Equals(object obj) =>
            obj != null && obj.GetType() == GetType() &&
            this.GetEqualityComponents().SequenceEqual(((ValueObject)obj).GetEqualityComponents());

        /// <inheritdoc />
        public override int GetHashCode() =>
            GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);

        /// <inheritdoc />
        public object Clone() =>
            this.DeepClone();
    }
}
