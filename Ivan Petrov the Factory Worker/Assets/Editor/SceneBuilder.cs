using UnityEngine;
using UnityEditor;
using System.Collections;

public class SceneBuilder : EditorWindow {
    public static Vector2 ghostPosition = new Vector2(0, 0);
    public static Vector2 ghostSize = new Vector2(1, 1);

    [MenuItem("Window/Scene builder")]
    public static void ShowWindow () {
        GetWindow<SceneBuilder> ();
    }

    void OnGUI () {
        ghostPosition = EditorGUILayout.Vector2Field ("Position", ghostPosition);
        ghostSize = EditorGUILayout.Vector2Field ("Size", ghostSize);
        GUILayout.Toolbar (0, new string[]{ "Floor", "Walls" });
        GUILayout.BeginHorizontal ();
        GUILayout.Button ("Build");
        GUILayout.Button ("Destroy");
        GUILayout.EndHorizontal ();
    }

    void OnSceneGUI (SceneView scene) {
        Handles.DrawLine (new Vector3(ghostPosition.x - 0.5f, ghostPosition.y - 0.5f) * 8, (new Vector3(ghostPosition.x - 0.5f, ghostPosition.y - 0.5f) + new Vector3(ghostSize.x, ghostSize.y)) * 8);
    }

    void OnEnable () {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    void OnDisable () {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }
}
