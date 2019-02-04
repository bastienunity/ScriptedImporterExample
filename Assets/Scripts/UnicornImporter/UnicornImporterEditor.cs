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
        base.OnEnable();

        m_TailColorOverride = serializedObject.FindProperty("m_TailColorOverride");
        m_TailColors = serializedObject.FindProperty("m_TailColors");
        m_FileTailColors = serializedObject.FindProperty("m_FileTailColors");
    }

    public override void OnInspectorGUI()
    {
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

        ApplyRevertGUI();
    }
}
