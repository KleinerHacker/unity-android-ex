using System;
using Unity.Notifications.Android;
using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Extras;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Assets;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Metadata;
using UnityEngine.Serialization;

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

#if UNITY_LOCALIZATION
        [SerializeField]
        [SimpleLocalization]
        private LocalizedString name;

        [SerializeField]
        [SimpleLocalization]
        private LocalizedString description;
#else 
        [SerializeField]
        private String name;

        [SerializeField]
        private String description;
#endif

        [SerializeField]
        private Importance importance = Importance.Default;

        [SerializeField]
        private bool allowLights;

        [SerializeField]
        private bool allowVibration;

        [SerializeField]
        private LockScreenVisibility lockScreenVisibility = LockScreenVisibility.Private;

        [SerializeField]
        private string vibrationPattern;

        #endregion

        #region Properties

        public string ID => id;

#if UNITY_LOCALIZATION
        public string Name => name.GetLocalizedString();

        public string Description => description.GetLocalizedString();
#else
        public string Name => name;

        public string Description => description;
#endif

        public Importance Importance => importance;

        public bool AllowLights => allowLights;

        public bool AllowVibration => allowVibration;

        public LockScreenVisibility LockScreenVisibility => lockScreenVisibility;

        public string VibrationPattern => vibrationPattern;

        #endregion
    }

    [Serializable]
    public sealed class NotificationItem
    {
        #region Inspector Data

        [SerializeField]
        private LocalizedString title;

        [SerializeField]
        private LocalizedString text;

        [FormerlySerializedAs("daysDelayed")]
        [SerializeField]
        [Min(1)]
        private uint minutesDelayed = 1;

        [SerializeField]
        private string channelRef;

        #endregion

        #region Properties

        public string Title => title.GetLocalizedString();

        public string Text => text.GetLocalizedString();

        public uint MinutesDelayed => minutesDelayed;

        public string ChannelRef => channelRef;

        #endregion
    }
}