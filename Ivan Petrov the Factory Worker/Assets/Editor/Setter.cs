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
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab"), pos * f.scaleFactor, Quaternion.identity) as GameObject;
        gameObject.name = "Wall";
        gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(top.name)[isGradientUsing ? GradientProvider.GetIndex (((f.IsExists(new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0))/* | 240*/) : 0];
        gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [0];
        gameObject.transform.SetParent (GameObject.Find ("/Walls").transform);
    }
}

class FloorSetter : AbstractSetter {
    private Texture2D floor;

    public override void ShowRequirements () {
        floor = (Texture2D)EditorGUILayout.ObjectField ("Floor", floor, typeof(Texture2D), false);
        base.ShowRequirements ();
    }

    public override void Set (AbstractFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Floor.prefab"), pos * f.scaleFactor, Quaternion.identity) as GameObject;
        gameObject.name = "Floor";
        gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (floor.name) [isGradientUsing ? GradientProvider.GetIndex ((f.IsExists(new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0) |
            (f.IsExists(new Vector2(pos.x + 1, pos.y + 1)) ? GradientProvider.first : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y + 1)) ? GradientProvider.second : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y - 1)) ? GradientProvider.third : 0) |
            (f.IsExists(new Vector2(pos.x + 1, pos.y - 1)) ? GradientProvider.fourth : 0)) : 0];
        gameObject.transform.SetParent (GameObject.Find ("/Floor").transform);
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
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Fence.prefab"), pos * f.scaleFactor, Quaternion.identity) as GameObject;
        gameObject.name = "Fence";
        gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(top.name)[isGradientUsing ? GradientProvider.GetIndex ((f.IsExists(new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0)) : 0];
        if (f.IsExists(new Vector2(pos.x + 1, pos.y)) && f.IsExists(new Vector2(pos.x - 1, pos.y))) gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [0];
        else if (f.IsExists(new Vector2(pos.x + 1, pos.y))) gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [12];
        else if (f.IsExists(new Vector2(pos.x - 1, pos.y))) gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [14];
        if (f.IsExists(new Vector2(pos.x, pos.y - 1))) gameObject.transform.FindChild ("Forward").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [15];
        gameObject.transform.SetParent (GameObject.Find ("/Walls").transform);
    }
}

class DoorToSetter : AbstractSetter {
    private Texture2D top, side;
    private string doorTo;
    private Vector2 appearAt;

    public override void ShowRequirements () {
        top = (Texture2D)EditorGUILayout.ObjectField ("Top", top, typeof(Texture2D), false);
        side = (Texture2D)EditorGUILayout.ObjectField ("Side", side, typeof(Texture2D), false);
        doorTo = EditorGUILayout.TextField ("Door to", doorTo);
        appearAt = EditorGUILayout.Vector2Field ("Appear at", appearAt);
        base.ShowRequirements ();
    }

    public override void Set (AbstractFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/DoorTo.prefab"), pos * f.scaleFactor, Quaternion.identity) as GameObject;
        gameObject.name = "DoorTo";
        gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(top.name)[isGradientUsing ? GradientProvider.GetIndex (((f.IsExists(new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0)) | 240) : 0];
        gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [0];
        gameObject.GetComponent<DoorTo> ().doorTo = doorTo;
        gameObject.GetComponent<DoorTo> ().appearAt = appearAt;
        gameObject.transform.SetParent (GameObject.Find ("/Walls").transform);
    }
}

class DoorSetter : AbstractSetter {
    private Texture2D top, side, topOfSide;

    public override void ShowRequirements () {
        top = (Texture2D)EditorGUILayout.ObjectField ("Top", top, typeof(Texture2D), false);
        side = (Texture2D)EditorGUILayout.ObjectField ("Side", side, typeof(Texture2D), false);
        topOfSide = (Texture2D)EditorGUILayout.ObjectField ("Top of side", topOfSide, typeof(Texture2D), false);
        base.ShowRequirements ();
    }

    public override void Set (AbstractFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Door.prefab"), pos * f.scaleFactor, Quaternion.identity) as GameObject;
        gameObject.name = "Door";
        gameObject.transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite>(top.name)[isGradientUsing ? GradientProvider.GetIndex (((f.IsExists(new Vector2(pos.x + 1, pos.y)) ? GradientProvider.right : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y + 1)) ? GradientProvider.top : 0) |
            (f.IsExists(new Vector2(pos.x - 1, pos.y)) ? GradientProvider.left : 0) |
            (f.IsExists(new Vector2(pos.x, pos.y - 1)) ? GradientProvider.bottom : 0)) | 240) : 0];
        gameObject.transform.FindChild ("Side").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (side.name) [0];
        gameObject.transform.FindChild ("TopOfSide").GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (topOfSide.name) [0];
        gameObject.transform.SetParent (GameObject.Find ("/Walls").transform);
    }
}

class OnWallSetter : AbstractSetter {
    private Texture2D tex;

    public override void ShowRequirements () {
        tex = (Texture2D)EditorGUILayout.ObjectField ("Sprite", tex, typeof(Texture2D), false);
    }

    public override void Set (AbstractFiller f, Vector2 pos) {
        GameObject gameObject = GameObject.Instantiate (AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/OnWall.prefab"), pos * f.scaleFactor, Quaternion.identity) as GameObject;
        gameObject.name = "MaybeWindowMaybeNot";
        gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (tex.name) [0];
        gameObject.transform.SetParent (GameObject.Find ("/Walls").transform);
    }
}

class Destroyer : AbstractSetter {
    public override void ShowRequirements () {}

    public override void Set (AbstractFiller f, Vector2 pos) {
        foreach (GameObject go in GameObject.FindObjectsOfType <GameObject> ())
            if (go.transform.position == new Vector3 ((pos * f.scaleFactor).x, (pos * f.scaleFactor).y, 0) && (go.name == "Floor" || go.name == "Wall"))
                GameObject.DestroyImmediate (go);
    }
}