using System;
using Unity.Notifications.Android;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Assets;
using UnityEngine;
using UnityEngine.Serialization;
using UnityLocalization.Runtime.localization.Scripts.Runtime.Types;

namespace UnityAndroidEx.Runtime.android_ex.Scripts.Runtime.Assets
{
    public sealed class NotificationSettings : ProviderAsset<NotificationSettings>
    {
        #region Static Area

        public static NotificationSettings Singleton => GetSingleton("Notification", "notification.asset");

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => GetSerializedSingleton("Notification", "notification.asset");
#endif

        #endregion

        #region Inspector Data

        [SerializeField]
        private NotificationItem[] items = Array.Empty<NotificationItem>();

        [SerializeField]
        private NotificationChannel[] channels = Array.Empty<NotificationChannel>();

        #endregion

        #region Properties

        public NotificationItem[] Items => items;

        public NotificationChannel[] Channels => channels;

        #endregion
    }

    [Serializable]
    public sealed class NotificationChannel
    {
        #region Inspector Data

        [SerializeField]
        private string id;

        [SerializeField]
        private LocalizedTextRef name;

        [SerializeField]
        private LocalizedTextRef description;

        [SerializeField]
        private Importance importance = Importance.Default;

        #endregion

        #region Properties

        public string ID => id;

        public string Name => name;

        public string Description => description;

        public Importance Importance => importance;

        #endregion
    }

    [Serializable]
    public sealed class NotificationItem
    {
        #region Inspector Data

        [SerializeField]
        private LocalizedTextRef title;

        [SerializeField]
        private LocalizedTextRef text;

        [FormerlySerializedAs("daysDelayed")]
        [SerializeField]
        [Min(1)]
        private uint minutesDelayed = 1;

        [SerializeField]
        private string channelRef;

        #endregion

        #region Properties

        public string Title => title;

        public string Text => text;

        public uint MinutesDelayed => minutesDelayed;

        public string ChannelRef => channelRef;

        #endregion
    }
}