using UnityEngine;
using System;

[Serializable]
public class PlayOptions {
    public Regime regime;

    public Location location;
    public SerVector appearAt;

    public float health;
    public float satiety;
    public float happiness;
    public float drunkeness;
    public float patriotism;

    public PlayOptions (Regime r) {
        health = 100;
        satiety = 80;
        happiness = 40;
        drunkeness = 0;
        patriotism = 40;
        regime = r;
        location = Location.Home;
        appearAt = new Vector3 (0, 0, 0);
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

[Serializable]
public class SerVector {
    float x, y, z;

    public static implicit operator SerVector (Vector3 v) {
        SerVector sv = new SerVector ();
        sv.x = v.x;
        sv.y = v.y;
        sv.z = v.z;
        return sv;
    }

    public static implicit operator Vector3 (SerVector sv) {
        return new Vector3(sv.x, sv.y, sv.z);
    }
}