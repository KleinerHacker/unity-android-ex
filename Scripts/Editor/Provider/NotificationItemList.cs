using System.Linq;
using UnityAndroidEx.Runtime.android_ex.Scripts.Runtime.Assets;
using UnityAndroidEx.Runtime.android_ex.Scripts.Runtime.Utils;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils.Extensions;
using UnityEngine;

namespace UnityAndroidEx.Editor.android_ex.Scripts.Editor.Provider
{
    public sealed class NotificationItemList : TableReorderableList
    {
        public NotificationItemList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements)
        {
            Columns.Add(new FixedColumn { HeaderText = "Channel", AbsoluteWidth = 150f, ElementCallback = OnChannel });
            Columns.Add(new FixedColumn { HeaderText = "Title", AbsoluteWidth = 300f, ElementCallback = OnTitle });
            Columns.Add(new FlexibleColumn { HeaderText = "Description", ElementCallback = OnMessage });
            Columns.Add(new FixedColumn { HeaderText = "Delay in minutes", AbsoluteWidth = 100f, MaxHeight = 20f, ElementCallback = OnDelay });

            elementHeight = 225f;
        }

        private void OnChannel(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("channelRef");
            var channels = NotificationSettings.Singleton.Channels.Select(x => x.ID).ToArray();
            var index = channels.IndexOf(x => string.Equals(x, prop.stringValue));
            var newIndex = EditorGUI.Popup(rect, index, channels);
            if (newIndex != index)
            {
                prop.stringValue = newIndex < 0 ? "" : channels[newIndex];
            }
        }

        private void OnTitle(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("title");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }

        private void OnMessage(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("text");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }

        private void OnDelay(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("minutesDelayed");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }
    }

    public sealed class NotificationChannelList : TableReorderableList
    {
        public NotificationChannelList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements)
        {
            Columns.Add(new FixedColumn { HeaderText = "ID", AbsoluteWidth = 100f, MaxHeight = 20f, ElementCallback = OnID });
            Columns.Add(new FixedColumn { HeaderText = "Name", AbsoluteWidth = 200f, ElementCallback = OnName });
            Columns.Add(new FlexibleColumn { HeaderText = "Description", ElementCallback = OnDescription });
            Columns.Add(new FixedColumn { HeaderText = "Importance", AbsoluteWidth = 70f, ElementCallback = OnImportance });
            Columns.Add(new FixedColumn { Header = new GUIContent(EditorGUIUtility.IconContent("PointLight Gizmo").image, "Allow LED Lights"), AbsoluteWidth = 20f, MaxHeight = 20f, ElementCallback = OnAllowLights });
            Columns.Add(new FixedColumn { Header = new GUIContent(EditorGUIUtility.IconContent("ReverbFilter Icon").image, "Allow Vibrations"), AbsoluteWidth = 20f, MaxHeight = 20f, ElementCallback = OnAllowVibration });
            Columns.Add(new FixedColumn { HeaderText = "Vibration Pattern", AbsoluteWidth = 100f, MaxHeight = 20f, ElementCallback = OnVibrationPattern });
            Columns.Add(new FixedColumn { HeaderText = "Lockscreen Presentation", AbsoluteWidth = 70f, ElementCallback = OnLockscreenPresentation });

            elementHeight = 225f;
        }

        private void OnID(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("id");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }

        private void OnName(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("name");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }

        private void OnDescription(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("description");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }

        private void OnImportance(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("importance");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }

        private void OnAllowLights(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("allowLights");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }

        private void OnAllowVibration(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("allowVibration");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }

        private void OnVibrationPattern(Rect rect, int i, bool isactive, bool isfocused)
        {
            var enabledProp = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("allowVibration");
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("vibrationPattern");
            EditorGUI.BeginDisabledGroup(!enabledProp.boolValue);
            {
                var pattern = EditorGUI.TextField(rect, GUIContent.none, prop.stringValue);
                if (prop.stringValue != pattern)
                {
                    if (!VibrationPatternUtils.IsValidPattern(pattern))
                    {
                        Debug.LogError("[ANDROID-NOTIFY] Unable to parse vibration pattern <" + pattern + "> for channel");
                    }
                    else
                    {
                        prop.stringValue = pattern;
                    }
                }
            }
            EditorGUI.EndDisabledGroup();
        }

        private void OnLockscreenPresentation(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("lockScreenVisibility");
            EditorGUI.PropertyField(rect, prop, GUIContent.none);
        }
    }
}