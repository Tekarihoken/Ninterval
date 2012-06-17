using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Ninterval.Contracts;

namespace Ninterval
{
    /// <summary>
    /// Interval class
    /// </summary>
    /// <typeparam name="T">type of data represented with the intervals</typeparam>
    [ContractClass(typeof(IIntervalContract<>))]
    public interface IInterval<T>
        where T :IComparable<T>
    {
        /// <summary>
        /// Left bound of the interval
        /// </summary>
        T Left{ get; }

        /// <summary>
        /// Indicate if the left bound is open (or close)
        /// </summary>
        bool IsLeftOpenedInterval { get; }

        /// <summary>
        /// Indicate if the left bound correspond to an infinite value
        /// </summary>
        bool IsLeftInfinite { get; }

        /// <summary>
        /// Right bound of the interval
        /// </summary>
        T Right { get; }

        /// <summary>
        /// Indicate if the right bound is open (or close)
        /// </summary>
        bool IsRightOpenedInterval { get; }

        /// <summary>
        /// Indicate if the right bound correspond to an infinite value
        /// </summary>
        bool IsRightInfinite { get; }

        /// <summary>
        /// Indicate if the current interval overlap the other interval
        /// </summary>
        /// <param name="other">other interval</param>
        /// <returns>true if the 2 intvervals are overlaping</returns>
        bool Overlaps(IInterval<T> other);
    }
}
