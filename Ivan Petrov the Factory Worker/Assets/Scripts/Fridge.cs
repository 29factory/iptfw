using UnityEngine;
using System.Collections;

public class Fridge : Backlight {
    protected override void Action (int act)
    {
        switch (act) {
        case 0:
            GameManagement.gameData.stats [2] += GameManagement.gameData.inventory [0] * 2;
            GameManagement.gameData.inventory [0] = 0;
            break;
        case 1:
            GameManagement.gameData.stats [3] += GameManagement.gameData.inventory [1] * 2;
            GameManagement.gameData.inventory [1] = 0;
            break;
        }
    }

    protected override string GetTitle ()
    {
        return "Fridge";
    }
}
