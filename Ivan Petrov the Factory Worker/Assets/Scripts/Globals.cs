using System.Collections.Generic;
using System;

public static class Globals {
    public static readonly string savingPath = UnityEngine.Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + "Saves";
    public static readonly string savingExt = ".whyitshouldbesomethingshort";

    public static readonly Dictionary<Stat, Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>>> statEffectGraph;
    private const float factor = .1f;

    static Globals () {
        statEffectGraph = new Dictionary<Stat, Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>>> ();
        statEffectGraph [Stat.Health] = new Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>> ();
        statEffectGraph [Stat.Health] [StatCondition.MoreThan80] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Health] [StatCondition.MoreThan80] [Stat.Happiness] = (p) => p + 1 * factor;
        statEffectGraph [Stat.Health] [StatCondition.MoreThan80] [Stat.Patriotism] = (p) => p + 5 * factor;
        statEffectGraph [Stat.Health] [StatCondition.LessThan20] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Health] [StatCondition.LessThan20] [Stat.Happiness] = (p) => p - 3 * factor;
        statEffectGraph [Stat.Health] [StatCondition.LessThan20] [Stat.Patriotism] = (p) => p - 3 * factor;
        statEffectGraph [Stat.Satiety] = new Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>> ();
        statEffectGraph [Stat.Satiety] [StatCondition.MoreThan80] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Satiety] [StatCondition.MoreThan80] [Stat.Health] = (p) => p + 3 * factor;
        statEffectGraph [Stat.Satiety] [StatCondition.MoreThan80] [Stat.Happiness] = (p) => p + 2 * factor;
        statEffectGraph [Stat.Satiety] [StatCondition.MoreThan80] [Stat.Patriotism] = (p) => p + 4 * factor;
        statEffectGraph [Stat.Satiety] [StatCondition.LessThan20] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Satiety] [StatCondition.LessThan20] [Stat.Health] = (p) => p - 2 * factor;
        statEffectGraph [Stat.Satiety] [StatCondition.LessThan20] [Stat.Happiness] = (p) => p - 2 * factor;
        statEffectGraph [Stat.Satiety] [StatCondition.LessThan20] [Stat.Patriotism] = (p) => p - 4 * factor;
        statEffectGraph [Stat.Drunkeness] = new Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>> ();
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] [Stat.Health] = (p) => p - 4 * factor;
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] [Stat.Happiness] = (p) => p + 8 * factor;
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] [Stat.Satiety] = (p) => p - 2 * factor;
        statEffectGraph [Stat.Drunkeness] [StatCondition.MoreThan80] [Stat.Patriotism] = (p) => p + 11 * factor;
        statEffectGraph [Stat.Drunkeness] [StatCondition.LessThan20] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Drunkeness] [StatCondition.LessThan20] [Stat.Happiness] = (p) => p - 3 * factor;
        statEffectGraph [Stat.Drunkeness] [StatCondition.LessThan20] [Stat.Patriotism] = (p) => p - 4 * factor;
        statEffectGraph [Stat.Patriotism] = new Dictionary<StatCondition, Dictionary<Stat, Func<float, float>>> ();
        statEffectGraph [Stat.Patriotism] [StatCondition.MoreThan80] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Patriotism] [StatCondition.MoreThan80] [Stat.Happiness] = (p) => p + 4 * factor;
        statEffectGraph [Stat.Patriotism] [StatCondition.LessThan20] = new Dictionary<Stat, Func<float, float>> ();
        statEffectGraph [Stat.Patriotism] [StatCondition.LessThan20] [Stat.Happiness] = (p) => p - 3 * factor;
    }
}