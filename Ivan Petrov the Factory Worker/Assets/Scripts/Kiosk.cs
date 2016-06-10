using UnityEngine;
using System.Collections;

public class Kiosk : Backlight {
    protected override void Action ()
    {
        Debug.Log ("Bread: " + (GameManagement.gameData.inventory[0] += GameManagement.gameData.tickets [0]));
        GameManagement.gameData.tickets [0] = 0;
    }
}
