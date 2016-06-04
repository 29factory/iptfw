using UnityEngine;
using System.Collections;

public abstract class Backlight : Interactable {
    protected override void OnMouseEnter () {
        GetComponent<Renderer> ().material.color = Color.red;
    }

    protected override void OnMouseExit () {
        GetComponent<Renderer> ().material.color = Color.white;
    }
}
