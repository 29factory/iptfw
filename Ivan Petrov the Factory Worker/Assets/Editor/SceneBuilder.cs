using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System;

public class Context {
    public Vector2 ghostPosition = new Vector2(0, 0);
    public Vector2 ghostSize = new Vector2(1, 1);
    public Texture2D tex1, tex2;
    public bool isGradientUsing = true;
}

public class SceneBuilder : EditorWindow {
    private static Context c = new Context ();

    private int fillType = 0, rawType = 0;
    private MonoScript filler;
    private RawSetter[] rawSetters = new RawSetter[] { new FloorSetter (), new WallSetter (), new FenceSetter(), new Destroyer() };

    [MenuItem("Window/Scene Builder")]
    public static void ShowWindow () {
        GetWindowWithRect<SceneBuilder> (new Rect (0, 0, 200, 400), false, "Scene Builder");
    }

    void OnGUI () {
        c.ghostPosition = EditorGUILayout.Vector2Field ("Position", c.ghostPosition);
        c.ghostSize = EditorGUILayout.Vector2Field ("Size", c.ghostSize);
        GUILayout.Label ("Fill type:");
        if ((fillType = GUILayout.Toolbar (fillType, new string[]{ "Fill", "Draw", "Custom" })) == 2)
            filler = (MonoScript)EditorGUILayout.ObjectField ("Filler", filler, typeof(MonoScript), false);
        GUILayout.Label ("Raw type:");
        rawType = GUILayout.Toolbar (rawType, new string[] { "Floor", "Wall", "Fence", "Void" });
        rawSetters[rawType].ShowRequirements ();
        c.isGradientUsing = GUILayout.Toggle (c.isGradientUsing, "Use gradient technology");
        if (GUILayout.Button ("Just do it!")) switch (fillType) {
        default :
            new Filler ().Call (c, rawSetters [rawType]);
            break;
        case 1:
            new Drawer ().Call (c, rawSetters [rawType]);
            break;
        case 2:
            ((IFiller)Activator.CreateInstance (filler.GetClass ())).Call (c, rawSetters [rawType]);
            break;
        }
    }

    void OnSceneGUI (SceneView scene) {
        Handles.DrawLine (new Vector3(c.ghostPosition.x - 0.5f, c.ghostPosition.y - 0.5f) * 8, (new Vector3(c.ghostPosition.x - 0.5f, c.ghostPosition.y - 0.5f) + new Vector3(c.ghostSize.x, c.ghostSize.y)) * 8);
        Handles.DrawSolidRectangleWithOutline (new Rect(new Vector2(c.ghostPosition.x - 0.5f, c.ghostPosition.y - 0.5f) * 8, new Vector2(c.ghostSize.x, c.ghostSize.y) * 8), Color.clear, Color.white);
    }

    void OnEnable () {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    void OnDisable () {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }
}

public interface RawSetter {
    void ShowRequirements ();
    void Set (Context c, IFiller f, Vector2 pos);
}

class WallSetter : RawSetter {
    private Texture2D top, side;

    public void ShowRequirements () {
        top = (Texture2D)EditorGUILayout.ObjectField ("Top", top, typeof(Texture2D), false);
        side = (Texture2D)EditorGUILayout.ObjectField ("Side", side, typeof(Texture2D), false);
    }

