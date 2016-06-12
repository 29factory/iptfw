using System;
using UnityEngine;

[Serializable]
public sealed class GameData {
    public Regime regime;

    public float health;
    public float satiety;
    public float happiness;
    public float drunkeness;
    public float patriotism;

    public SceneBetweener betweener;

    private GameData (Regime r, Location l, VectorData p) {
        health = 100;
        satiety = 80;
        happiness = 40;
        drunkeness = 0;
        patriotism = 40;
        regime = r;
        betweener = new SceneBetweener ();
        betweener.location = l;
        betweener.appearAt = p;
        betweener.direction = new VectorData (0, -1);
    }

    public static GameData Create (VectorData p, Regime r = Regime.Zheleznov, Location l = Location.Home) {
        return new GameData (r, l, p);
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
    Health,
    Satiety,
    Happiness,
    Drunkeness,
    Patriotism
}

public enum StatCondition {
    LessThan20,
    MoreThan80,
    Zero,
    Hundred
}

[Serializable]
public sealed class VectorData {
    public float x, y, z;

    public VectorData (float x, float y, float z = 0) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public VectorData (Vector3 v) {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public VectorData (Vector2 v) {
        x = v.x;
        y = v.y;
    }

    public static implicit operator VectorData (Vector3 v) {
        return new VectorData (v);
    }

    public static implicit operator Vector3 (VectorData sv) {
        return new Vector3(sv.x, sv.y, sv.z);
    }

    public static implicit operator VectorData (Vector2 v) {
        return new VectorData (v);
    }

    public static implicit operator Vector2 (VectorData sv) {
        return new Vector2(sv.x, sv.y);
    }
}

[Serializable]
public sealed class SceneBetweener {
    public Location location;
    public VectorData appearAt;
    public VectorData direction;
}