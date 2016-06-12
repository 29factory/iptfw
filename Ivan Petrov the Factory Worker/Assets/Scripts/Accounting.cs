using UnityEngine;
using System.Collections;

public class Accounting : Backlight {
    protected override void Action (int act)
    {
        Debug.Log ("Tickets for bread: " + (GameManagement.gameData.tickets [0] += GameManagement.gameData.products [0]));
        Debug.Log ("Tickets for vodka: " + (GameManagement.gameData.tickets [1] += GameManagement.gameData.products [1]));
        GameManagement.gameData.products [0] = 0;
        GameManagement.gameData.products [1] = 0;
    }

    protected override string GetTitle ()
    {
        return "Accounting";
    }
}
