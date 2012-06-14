using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Ninterval
{
    public abstract class Interval<T> : IInterval<T>
        where T:IComparable<T>
    {
        private readonly T _left;
        private readonly T _right;
        private readonly BitArray _boolArray;

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
            _boolArray = new BitArray(
                new bool[4] { isLeftInfinite, isRightInfinite, isLeftOpenedInterval, isRightOpenedInterval });
        }

        public T Left
        {
            get { return _left; }
        }

        public bool IsLeftInfinite
        {
            get { return _boolArray[0]; }
        }

        public bool IsLeftOpenedInterval
        {
            get { return _boolArray[2]; }
        }

        public T Right
        {
            get { return _right; }
        }

        public bool IsRightOpenedInterval
        {
            get { return _boolArray[3]; }
        }

        public bool IsRightInfinite
        {
            get { return _boolArray[1]; }
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
