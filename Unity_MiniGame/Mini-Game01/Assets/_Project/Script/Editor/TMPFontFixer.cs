#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using TMPro;

public class TMPFontFixer : EditorWindow
{
    public TMP_FontAsset targetFont;

    [MenuItem("Tools/TMP Font Fixer")]
    public static void ShowWindow()
    {
        GetWindow<TMPFontFixer>("TMP Font Fixer");
    }

    private void OnGUI()
    {
        targetFont = (TMP_FontAsset)EditorGUILayout.ObjectField("Font to Apply", targetFont, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Apply to All TMP Texts"))
        {
            ApplyFontToAllTMPTexts();
        }
    }

    private void ApplyFontToAllTMPTexts()
    {
        var allTexts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        int count = 0;

        foreach (var text in allTexts)
        {
            if (text.font != targetFont)
            {
                Undo.RecordObject(text, "Change TMP Font");
                text.font = targetFont;
                EditorUtility.SetDirty(text);
                count++;
            }
        }

        Debug.Log($"TMP Font Fixer: Updated {count} TMP texts.");
    }
}
#endif