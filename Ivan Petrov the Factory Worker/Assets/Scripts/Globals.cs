using System.Collections.Generic;
using System;

public static class Globals {
    public static readonly string savingPath = UnityEngine.Application.persistentDataPath;
    public static readonly string savingExt = ".whyitshouldbesomethingshort";

    public static readonly Dictionary<Stat, Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>>> statEffectGraph;

    static Globals () {
        statEffectGraph = new Dictionary<Stat, Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>>> ();
        statEffectGraph [Stat.Health] = new Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>> ();
        statEffectGraph [Stat.Health] [StatCondition.MoreThan80] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Health] [StatCondition.MoreThan80] [Stat.Happiness] = (p) => p + 1;
        statEffectGraph [Stat.Health] [StatCondition.MoreThan80] [Stat.Patriotism] = p => p + 5;
        statEffectGraph [Stat.Health] [StatCondition.LessThan20] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Health] [StatCondition.LessThan20] [Stat.Happiness] = p => p - 3;
        statEffectGraph [Stat.Health] [StatCondition.LessThan20] [Stat.Patriotism] = p => p - 3;
        statEffectGraph [Stat.Satiety] = new Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>> ();
        statEffectGraph [Stat.Satiety] [StatCondition.MoreThan80] [Stat.Health] = p => p + 3;
        statEffectGraph [Stat.Satiety] [StatCondition.MoreThan80] [Stat.Happiness] = p => p + 2;
        statEffectGraph [Stat.Satiety] [StatCondition.MoreThan80] [Stat.Patriotism] = p => p + 4;
        statEffectGraph [Stat.Satiety] [StatCondition.LessThan20] [Stat.Health] = p => p - 2;
        statEffectGraph [Stat.Satiety] [StatCondition.LessThan20] [Stat.Happiness] = p => p - 2;
        statEffectGraph [Stat.Satiety] [StatCondition.LessThan20] [Stat.Patriotism] = p => p - 4;
        statEffectGraph [Stat.Drunkeness] = new Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>> ();
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] [Stat.Health] = p => p - 4;
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] [Stat.Happiness] = p => p + 8;
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] [Stat.Satiety] = p => p - 2;
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] [Stat.Patriotism] = p => p + 11;
        statEffectGraph [Stat.Drunkeness] [StatCondition.LessThan20] [Stat.Happiness] = p => p - 3;
        statEffectGraph [Stat.Drunkeness] [StatCondition.LessThan20] [Stat.Patriotism] = p => p - 4;
        statEffectGraph [Stat.Patriotism] = new Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>> ();
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] [Stat.Happiness] = p => p + 4;
        statEffectGraph [Stat.Drunkeness] [StatCondition.LessThan20] [Stat.Happiness] = p => p - 3;
    }
}