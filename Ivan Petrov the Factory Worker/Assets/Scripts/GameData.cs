using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public sealed class GameData {
    public Regime regime;

    public float[] stats = new float[] { 100, 80, 40, 0, 40 }, oldStats = new float[] { 100, 80, 40, 0, 40 };
    public int[] products = new int[] { 0, 0 };

    public SceneBetweener betweener;

    private GameData (Regime r, Location l, VectorData p) {
        regime = r;
        betweener = new SceneBetweener ();
        betweener.location = l;
        betweener.appearAt = p;
    }

    public static GameData Create (VectorData p, Regime r = Regime.Zheleznov, Location l = Location.Home) {
        return new GameData (r, l, p);
    }

    public static StatCondition GetCondition(float value) {
        /*if (value >= 100)
            return StatCondition.Hundred;
        else*/ if (value >= 80)
            return StatCondition.MoreThan80;
        /*else if (value <= 0)
            return StatCondition.Zero;
        */else if (value <= 20)
            return StatCondition.LessThan20;
        else
            return StatCondition.Normal;
    }

    public void CalculateStats () {
        foreach (int s in Enum.GetValues(typeof(Stat)))
            oldStats [s] = stats [s];

        foreach (Stat s in Enum.GetValues(typeof(Stat))) {
            if (Globals.statEffectGraph.ContainsKey (s))
            if (Globals.statEffectGraph [s].ContainsKey (GetCondition (oldStats [(int)s])))
                foreach (var p in Globals.statEffectGraph[s][GetCondition(oldStats[(int) s])]) {
                    stats [(int) p.Key] = p.Value (stats [(int) p.Key]);
                }
        }

        foreach (int s in Enum.GetValues(typeof(Stat)))
            stats [s] -= .2f;

        foreach (Stat s in Enum.GetValues(typeof(Stat)))
            GameObject.Find (s.ToString ()).transform.GetChild (0).GetComponent<Slider> ().value = stats [(int) s];
    }
}

public enum Regime {
    Zheleznov,
    Brevnov,
    Chaikovsky,
    Kucherov
}

public enum Location {
    Home,
    Outdoors,
    Factory,
    NKUSOffice,
    Market
}

public enum Stat {
    Health = 0,
    Satiety = 1,
    Happiness = 2,
    Drunkeness = 3,
    Patriotism = 4
}

public enum StatCondition {
    //Zero = 0,
    LessThan20 = 1,
    Normal = 2,
    MoreThan80 = 3,
    //Hundred = 4
}

public enum Product {
    NuclearMissileShell = 0,
    NewYearDecorations = 1
}

[Serializable]
public sealed class VectorData {
    public float x, y, z;

    public static implicit operator VectorData (Vector3 v) {
        VectorData sv = new VectorData ();
        sv.x = v.x;
        sv.y = v.y;
        sv.z = v.z;
        return sv;
    }

    public static implicit operator Vector3 (VectorData sv) {
        return new Vector3(sv.x, sv.y, sv.z);
    }

    public static implicit operator VectorData (Vector2 v) {
        VectorData sv = new VectorData ();
        sv.x = v.x;
        sv.y = v.y;
        return sv;
    }

    public static implicit operator Vector2 (VectorData sv) {
        return new Vector2(sv.x, sv.y);
    }
}

[Serializable]
public sealed class SceneBetweener {
    public Location location;
    public VectorData appearAt;
}