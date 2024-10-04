#if PLATFORM_ANDROID
using UnityAndroidEx.Runtime.Projects.unity_android_ex.Scripts.Runtime.Assets;

namespace UnityAndroidEx.Runtime.Projects.unity_android_ex.Scripts.Runtime.Utils.Extensions
{
    internal static class NotificationSettingsExtensions
    {
        public static long[] GetNativeVibrationPattern(this NotificationChannel channel) =>
            VibrationPatternUtils.ParsePattern(channel.VibrationPattern);
    }
}
#endif