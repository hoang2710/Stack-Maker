using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (LevelManager)target;

        if (GUILayout.Button("Save Level"))
        {
            script.SaveLevel();
        }
        if (GUILayout.Button("Clear Level"))
        {
            script.ClearLevelEditor();
        }
        if (GUILayout.Button("Load Level"))
        {
            script.LoadLevel();
        }
    }
}
