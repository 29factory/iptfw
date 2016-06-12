using UnityEngine;
using System.Collections;

public class Kiosk : Backlight {
    protected override void Action (int act)
    {
        switch (act) {
        case 0:
            if (GameManagement.gameData.tickets [0] > 0)
                Debug.Log ("Bread: " + ++GameManagement.gameData.inventory [0]);
            GameManagement.gameData.tickets [0]--;
            break;
        case 1:
            Debug.Log ("Bread: " + (GameManagement.gameData.inventory [0] += GameManagement.gameData.tickets [0]));
            GameManagement.gameData.tickets [0] = 0;
            break;
        case 2:
            if (GameManagement.gameData.tickets [1] > 0)
                Debug.Log ("Vodka: " + ++GameManagement.gameData.inventory [1]);
            GameManagement.gameData.tickets [1]--;
            break;
        case 3:
            Debug.Log ("Vodka: " + (GameManagement.gameData.inventory [1] += GameManagement.gameData.tickets [1]));
            GameManagement.gameData.tickets [1] = 0;
            break;
        }
    }

    protected override string GetTitle ()
    {
        return "Kiosk";
    }
}
