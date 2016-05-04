using UnityEngine;
using UnityEditor;
using System.Collections;

public abstract class AbstractFiller {
    public Vector2 ghostPosition = new Vector2(0, 0);
    public float scaleFactor = 8;

    public virtual void ShowRequirements () {
        scaleFactor = EditorGUILayout.FloatField ("Scale Factor", scaleFactor);
        ghostPosition = EditorGUILayout.Vector2Field ("Position", ghostPosition);
    }

    public abstract void Call (AbstractSetter rs);
    public abstract bool IsExists (Vector2 pos);
}

public abstract class AreaFiller : AbstractFiller {
    public Vector2 ghostSize = new Vector2(1, 1);

    public override void ShowRequirements () {
        base.ShowRequirements ();
        ghostSize = EditorGUILayout.Vector2Field ("Size", ghostSize);
    }
}

class Dotter : AbstractFiller {
    public override void Call (AbstractSetter rs) {
        rs.Set (this, new Vector2 (ghostPosition.x, ghostPosition.y));
    }

    public override bool IsExists (Vector2 pos) {
        return pos == ghostPosition;
    }
}

class Filler : AreaFiller {
    public override void Call (AbstractSetter rs) {
        for (int x = 0; x < ghostSize.x; x++)
            for (int y = 0; y < ghostSize.y; y++)
                rs.Set (this, new Vector2 (ghostPosition.x + x, ghostPosition.y + y));
    }

    public override bool IsExists (Vector2 pos) {
        return pos.x >= ghostPosition.x && pos.x < ghostPosition.x + ghostSize.x && pos.y >= ghostPosition.y && pos.y < ghostPosition.y + ghostSize.y;
    }
}

class Drawer : AreaFiller {
    public override void Call (AbstractSetter rs) {
        for (int x = 0; x < ghostSize.x; x++) {
            rs.Set (this, new Vector2(ghostPosition.x + x, ghostPosition.y));
            rs.Set (this, new Vector2(ghostPosition.x + x, ghostPosition.y + ghostSize.y - 1));
        }
        for (int y = 1; y < ghostSize.y - 1; y++) {
            rs.Set (this, new Vector2(ghostPosition.x, ghostPosition.y + y));
            rs.Set (this, new Vector2(ghostPosition.x + ghostSize.x - 1, ghostPosition.y + y));
        }
    }

    public override bool IsExists (Vector2 pos) {
        return !((pos.x < ghostPosition.x || pos.x > ghostPosition.x + ghostSize.x - 1 || pos.y < ghostPosition.y || pos.y > ghostPosition.y + ghostSize.y - 1) ||
            (pos.x > ghostPosition.x && pos.x < ghostPosition.x + ghostSize.x - 1 && pos.y > ghostPosition.y && pos.y < ghostPosition.y + ghostSize.y - 1));
    }
}