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
            Contract.Invariant(IsLeftInfinite || IsRightInfinite ||Right.CompareTo(Left)>=0);
        }
    }
}
