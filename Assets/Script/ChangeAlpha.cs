using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlpha : MonoBehaviour
{
    private SpriteRenderer sr;
    public float fadeDuration = 2f; // フェードにかける時間
    private float timer = 0f;
    private bool fadingIn = true; // trueならフェードイン中、falseならフェードアウト中

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0f; // 最初は透明
        sr.color = c;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / fadeDuration);

        if (fadingIn)
        {
            // フェードイン
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, t);

            if (t >= 1f)
            {
                // フェードイン終了 → フェードアウトに切り替え
                fadingIn = false;
                timer = 0f;
            }
        }
        else
        {
            // フェードアウト
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f - t);

            if (t >= 1f)
            {
                // フェードアウト終了 → フェードインに切り替え
                fadingIn = true;
                timer = 0f;
            }
        }
    }
}
