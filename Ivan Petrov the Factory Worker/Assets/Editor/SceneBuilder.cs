using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

public class SceneBuilder : EditorWindow {
    public static Vector2 ghostPosition = new Vector2(0, 0);
    public static Vector2 ghostSize = new Vector2(1, 1);
    private static int fillType = 0;
    private static Texture2D tex1, tex2;
    private static bool isGradientUsing = true;

    [MenuItem("Window/Scene Builder")]
    public static void ShowWindow () {
        GetWindowWithRect<SceneBuilder> (new Rect (0, 0, 200, 400), false, "Scene Builder");
    }

    void OnGUI () {
        ghostPosition = EditorGUILayout.Vector2Field ("Position", ghostPosition);
        ghostSize = EditorGUILayout.Vector2Field ("Size", ghostSize);
        switch (fillType = GUILayout.Toolbar (fillType, new string[]{ "Floor", "Wall" })) {
        default:
            tex1 = (Texture2D) EditorGUILayout.ObjectField ("Floor", tex1, typeof(Texture2D), false);
            break;
        case 1:
            tex1 = (Texture2D) EditorGUILayout.ObjectField ("Top", tex1, typeof(Texture2D), false);
            tex2 = (Texture2D) EditorGUILayout.ObjectField ("Side", tex2, typeof(Texture2D), false);
            break;
        }
        isGradientUsing = GUILayout.Toggle (isGradientUsing, "Use gradient technology");
        GUILayout.BeginHorizontal ();
        if (GUILayout.Button ("Build")) {
            if (fillType == 0) {
                for (int x = 0; x < ghostSize.x; x++)
                    for (int y = 0; y < ghostSize.y; y++) {
                        GameObject gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Floor.prefab"), new Vector2(ghostPosition.x + x, ghostPosition.y + y) * 8, Quaternion.identity) as GameObject;
                        gameObject.name = "Floor";
                        gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (tex1.name) [isGradientUsing ? GradientProvider.GetIndex ((((x == ghostSize.x - 1 ? GradientProvider.right : 0) |
                                                                                                                                                                  (y == ghostSize.y - 1 ? GradientProvider.top : 0) |
                                                                                                                                                                  (x == 0 ? GradientProvider.left : 0) |
                                                                                                                                                                  (y == 0 ? GradientProvider.bottom : 0)) ^ 15) | 240) : 13];
                        gameObject.transform.SetParent (GameObject.Find ("/Floor1").transform);
                    }
            } else {
                for (int x = 0; x < ghostSize.x; x++) {
                    GameObject gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x + x, ghostPosition.y) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(tex1.name)[isGradientUsing ? GradientProvider.GetIndex ((GradientProvider.bottom |
                                                                                                                                                                       (x > 0 && x < ghostSize.x-1 ? GradientProvider.top : 0) |
                                                                                                                                                                       (x == 0 ? GradientProvider.left : 0) |
                                                                                                                                                                       (x == ghostSize.x-1 ? GradientProvider.right : 0)) ^ 15) : 13];
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (tex2.name) [13];
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                    gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x + x, ghostPosition.y + ghostSize.y - 1) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(tex1.name)[isGradientUsing ? GradientProvider.GetIndex ((GradientProvider.top |
                                                                                                                                                                       (x > 0 && x < ghostSize.x-1 ? GradientProvider.bottom : 0) |
                                                                                                                                                                       (x == 0 ? GradientProvider.left : 0) |
                                                                                                                                                                       (x == ghostSize.x-1 ? GradientProvider.right : 0)) ^ 15) : 13];
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (tex2.name) [13];
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                }
                for (int y = 1; y < ghostSize.y - 1; y++) {
                    GameObject gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x, ghostPosition.y + y) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(tex1.name)[isGradientUsing ? GradientProvider.GetIndex (GradientProvider.top | GradientProvider.bottom) : 13];
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (tex2.name) [13];
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                    gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x + ghostSize.x - 1, ghostPosition.y + y) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(tex1.name)[isGradientUsing ? GradientProvider.GetIndex (GradientProvider.top | GradientProvider.bottom) : 13];
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (tex2.name) [13];
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
