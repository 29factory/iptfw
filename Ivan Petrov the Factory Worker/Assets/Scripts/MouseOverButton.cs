using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseOverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerEnter (PointerEventData e) {
        GetComponentInChildren<Text> ().fontStyle = FontStyle.Bold;
    }

    public void OnPointerExit (PointerEventData e) {
        GetComponentInChildren<Text> ().fontStyle = FontStyle.Normal;
    }
}
