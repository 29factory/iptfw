using UnityEngine;
using System.Collections;

public class Bench : Backlight {
    protected override void Action () {
        GameManagement.gameData.products [1]++;
        Debug.Log ("New Year decorations: " + GameManagement.gameData.products[1]);
    }
}
