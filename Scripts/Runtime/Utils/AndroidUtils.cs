#if PLATFORM_ANDROID
using UnityEngine;

namespace UnityAndroidEx.Runtime.Projects.unity_android_ex.Scripts.Runtime.Utils
{
    public static class AndroidUtils
    {
        private static AndroidJavaObject Activity
        {
            get
            {
                var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }

        public static AndroidJavaObject Context => Activity.Call<AndroidJavaObject>("getApplicationContext");

        public static bool IsOnTV
        {
            get
            {
                var service = Context.Call<AndroidJavaObject>("getSystemService", "uimode");
                var mode = service.Call<int>("getCurrentModeType");

                return mode == 4;
            }
        }

        public static void ShowToast(string message)
        {
            var toastClass = new AndroidJavaClass("android.widget.Toast");
            Activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                var toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", Activity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
#endif