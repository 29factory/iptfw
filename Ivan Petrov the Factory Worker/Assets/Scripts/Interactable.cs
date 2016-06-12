using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    protected bool mouseOverThis = false;
    protected bool showActions = false;
    protected Vector2 lastClick;

    public string[] actions;

    protected abstract void Action (int actNum);
    protected abstract void OnMouseEnter ();
    protected abstract void OnMouseExit ();
    protected abstract string GetTitle ();

    protected void OnMouseDown () {
        if (actions.Length <= 1)
            Action (0);
        else {
            showActions = true;
            lastClick = Input.mousePosition;
        }
    }
}
