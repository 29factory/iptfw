using UnityEngine;
using System.Collections;

public class Bench : Backlight {
    protected override void Action (int act) {
        GameManagement.gameData.products [act]++;
        Debug.Log ((act == 0 ? "Nuclear missile shells" : "New Year decorations") + ": " + GameManagement.gameData.products[act]);
    }

    protected override string GetTitle ()
    {
        return "Bench";
    }
}
