using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

public class SceneBuilder : EditorWindow {
    public static Vector2 ghostPosition = new Vector2(0, 0);
    public static Vector2 ghostSize = new Vector2(1, 1);
    private static int fillType = 0;
    private static Texture2D tex1;
    private static Sprite tex2;

    /*[MenuItem("Window/Test Gradient")]
    public static void TestGradient () {
        GameObject g = new GameObject ();
        g.transform.position = new Vector3 (-40, -40, 0);
        g.AddComponent<SpriteRenderer> ();
        SpriteRenderer r = g.GetComponent<SpriteRenderer> ();
        GradientProvider.SetSprite (ref r, Resources.LoadAll<Sprite> ("Grass"), GradientProvider.top | GradientProvider.bottom | GradientProvider.left | GradientProvider.right);
        //Debug.Log (AssetDatabase.LoadAssetAtPath<Sprite> ("Assets/Sprites/Grass.png/grass_0"));
        //Debug.Log (GameObject.Find ("New Game Object").GetComponent<SpriteRenderer> ().sprite.bounds);
        //Debug.Log (GameObject.Find ("New Game Object").GetComponent<SpriteRenderer> ().sprite.name);
        //Debug.Log (GameObject.Find ("New Game Object").GetComponent<SpriteRenderer> ().sprite.pivot);
        //Debug.Log (GameObject.Find ("New Game Object").GetComponent<SpriteRenderer> ().sprite.pixelsPerUnit);
        //Debug.Log (GameObject.Find ("New Game Object").GetComponent<SpriteRenderer> ().sprite.rect);
        // SOLVED: это работает не так. Плодит текстуру для каждого инстанса. не загружать текстуру из ассетов а прикреплять к строителю
        // NOTE: избавиться от зависимости от Resources (мб)
    }*/

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
            tex2 = (Sprite) EditorGUILayout.ObjectField ("Side", tex2, typeof(Sprite), false);
            break;
        }
        GUILayout.BeginHorizontal ();
        if (GUILayout.Button ("Build")) {
            if (fillType == 0) {
                for (int x = 0; x < ghostSize.x; x++)
                    for (int y = 0; y < ghostSize.y; y++) {
                        GameObject gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Floor.prefab"), new Vector2(ghostPosition.x + x, ghostPosition.y + y) * 8, Quaternion.identity) as GameObject;
                        gameObject.name = "Floor";
                        SpriteRenderer s = gameObject.GetComponent<SpriteRenderer> ();
                        GradientProvider.SetSprite (ref s, Resources.LoadAll<Sprite>(tex1.name), ((x == ghostSize.x-1 ? GradientProvider.right : 0) | (y == ghostSize.y-1 ? GradientProvider.top : 0) | (x == 0 ? GradientProvider.left : 0) | (y == 0 ? GradientProvider.bottom : 0)) ^ 15);
                        gameObject.transform.SetParent (GameObject.Find ("/Floor1").transform);
                    }
            } else {
                for (int x = 0; x < ghostSize.x; x++) {
                    GameObject gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x + x, ghostPosition.y) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    SpriteRenderer s = gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ();
                    GradientProvider.SetSprite (ref s, Resources.LoadAll<Sprite>(tex1.name), (GradientProvider.bottom | (x > 0 && x < ghostSize.x-1 ? GradientProvider.top : 0) | (x == 0 ? GradientProvider.left : 0) | (x == ghostSize.x-1 ? GradientProvider.right : 0)) ^ 15);
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = tex2;
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                    gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x + x, ghostPosition.y + ghostSize.y - 1) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    s = gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ();
                    GradientProvider.SetSprite (ref s, Resources.LoadAll<Sprite>(tex1.name), (GradientProvider.top | (x > 0 && x < ghostSize.x-1 ? GradientProvider.bottom : 0) | (x == 0 ? GradientProvider.left : 0) | (x == ghostSize.x-1 ? GradientProvider.right : 0)) ^ 15);
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = tex2;
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                }
                for (int y = 1; y < ghostSize.y - 1; y++) {
                    GameObject gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x, ghostPosition.y + y) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    SpriteRenderer s = gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ();
                    GradientProvider.SetSprite (ref s, Resources.LoadAll<Sprite>(tex1.name), GradientProvider.top | GradientProvider.bottom);
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = tex2;
                    gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
                    gameObject = Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), new Vector2(ghostPosition.x + ghostSize.x - 1, ghostPosition.y + y) * 8, Quaternion.identity) as GameObject;
                    gameObject.name = "Wall";
                    s = gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ();
                    GradientProvider.SetSprite (ref s, Resources.LoadAll<Sprite>(tex1.name), GradientProvider.top | GradientProvider.bottom);
                    gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = tex2;
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
