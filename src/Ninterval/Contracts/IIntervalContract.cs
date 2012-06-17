using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Ninterval.Contracts
{
    [ContractClassFor(typeof(IInterval<>))]
    abstract class IIntervalContract<T> : IInterval<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Indicate if the interval is empty or not
        /// </summary>
        public abstract bool IsEmpty { get; }

        public abstract T Left { get; }

        public abstract bool IsLeftOpenedInterval { get; }

        public abstract bool IsLeftInfinite { get; }

        public abstract T Right { get; }

        public abstract bool IsRightOpenedInterval { get; }

        public abstract bool IsRightInfinite { get; }

        /// <summary>
        /// Indicate if the current interval overlap the other interval
        /// </summary>
        /// <param name="other">other interval</param>
        /// <returns>true if the 2 intvervals are overlaping</returns>
        bool IInterval<T>.Overlaps(IInterval<T> other)
        {
            Contract.Requires(other != null);
            return false;
        }

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(IsEmpty || IsLeftInfinite || IsRightInfinite || Right.CompareTo(Left) >= 0,
                "Left bound should be lower or equals to right bound");
            Contract.Invariant(!(Right.Equals(Left) && (IsLeftOpenedInterval || IsRightOpenedInterval)),
                "Only singleton [x,x] can have right value equals to left value");
        }
    }
}
