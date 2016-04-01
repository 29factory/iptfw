using UnityEngine;
using System.Collections;

public class RegimeSelector : MonoBehaviour {
    public void Select(string r) {
        GameObject.Find ("GameManagement").GetComponent<GameManagement> ().Play (r);
    }
}
