using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LocalizationTools))]
public class LocalizationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LocalizationTools localizationTools = (LocalizationTools)target;
        if (GUILayout.Button("Replace comma seperator with asterix in localization csv"))
        {
            localizationTools.ReplaceCharInLocCSV(",", "*");
        }
    }
}