    public void Set (Context c, IFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), pos * 8, Quaternion.identity) as GameObject;
        gameObject.name = "Wall";
        gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(top.name)[c.isGradientUsing ? GradientProvider.GetIndex (((f.IsExists(c, new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(c, new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(c, new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(c, new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0)) | 240) : 13];
        gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [13];
        gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
    }
}

class FloorSetter : RawSetter {
    private Texture2D floor;

    public void ShowRequirements () {
        floor = (Texture2D)EditorGUILayout.ObjectField ("Floor", floor, typeof(Texture2D), false);
    }

    public void Set (Context c, IFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Floor.prefab"), pos * 8, Quaternion.identity) as GameObject;
        gameObject.name = "Floor";
        gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (floor.name) [c.isGradientUsing ? GradientProvider.GetIndex ((f.IsExists(c, new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(c, new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(c, new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(c, new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0) |
            (f.IsExists(c, new Vector2(pos.x + 1, pos.y + 1)) ? GradientProvider.first : 0) |
            (f.IsExists(c, new Vector2(pos.x - 1, pos.y + 1)) ? GradientProvider.second : 0) |
            (f.IsExists(c, new Vector2(pos.x - 1, pos.y - 1)) ? GradientProvider.third : 0) |
            (f.IsExists(c, new Vector2(pos.x + 1, pos.y - 1)) ? GradientProvider.fourth : 0)) : 13];
        gameObject.transform.SetParent (GameObject.Find ("/Floor1").transform);
    }
}

class FenceSetter : RawSetter {
    private Texture2D top, side;

    public void ShowRequirements () {
        top = (Texture2D)EditorGUILayout.ObjectField ("Top", top, typeof(Texture2D), false);
        side = (Texture2D)EditorGUILayout.ObjectField ("Side", side, typeof(Texture2D), false);
    }

    public void Set (Context c, IFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Fence.prefab"), pos * 8, Quaternion.identity) as GameObject;
        gameObject.name = "Fence";
        gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(top.name)[c.isGradientUsing ? GradientProvider.GetIndex ((f.IsExists(c, new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(c, new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(c, new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(c, new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0)) : 13];
        if (f.IsExists(c, new Vector2(pos.x, pos.y - 1))) gameObject.transform.FindChild ("Forward").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [15];
        gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
    }
}

class Destroyer : RawSetter {
    public void ShowRequirements () {}

    public void Set (Context c, IFiller f, Vector2 pos) {
        foreach (GameObject go in GameObject.FindObjectsOfType <GameObject> ())
            if (go.transform.position == new Vector3 ((pos * 8).x, (pos * 8).y, 0) && (go.name == "Floor" || go.name == "Wall"))
                GameObject.DestroyImmediate (go);
    }
}

public interface IFiller {
    void Call (Context c, RawSetter rs);
    bool IsExists (Context c, Vector2 pos);
}

class Filler : IFiller {
    public void Call (Context c, RawSetter rs) {
        for (int x = 0; x < c.ghostSize.x; x++)
            for (int y = 0; y < c.ghostSize.y; y++)
                rs.Set (c, this, new Vector2 (c.ghostPosition.x + x, c.ghostPosition.y + y));
    }

    public bool IsExists (Context c, Vector2 pos) {
        return pos.x >= c.ghostPosition.x && pos.x < c.ghostPosition.x + c.ghostSize.x && pos.y >= c.ghostPosition.y && pos.y < c.ghostPosition.y + c.ghostSize.y;
    }
}

class Drawer : IFiller {
    public void Call (Context c, RawSetter rs) {
        for (int x = 0; x < c.ghostSize.x; x++) {
            rs.Set (c, this, new Vector2(c.ghostPosition.x + x, c.ghostPosition.y));
            rs.Set (c, this, new Vector2(c.ghostPosition.x + x, c.ghostPosition.y + c.ghostSize.y - 1));
        }
        for (int y = 1; y < c.ghostSize.y - 1; y++) {
            rs.Set (c, this, new Vector2(c.ghostPosition.x, c.ghostPosition.y + y));
            rs.Set (c, this, new Vector2(c.ghostPosition.x + c.ghostSize.x - 1, c.ghostPosition.y + y));
        }
    }

    public bool IsExists (Context c, Vector2 pos) {
        return !((pos.x < c.ghostPosition.x || pos.x > c.ghostPosition.x + c.ghostSize.x - 1 || pos.y < c.ghostPosition.y || pos.y > c.ghostPosition.y + c.ghostSize.y - 1) ||
            (pos.x > c.ghostPosition.x && pos.x < c.ghostPosition.x + c.ghostSize.x - 1 && pos.y > c.ghostPosition.y && pos.y < c.ghostPosition.y + c.ghostSize.y - 1));
    }
}
