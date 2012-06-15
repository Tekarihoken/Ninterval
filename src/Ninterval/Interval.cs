using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Ninterval
{


    /// <summary>
    /// Can be used as base class in order to represent an interval
    /// </summary>
    /// <typeparam name="T">Type of the interval</typeparam>
    public abstract class Interval<T> : IInterval<T>
        where T:IComparable<T>
    {
        /// <summary>
        /// Flags enumeration used in order to manage the type of bound
        /// </summary>
        [Flags]
        private enum BoundAtributes
        {
            None = 0,
            IsLeftInfinite = 1,
            IsRightInfinite = 2,
            IsLeftOpenedInterval = 4,
            IsRightOpenedInterval = 8
        }

        /// <summary>
        /// Left bound of the interval
        /// </summary>
        private readonly T _left;
        /// <summary>
        /// Right bound of the interval
        /// </summary>
        private readonly T _right;
        /// <summary>
        /// Enumeration storing the attributes of the different bound
        /// </summary>
        private readonly BoundAtributes _boundAttributes;

        public Interval(T left, T right,
            bool isLeftInfinite, bool isRightInfinite,
            bool isLeftOpenedInterval, bool isRightOpenedInterval)
        {
            Contract.Ensures(this.Left.Equals(left));
            Contract.Ensures(this.Right.Equals(right));
            Contract.Ensures(this.IsLeftInfinite == isLeftInfinite);
            Contract.Ensures(this.IsRightInfinite == isRightInfinite);
            Contract.Ensures(this.IsLeftOpenedInterval == isLeftOpenedInterval);
            Contract.Ensures(this.IsRightOpenedInterval == isRightOpenedInterval);

            _left = left;
            _right = right;
            BoundAtributes enumeration = BoundAtributes.None;
            if (isLeftInfinite)
            {
                enumeration |= BoundAtributes.IsLeftInfinite;
            }
            if (isRightInfinite)
            {
                enumeration |= BoundAtributes.IsRightInfinite;
            }
            if (isLeftOpenedInterval)
            {
                enumeration |= BoundAtributes.IsLeftOpenedInterval;
            }
            if (isRightOpenedInterval)
            {
                enumeration |= BoundAtributes.IsRightOpenedInterval;
            }
            _boundAttributes = enumeration;
        }

        public T Left
        {
            get { return _left; }
        }

        public bool IsLeftInfinite
        {
            get { return _boundAttributes.HasFlag(BoundAtributes.IsLeftInfinite); }
        }

        public bool IsLeftOpenedInterval
        {
            get { return _boundAttributes.HasFlag(BoundAtributes.IsLeftOpenedInterval); }
        }

        public T Right
        {
            get { return _right; }
        }

        public bool IsRightOpenedInterval
        {
            get { return _boundAttributes.HasFlag(BoundAtributes.IsRightInfinite); }
        }

        public bool IsRightInfinite
        {
            get { return _boundAttributes.HasFlag(BoundAtributes.IsRightOpenedInterval); }
        }

        public override string ToString()
        {
            char right;
            if (IsRightOpenedInterval)
            {
                right = '[';
            }
            else
            {
                right = ']';
            }
            char left;
            if (IsLeftOpenedInterval)
            {
                left =']' ;
            }
            else
            {
                left = '[';
            }

            return string.Format("{0}{1},{2}{3}", left, Left, right, Right);

        }
    }
}
