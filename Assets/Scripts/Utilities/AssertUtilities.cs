using System;
using System.Collections.Generic;

namespace Utilities
{
    public static class AssertUtilities
    {

        public static void Assert(bool value, string message)
        {
            if (!value)
                throw new Exception(message);
        }
        
        public static T ThrowExceptionIfNull<T>(this T value)
        {
            if (value == null)
                throw new NullReferenceException();
            return value;
        }
        
        public static float ThrowExceptionIfNegative(this float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();
            return value;
        }
        
        public static float ThrowExceptionIfMoreThan(this float value, float otherValue)
        {
            if (value > otherValue)
                throw new ArgumentOutOfRangeException();
            return value;
        }
    }
}