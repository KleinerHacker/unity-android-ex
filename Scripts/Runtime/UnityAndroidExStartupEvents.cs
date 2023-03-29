#if PCSOFT_ANDROID_NOTIFICATION && PLATFORM_ANDROID
using Unity.Notifications.Android;
using UnityAndroidEx.Runtime.android_ex.Scripts.Runtime.Assets;
using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime;
using UnityEngine;
using UnityEngine.Android;

namespace UnityAndroidEx.Runtime.android_ex.Scripts.Runtime
{
    //Permission android.permission.POST_NOTIFICATIONS required
    public static class UnityAndroidExStartupEvents
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            Debug.Log("[ANDROID-NOTIFY] Request permission....");
            if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            {
                Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
            }

            Debug.Log("[ANDROID-NOTIFY] Initialize notification...");
            AssetResourcesLoader.LoadFromResources<NotificationSettings>("");

#if PCSOFT_ANDROID_NOTIFICATION_LOGGING
            Debug.Log("[ANDROID-NOTIFY] Register channels... (" + NotificationSettings.Singleton.Channels.Length + ")");
#endif
            foreach (var channel in NotificationSettings.Singleton.Channels)
            {
#if PCSOFT_ANDROID_NOTIFICATION_LOGGING
                Debug.Log("[ANDROID-NOTIFY] Register channels " + channel.ID + " (" + channel.Name + ")");
#endif
                var androidChannel = new AndroidNotificationChannel(channel.ID, channel.Name, channel.Description, channel.Importance);
                AndroidNotificationCenter.RegisterNotificationChannel(androidChannel);
            }

#if PCSOFT_ANDROID_NOTIFICATION_LOGGING
            Debug.Log("[ANDROID-NOTIFY] Register notifications... (" + NotificationSettings.Singleton.Items.Length + ")");
#endif
            AndroidNotificationCenter.CancelAllNotifications();
            foreach (var item in NotificationSettings.Singleton.Items)
            {
#if PCSOFT_ANDROID_NOTIFICATION_LOGGING
                Debug.Log("[ANDROID-NOTIFY] Register notification " + item.Title + " on channel " + item.ChannelRef);
#endif
                var notification = new AndroidNotification
                {
                    Title = item.Title,
                    Text = item.Text,
                    FireTime = System.DateTime.Now.AddMinutes(item.MinutesDelayed)
                };

                AndroidNotificationCenter.SendNotification(notification, item.ChannelRef);
            }
        }
    }
}
#endif