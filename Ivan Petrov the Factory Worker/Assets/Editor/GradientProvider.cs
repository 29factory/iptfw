using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public static class GradientProvider {
    public const int right = 1 << 0;
    public const int top = 1 << 1;
    public const int left = 1 << 2;
    public const int bottom = 1 << 3;
    public const int first = 1 << 4;
    public const int second = 1 << 5;
    public const int third = 1 << 6;
    public const int fourth = 1 << 7;

    private static readonly Dictionary<byte, Dictionary<byte, int>> gradientForms;

    static GradientProvider(){
        gradientForms = new Dictionary<byte, Dictionary<byte, int>> ();
        for (int i = 0; i < 16; i++)
            gradientForms [Convert.ToByte (Convert.ToString(i, 2) + "1111", 2)] = new Dictionary<byte, int> ();
        gradientForms [Convert.ToByte ("10001111", 2)] [Convert.ToByte ("10001001", 2)] = 13;
        gradientForms [Convert.ToByte ("11001111", 2)] [Convert.ToByte ("11001101", 2)] = 1;
        gradientForms [Convert.ToByte ("01001111", 2)] [Convert.ToByte ("01001100", 2)] = 2;
        gradientForms [Convert.ToByte ("00001111", 2)] [Convert.ToByte ("00001000", 2)] = 3;
        gradientForms [Convert.ToByte ("10001111", 2)] [Convert.ToByte ("00001001", 2)] = 4;
        gradientForms [Convert.ToByte ("11001111", 2)] [Convert.ToByte ("00001101", 2)] = 5;
        gradientForms [Convert.ToByte ("01001111", 2)] [Convert.ToByte ("00001100", 2)] = 6;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("11011111", 2)] = 7;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("10011111", 2)] = 8;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("10001111", 2)] = 9;
        gradientForms [Convert.ToByte ("00111111", 2)] [Convert.ToByte ("00010111", 2)] = 10;
        gradientForms [Convert.ToByte ("01101111", 2)] [Convert.ToByte ("01001110", 2)] = 11;
        gradientForms [Convert.ToByte ("10011111", 2)] [Convert.ToByte ("10011011", 2)] = 12;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("11111111", 2)] = 0;
        gradientForms [Convert.ToByte ("01101111", 2)] [Convert.ToByte ("01101110", 2)] = 14;
        gradientForms [Convert.ToByte ("00001111", 2)] [Convert.ToByte ("00001010", 2)] = 15;
        gradientForms [Convert.ToByte ("10011111", 2)] [Convert.ToByte ("00001011", 2)] = 16;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("00001111", 2)] = 17;
        gradientForms [Convert.ToByte ("01101111", 2)] [Convert.ToByte ("00001110", 2)] = 18;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("01111111", 2)] = 19;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("00111111", 2)] = 20;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("00101111", 2)] = 21;
        gradientForms [Convert.ToByte ("11001111", 2)] [Convert.ToByte ("01001101", 2)] = 22;
        gradientForms [Convert.ToByte ("11001111", 2)] [Convert.ToByte ("10001101", 2)] = 23;
        gradientForms [Convert.ToByte ("00011111", 2)] [Convert.ToByte ("00010011", 2)] = 24;
        gradientForms [Convert.ToByte ("00111111", 2)] [Convert.ToByte ("00110111", 2)] = 25;
        gradientForms [Convert.ToByte ("00101111", 2)] [Convert.ToByte ("00100110", 2)] = 26;
        gradientForms [Convert.ToByte ("00001111", 2)] [Convert.ToByte ("00000010", 2)] = 27;
        gradientForms [Convert.ToByte ("00011111", 2)] [Convert.ToByte ("00000011", 2)] = 28;
        gradientForms [Convert.ToByte ("00111111", 2)] [Convert.ToByte ("00000111", 2)] = 29;
        gradientForms [Convert.ToByte ("00101111", 2)] [Convert.ToByte ("00000110", 2)] = 30;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("10111111", 2)] = 31;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("01101111", 2)] = 32;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("00011111", 2)] = 33;
        gradientForms [Convert.ToByte ("01101111", 2)] [Convert.ToByte ("00101110", 2)] = 34;
        gradientForms [Convert.ToByte ("10011111", 2)] [Convert.ToByte ("00011011", 2)] = 35;
        gradientForms [Convert.ToByte ("00001111", 2)] [Convert.ToByte ("00000001", 2)] = 36;
        gradientForms [Convert.ToByte ("00001111", 2)] [Convert.ToByte ("00000101", 2)] = 37;
        gradientForms [Convert.ToByte ("00001111", 2)] [Convert.ToByte ("00000100", 2)] = 38;
        gradientForms [Convert.ToByte ("00001111", 2)] [Convert.ToByte ("00000000", 2)] = 39;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("10101111", 2)] = 40;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("01011111", 2)] = 41;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("11101111", 2)] = 42;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("11001111", 2)] = 43;
        gradientForms [Convert.ToByte ("11111111", 2)] [Convert.ToByte ("01001111", 2)] = 44;
        gradientForms [Convert.ToByte ("10011111", 2)] [Convert.ToByte ("10001011", 2)] = 45;
        gradientForms [Convert.ToByte ("00111111", 2)] [Convert.ToByte ("00100111", 2)] = 46;
    }

    public static Sprite GetSprite (Sprite[] s, int form) {
        return s [GetIndex ((byte) form)];
    }

    public static int GetIndex (int form) {
        for (int i = 0; i < 16; i++)
            if (gradientForms [Convert.ToByte (Convert.ToString(i, 2) + "1111", 2)].ContainsKey ((byte)(form & Convert.ToByte (Convert.ToString(i, 2) + "1111", 2))))
                return gradientForms [Convert.ToByte (Convert.ToString(i, 2) + "1111", 2)] [(byte)(form & Convert.ToByte (Convert.ToString(i, 2) + "1111", 2))];
        return 0;
        //return mul [Array.IndexOf (gradientForms, (byte)form)];
    }
}