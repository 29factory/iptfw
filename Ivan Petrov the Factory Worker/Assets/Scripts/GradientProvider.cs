using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public static class GradientProvider {
    public const int right = 1 << 0;
    public const int top = 1 << 1;
    public const int left = 1 << 2;
    public const int bottom = 1 << 3;

    public static void GetSprite (ref SpriteRenderer s, Texture2D[] mul, int form) {
        int links = 0;
        links += Convert.ToInt32 ((form & right) == right);
        links += Convert.ToInt32 ((form & top) == top);
        links += Convert.ToInt32 ((form & left) == left);
        links += Convert.ToInt32 ((form & bottom) == bottom);
        Debug.Log (mul[0]);
        if (links == 0) {
            s.sprite = Sprite.Create(mul [5], new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
        } else if (links == 4) {
            s.sprite = Sprite.Create(mul [0], new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
        } else if (links == 1) {
            s.sprite = Sprite.Create(mul [4], new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
            if ((form & top) == top)
                s.transform.Rotate (new Vector3 (0, 0, 90));
            else if ((form & left) == left)
                s.transform.Rotate (new Vector3 (0, 0, 180));
            else if ((form & bottom) == bottom)
                s.transform.Rotate (new Vector3 (0, 0, 270));
        } else if (links == 2) {
            if (((form & (right | left)) == (right | left)) || ((form & (top | bottom)) == (top | bottom))) {
                s.sprite = Sprite.Create(mul [3], new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
                if ((form & (top | bottom)) == (top | bottom))
                    s.transform.Rotate (new Vector3 (0, 0, 90));
            } else {
                s.sprite = Sprite.Create(mul [2], new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
                if ((form & (right | top)) == (right | top))
                    s.transform.Rotate (new Vector3 (0, 0, 90));
                else if ((form & (left | top)) == (left | top))
                    s.transform.Rotate (new Vector3 (0, 0, 180));
                else if ((form & (left | bottom)) == (left | bottom))
                    s.transform.Rotate (new Vector3 (0, 0, 270));
            }
        } else {
            s.sprite = Sprite.Create(mul [1], new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
            if ((form & right) == 0) s.transform.Rotate (new Vector3 (0, 0, 270));
            else if ((form & left) == 0) s.transform.Rotate (new Vector3 (0, 0, 90));
            else s.transform.Rotate (new Vector3 (0, 0, 180));
        }
    }
}