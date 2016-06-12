using UnityEngine;
using System.Collections;

public abstract class Backlight : Interactable {
    protected override void OnMouseEnter () {
        GetComponent<Renderer> ().material.color = Color.red;
        mouseOverThis = true;
    }

    protected override void OnMouseExit () {
        GetComponent<Renderer> ().material.color = Color.white;
        mouseOverThis = false;
    }

    void Update () {
        if (showActions)
        if (Input.GetMouseButtonDown (0))
        if (!new Rect (lastClick.x, Screen.height - lastClick.y, 200, 100).Contains (new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)) && (Vector2) Input.mousePosition != lastClick)
            //Debug.Log ("");
            showActions = false;
    }

    void OnGUI () {
        if (showActions) {
            GUI.BeginGroup (new Rect(lastClick.x, Screen.height - lastClick.y, 200, 100), "Actions");
            for (int i = 0; i < actions.Length; i++) {
                if (GUI.Button (new Rect (new Vector2(0, 20 + 20 * i), new GUIStyle().CalcSize(new GUIContent(actions[i]))), actions [i]))
                    Action (i);
            }
            GUI.EndGroup ();
        } else if (mouseOverThis) {
            GUIStyle s = new GUIStyle ();
            s.alignment = TextAnchor.LowerCenter;
            Vector2 size = s.CalcSize (new GUIContent (GetTitle ()));
            //EditorGUI.DrawRect (new Rect (new Vector2 (Input.mousePosition.x - size.x / 2, Screen.height - Input.mousePosition.y - size.y), size), new Color (1, 1, 1, 0.5f));
            GUI.Label (new Rect (new Vector2 (Input.mousePosition.x - size.x / 2, Screen.height - Input.mousePosition.y - size.y), size), GetTitle (), s);
        }
    }
}
