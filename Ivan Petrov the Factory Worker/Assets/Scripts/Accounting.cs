using UnityEngine;
using System.Collections;

public class Accounting : Backlight {
    protected override void Action ()
    {
        Debug.Log ("Tickets for bread: " + (GameManagement.gameData.tickets [0] += GameManagement.gameData.products [1]));
        GameManagement.gameData.products [1] = 0;
    }
}
