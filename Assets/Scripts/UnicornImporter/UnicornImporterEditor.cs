using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[CustomEditor(typeof(UnicornImporter))]
public class UnicornImporterEditor : ScriptedImporterEditor
{
    static class Style
    {
        public static GUIContent TailColorOverride = new GUIContent("Override Tail colors ?");
        public static GUIContent TailColors = new GUIContent("Tail colors");
        public static GUIContent ResetTailToFile = new GUIContent("Reset Tail");
    }

    SerializedProperty m_TailColorOverride;
    SerializedProperty m_TailColors;
    SerializedProperty m_FileTailColors;

    public override void OnEnable()
    {
        // Needs to make sure base.OnEnable is called as of 2019.2
        // it used to do nothing but it is not the case anymore.
        base.OnEnable();

        m_TailColorOverride = serializedObject.FindProperty("m_TailColorOverride");
        m_TailColors = serializedObject.FindProperty("m_TailColors");
        m_FileTailColors = serializedObject.FindProperty("m_FileTailColors");
    }

    public override void OnInspectorGUI()
    {
#if UNITY_2019_2_OR_NEWER
        // starting in 2019.2, AssetImporterEditor work the same as other Editors
        // and needs to call update/apply on serializedObject if any changes are made to serializedProperties.
        serializedObject.UpdateIfRequiredOrScript();
#endif

        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.PropertyField(m_TailColorOverride, Style.TailColorOverride);
            if (GUILayout.Button(Style.ResetTailToFile))
            {

            }
        }

        bool overrideTails = !m_TailColorOverride.hasMultipleDifferentValues && m_TailColorOverride.boolValue;
        using (new EditorGUI.DisabledScope(!overrideTails))
        {
            var prop = overrideTails ? m_TailColors : m_FileTailColors;
            EditorGUILayout.PropertyField(prop, Style.TailColors, prop.isExpanded);
        }

#if UNITY_2019_2_OR_NEWER
        serializedObject.ApplyModifiedProperties();
#endif

        // Make sure ApplyRevertGUI is called to have the Apply/Revert mechanism working correctly.
        ApplyRevertGUI();
    }
}
