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
        where T : IComparable<T>
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

        /// <summary>
        /// Left bound of the interval
        /// </summary>
        public T Left
        {
            get { return _left; }
        }

        /// <summary>
        /// Indicate if the left bound is open (or close)
        /// </summary>
        public bool IsLeftOpenedInterval
        {
            get { return _boundAttributes.HasFlag(BoundAtributes.IsLeftOpenedInterval); }
        }

        /// <summary>
        /// Indicate if the left bound correspond to an infinite value
        /// </summary>
        public bool IsLeftInfinite
        {
            get { return _boundAttributes.HasFlag(BoundAtributes.IsLeftInfinite); }
        }

        /// <summary>
        /// Right bound of the interval
        /// </summary>
        public T Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Indicate if the right bound is open (or close)
        /// </summary>
        public bool IsRightOpenedInterval
        {
            get { return _boundAttributes.HasFlag(BoundAtributes.IsRightInfinite); }
        }

        /// <summary>
        /// Indicate if the right bound correspond to an infinite value
        /// </summary>
        public bool IsRightInfinite
        {
            get { return _boundAttributes.HasFlag(BoundAtributes.IsRightOpenedInterval); }
        }

        /// <summary>
        /// Indicate if the current interval overlap the other interval
        /// </summary>
        /// <param name="other">other interval</param>
        /// <returns>true if the 2 intvervals are overlaping</returns>
        public bool Overlaps(IInterval<T> other)
        {
            //TODO : factorisation
            if (!other.IsLeftInfinite && !this.IsRightInfinite && this.Right.CompareTo(other.Left) == -1)
            {
                return false;
            }
            else if (!this.IsRightInfinite && !other.IsLeftInfinite&&this.Left.CompareTo(other.Right)==1)
            {
                return false;
            }
            else if (!other.IsLeftInfinite && !this.IsRightInfinite && this.Right.CompareTo(other.Left) == 0 &&
                (this.IsRightOpenedInterval || other.IsRightOpenedInterval))
            {
                return false;
            }
            else if (!this.IsRightInfinite && !other.IsLeftInfinite && this.Left.CompareTo(other.Right) == 0 &&
                (this.IsRightOpenedInterval || other.IsLeftOpenedInterval))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Write a string description of the instance
        /// </summary>
        /// <returns>string using the form : [x,y]</returns>
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
                left = ']';
            }
            else
            {
                left = '[';
            }

            return string.Format("{0}{1},{2}{3}", left, Left, right, Right);

        }
    }
}
