using UnityEngine;
using System.Collections;

public class Fridge : Backlight {
    protected override void Action ()
    {
        GameManagement.gameData.stats [2] += GameManagement.gameData.inventory [0] * 2;
        GameManagement.gameData.inventory [0] = 0;
    }
}
