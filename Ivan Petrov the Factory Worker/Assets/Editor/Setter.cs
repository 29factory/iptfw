using UnityEngine;
using UnityEditor;

public abstract class AbstractSetter {
    protected bool isGradientUsing = true;

    public virtual void ShowRequirements () {
        isGradientUsing = GUILayout.Toggle (isGradientUsing, "Use gradient technology");
    }
    public abstract void Set (AbstractFiller f, Vector2 pos);
}

class WallSetter : AbstractSetter {
    private Texture2D top, side;

    public override void ShowRequirements () {
        top = (Texture2D)EditorGUILayout.ObjectField ("Top", top, typeof(Texture2D), false);
        side = (Texture2D)EditorGUILayout.ObjectField ("Side", side, typeof(Texture2D), false);
        base.ShowRequirements ();
    }

    public override void Set (AbstractFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), pos * 8, Quaternion.identity) as GameObject;
        gameObject.name = "Wall";
        gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(top.name)[isGradientUsing ? GradientProvider.GetIndex (((f.IsExists(new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0)) | 240) : 13];
        gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [13];
        gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
    }
}

class FloorSetter : AbstractSetter {
    private Texture2D floor;

    public override void ShowRequirements () {
        floor = (Texture2D)EditorGUILayout.ObjectField ("Floor", floor, typeof(Texture2D), false);
        base.ShowRequirements ();
    }

    public override void Set (AbstractFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Floor.prefab"), pos * 8, Quaternion.identity) as GameObject;
        gameObject.name = "Floor";
        gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (floor.name) [isGradientUsing ? GradientProvider.GetIndex ((f.IsExists(new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0) |
            (f.IsExists(new Vector2(pos.x + 1, pos.y + 1)) ? GradientProvider.first : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y + 1)) ? GradientProvider.second : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y - 1)) ? GradientProvider.third : 0) |
            (f.IsExists(new Vector2(pos.x + 1, pos.y - 1)) ? GradientProvider.fourth : 0)) : 13];
        gameObject.transform.SetParent (GameObject.Find ("/Floor1").transform);
    }
}

class FenceSetter : AbstractSetter {
    private Texture2D top, side;

    public override void ShowRequirements () {
        top = (Texture2D)EditorGUILayout.ObjectField ("Top", top, typeof(Texture2D), false);
        side = (Texture2D)EditorGUILayout.ObjectField ("Side", side, typeof(Texture2D), false);
        base.ShowRequirements ();
    }

    public override void Set (AbstractFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Fence.prefab"), pos * 8, Quaternion.identity) as GameObject;
        gameObject.name = "Fence";
        gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(top.name)[isGradientUsing ? GradientProvider.GetIndex ((f.IsExists(new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0)) : 13];
        if (f.IsExists(new Vector2(pos.x + 1, pos.y)) && f.IsExists(new Vector2(pos.x - 1, pos.y))) gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [13];
        else if (f.IsExists(new Vector2(pos.x + 1, pos.y))) gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [12];
        else if (f.IsExists(new Vector2(pos.x - 1, pos.y))) gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [14];
        if (f.IsExists(new Vector2(pos.x, pos.y - 1))) gameObject.transform.FindChild ("Forward").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [15];
        gameObject.transform.SetParent (GameObject.Find ("/Walls1").transform);
    }
}

class Destroyer : AbstractSetter {
    public override void ShowRequirements () {}

    public override void Set (AbstractFiller f, Vector2 pos) {
        foreach (GameObject go in GameObject.FindObjectsOfType <GameObject> ())
            if (go.transform.position == new Vector3 ((pos * 8).x, (pos * 8).y, 0) && (go.name == "Floor" || go.name == "Wall"))
                GameObject.DestroyImmediate (go);
    }
}