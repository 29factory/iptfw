using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System;

public class SceneBuilder : EditorWindow {
    private int fillType = 0, rawType = 0;
    private MonoScript filler, oldFiller;
    private AbstractFiller[] fillers = new AbstractFiller[] { new Filler (), new Drawer (), new Dotter(), null };
    private AbstractSetter[] rawSetters = new AbstractSetter[] { new FloorSetter (), new WallSetter (), new FenceSetter(), new DoorToSetter(), new DoorSetter(), new Destroyer() };

    [MenuItem("Window/Scene Builder")]
    public static void ShowWindow () {
        GetWindowWithRect<SceneBuilder> (new Rect (0, 0, 200, 400), false, "Scene Builder");
    }

    void OnGUI () {
        if ((fillType = EditorGUILayout.Popup ("Fill type", fillType, new string[]{ "Fill", "Draw", "Dot", "Custom" })) == fillers.Length - 1) {
            oldFiller = filler;
            filler = (MonoScript)EditorGUILayout.ObjectField ("Filler", filler, typeof(MonoScript), false);
            if (oldFiller != filler && filler != null) fillers [fillers.Length - 1] = Activator.CreateInstance (filler.GetClass ()) as AbstractFiller;
        }
        if (fillers [fillType] != null)
            fillers [fillType].ShowRequirements ();
        rawType = EditorGUILayout.Popup ("Raw type", rawType, new string[] { "Floor", "Wall", "Fence", "Door to", "Door", "Void" });
        rawSetters[rawType].ShowRequirements ();
        if (GUILayout.Button ("Just do it!") && fillers[fillType] != null)
            fillers [fillType].Call (rawSetters [rawType]);
    }

    void OnSceneGUI (SceneView scene) {
        if (fillers [fillType] != null) {
            Vector2 ghostSize = fillers [fillType] is AreaFiller ? (fillers [fillType] as AreaFiller).ghostSize : new Vector2(1, 1);
            Handles.DrawLine (new Vector3(fillers [fillType].ghostPosition.x - 0.5f, fillers [fillType].ghostPosition.y - 0.5f) * fillers [fillType].scaleFactor, (new Vector3(fillers [fillType].ghostPosition.x - 0.5f, fillers [fillType].ghostPosition.y - 0.5f) + new Vector3(ghostSize.x, ghostSize.y)) * fillers [fillType].scaleFactor);
            Handles.DrawSolidRectangleWithOutline (new Rect(new Vector2(fillers [fillType].ghostPosition.x - 0.5f, fillers [fillType].ghostPosition.y - 0.5f) * fillers [fillType].scaleFactor, new Vector2(ghostSize.x, ghostSize.y) * fillers [fillType].scaleFactor), Color.clear, Color.white);
        }
    }

    void OnEnable () {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    void OnDisable () {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }
}
