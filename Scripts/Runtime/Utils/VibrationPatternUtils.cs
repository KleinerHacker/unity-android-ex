using System;
using System.Linq;
using UnityEngine;

namespace UnityAndroidEx.Runtime.android_ex.Scripts.Runtime.Utils
{
    internal static class VibrationPatternUtils
    {
        public static bool IsValidPattern(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                return true;

            var parts = pattern.Split(',');
            return parts.All(x => long.TryParse(x.Trim(), out _));
        }

        public static long[] ParsePattern(string pattern)
        {
            if (!IsValidPattern(pattern))
            {
                Debug.LogError("Pattern is invalid for vibration: " + pattern);
                return null;
            }

            var parts = pattern.Split(',');
            try
            {
                return parts.Select(long.Parse).ToArray();
            }
            catch (FormatException e)
            {
                Debug.LogError("Pattern is invalid for vibration <" + pattern + ">: " + e.Message);
                return null;
            }
        }
    }
}