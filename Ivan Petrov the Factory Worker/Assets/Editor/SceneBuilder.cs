using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

public class SceneBuilder : EditorWindow {
    public static Vector2 ghostPosition = new Vector2(0, 0);
    public static Vector2 ghostSize = new Vector2(1, 1);
    private static int fillType = 0;
    private static Sprite sprite1, sprite2;

    [MenuItem("Window/Test Gradient")]
    public static void TestGradient () {
        GameObject g = new GameObject ();//Instantiate (new GameObject(), new Vector3(-100, -100), Quaternion.identity) as GameObject;
        g.AddComponent<SpriteRenderer> ();
        SpriteRenderer s = g.GetComponent<SpriteRenderer> ();
        // TODO: это работает не так. Плодит текстуру для каждого инстанса. не загружать текстуру из ассетов а прикреплять к строителю
        GradientProvider.GetSprite (ref s, AssetDatabase.LoadAllAssetsAtPath ("Assets/Sprites/Grass.png").Select (x => x as Texture2D).ToArray(), 15);
    }

    [MenuItem("Window/Scene Builder")]
    public static void ShowWindow () {
        GetWindowWithRect<SceneBuilder> (new Rect (0, 0, 200, 400), false, "Scene Builder");
    }

    void OnGUI () {
        ghostPosition = EditorGUILayout.Vector2Field ("Position", ghostPosition);
        ghostSize = EditorGUILayout.Vector2Field ("Size", ghostSize);
        switch (fillType = GUILayout.Toolbar (fillType, new string[]{ "Floor", "Wall" })) {
        default:
            sprite1 = (Sprite) EditorGUILayout.ObjectField ("Floor", sprite1, typeof(Sprite), false);
            break;
        case 1:
            sprite1 = (Sprite) EditorGUILayout.ObjectField ("Top", sprite1, typeof(Sprite), false);
            sprite2 = (Sprite) EditorGUILayout.ObjectField ("Side", sprite2, typeof(Sprite), false);
            break;
        }
        GUILayout.BeginHorizontal ();
        if (GUILayout.Button ("Build")) {
            if (fillType == 0) {
                for (int x = 0; x < ghostSize.x; x++)
                    for (int y = 0; y < ghostSize.y; y++) {
                        GameObject gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Floor.prefab"), new Vector2(ghostPosition.x + x, ghostPosition.y + y) * 8, Quaternion.identity) as GameObject;
                        gameObject.name = "Floor";
                        gameObject.GetComponent<SpriteRenderer> ().sprite = sprite1;
                        gameObject.transform.SetParent (GameObject.Find ("/Floor1").transform);
                    }
            } else {
                for (int x = 0; x < ghostSize.x; x++) {
                    GameObject gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x + x, ghostPosition.y) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = sprite1;
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = sprite2;
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                    gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x + x, ghostPosition.y + ghostSize.y - 1) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = sprite1;
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = sprite2;
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                }
                for (int y = 1; y < ghostSize.y - 1; y++) {
                    GameObject gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x, ghostPosition.y + y) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = sprite1;
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = sprite2;
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                    gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x + ghostSize.x - 1, ghostPosition.y + y) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = sprite1;
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = sprite2;
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                }
            }
        }
        if (GUILayout.Button ("Destroy")) {
            // TODO: Это работает неправильно
            if (fillType == 0) {
                for (int x = 0; x < ghostSize.x; x++)
                    for (int y = 0; y < ghostSize.y; y++) {
                        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject> ())
                            if (obj.GetComponent<Transform> ().position == new Vector3 (ghostPosition.x + x, ghostPosition.y + y, 0) * 8)
                                DestroyImmediate (obj);
                    }
            } else {
                for (int x = 0; x < ghostSize.x; x++) {
                    foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject> ())
                        if (obj.GetComponent<Transform> ().position == new Vector3 (ghostPosition.x + x, ghostPosition.y, 0) * 8)
                            DestroyImmediate (obj);
                    foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject> ())
                        if (obj.GetComponent<Transform> ().position == new Vector3 (ghostPosition.x + x, ghostPosition.y + ghostSize.y - 1, 0) * 8)
                            DestroyImmediate (obj);
                }
                for (int y = 1; y < ghostSize.y - 1; y++) {
                    foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject> ())
                        if (obj.GetComponent<Transform> ().position == new Vector3 (ghostPosition.x, ghostPosition.y + y, 0) * 8)
                            DestroyImmediate (obj);
                    foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject> ())
                        if (obj.GetComponent<Transform> ().position == new Vector3 (ghostPosition.x + ghostSize.x - 1, ghostPosition.y + y, 0) * 8)
                            DestroyImmediate (obj);
                }
            }
        }
        GUILayout.EndHorizontal ();
    }

    void OnSceneGUI (SceneView scene) {
        Handles.DrawLine (new Vector3(ghostPosition.x - 0.5f, ghostPosition.y - 0.5f) * 8, (new Vector3(ghostPosition.x - 0.5f, ghostPosition.y - 0.5f) + new Vector3(ghostSize.x, ghostSize.y)) * 8);
        Handles.DrawSolidRectangleWithOutline (new Rect(new Vector2(ghostPosition.x - 0.5f, ghostPosition.y - 0.5f) * 8, new Vector2(ghostSize.x, ghostSize.y) * 8), Color.clear, Color.white);
    }

    void OnEnable () {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    void OnDisable () {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }
}
