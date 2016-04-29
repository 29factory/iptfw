using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public static class GradientProvider {
    public const int right = 1 << 0;
    public const int top = 1 << 1;
    public const int left = 1 << 2;
    public const int bottom = 1 << 3;

    public static void SetSprite (ref SpriteRenderer s, Sprite[] mul, int form) {
        int links = 0;
        links += Convert.ToInt32 ((form & right) == right);
        links += Convert.ToInt32 ((form & top) == top);
        links += Convert.ToInt32 ((form & left) == left);
        links += Convert.ToInt32 ((form & bottom) == bottom);
        if (links == 0) {
            s.sprite = mul [5];
        } else if (links == 4) {
            s.sprite = mul [0];
        } else if (links == 1) {
            s.sprite = mul[4];
            if ((form & top) == top)
                s.transform.Rotate (new Vector3 (0, 0, 90));
            else if ((form & left) == left)
                s.transform.Rotate (new Vector3 (0, 0, 180));
            else if ((form & bottom) == bottom)
                s.transform.Rotate (new Vector3 (0, 0, 270));
        } else if (links == 2) {
            if (((form & (right | left)) == (right | left)) || ((form & (top | bottom)) == (top | bottom))) {
                s.sprite = mul [3];
                if ((form & (top | bottom)) == (top | bottom))
                    s.transform.Rotate (new Vector3 (0, 0, 90));
            } else {
                s.sprite = mul [2];
                if ((form & (right | top)) == (right | top))
                    s.transform.Rotate (new Vector3 (0, 0, 90));
                else if ((form & (left | top)) == (left | top))
                    s.transform.Rotate (new Vector3 (0, 0, 180));
                else if ((form & (left | bottom)) == (left | bottom))
                    s.transform.Rotate (new Vector3 (0, 0, 270));
            }
        } else {
            s.sprite = mul [1];
            if ((form & right) == 0) s.transform.Rotate (new Vector3 (0, 0, 270));
            else if ((form & left) == 0) s.transform.Rotate (new Vector3 (0, 0, 90));
            else  if ((form & bottom) == 0) s.transform.Rotate (new Vector3 (0, 0, 180));
        }
    }
}