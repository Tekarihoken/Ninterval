using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninterval
{
    /// <summary>
    /// Interval class
    /// </summary>
    /// <typeparam name="T">type of data represented with the intervals</typeparam>
    [ContractClass(typeof(IntervalContractClass<T>))]
    public interface IInterval<in T> 
        where T:struct,IComparable<T>
    {
        /// <summary>
        /// Left value of the interval
        /// </summary>
        Nullable<T> Left{ get; }

        /// <summary>
        /// Right value of the interval
        /// </summary>
        Nullable<T> Right { get; }
    }

    [ContractClassFor(typeof(IInterval<T>))]
    internal class IntervalContractClass<T>:IInterval<T>
        where T : struct,IComparable<T>
    {
        public T? Left
        {
	        get { return null; }
        }

        public T? Right
        {
	        get { return null; }
        }

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(!Left.HasValue || !Right.HasValue || Left.Value.CompareTo(Right.Value) <= 0);
        }

}
}
