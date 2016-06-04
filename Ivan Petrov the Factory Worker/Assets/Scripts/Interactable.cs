using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {
    protected abstract void Action ();
    protected abstract void OnMouseEnter ();
    protected abstract void OnMouseExit ();
	
    void OnMouseDown () {
        Action ();
    }
}
