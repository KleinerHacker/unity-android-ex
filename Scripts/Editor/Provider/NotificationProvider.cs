using UnityAndroidEx.Runtime.android_ex.Scripts.Runtime.Assets;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityAndroidEx.Editor.android_ex.Scripts.Editor.Provider
{
    public sealed class NotificationProvider : SettingsProvider
    {
        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new NotificationProvider();
        }

        #endregion

        private SerializedObject _settings;
        private SerializedProperty _itemsProperty;
        private SerializedProperty _channelsProperty;

        private NotificationItemList notificationItemList;
        private NotificationChannelList notificationChannelList;

        public NotificationProvider() : base("Project/Player/Notification", SettingsScope.Project, new[] { "android", "mobile", "notification" })
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = NotificationSettings.SerializedSingleton;
            if (_settings == null)
                return;

            _itemsProperty = _settings.FindProperty("items");
            _channelsProperty = _settings.FindProperty("channels");

            notificationItemList = new NotificationItemList(_settings, _itemsProperty);
            notificationChannelList = new NotificationChannelList(_settings, _channelsProperty);
        }

        public override void OnTitleBarGUI()
        {
            GUILayout.BeginVertical();
            {
                ExtendedEditorGUILayout.SymbolField("Activate System", "PCSOFT_ANDROID_NOTIFICATION");
                EditorGUI.BeginDisabledGroup(
#if PCSOFT_ANDROID_NOTIFICATION
                    false
#else
                    true
#endif
                );
                {
                    ExtendedEditorGUILayout.SymbolField("Verbose Logging", "PCSOFT_ANDROID_NOTIFICATION_LOGGING");
                }
                EditorGUI.EndDisabledGroup();
            }
            GUILayout.EndVertical();
        }

        public override void OnGUI(string searchContext)
        {
            _settings.Update();

            GUILayout.Space(15f);

#if PCSOFT_ANDROID_NOTIFICATION
            GUILayout.Label("Channels");
            notificationChannelList.DoLayoutList();
            GUILayout.Space(5f);
            GUILayout.Label("Notifications");
            notificationItemList.DoLayoutList();
#else
            EditorGUILayout.HelpBox("Notification system is deactivated", MessageType.Info);
#endif

            _settings.ApplyModifiedProperties();
        }
    }
}