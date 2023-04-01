using UnityAndroidEx.Runtime.android_ex.Scripts.Runtime.Assets;

namespace UnityAndroidEx.Runtime.android_ex.Scripts.Runtime.Utils.Extensions
{
    internal static class NotificationSettingsExtensions
    {
        public static long[] GetNativeVibrationPattern(this NotificationChannel channel) =>
            VibrationPatternUtils.ParsePattern(channel.VibrationPattern);
    }
}