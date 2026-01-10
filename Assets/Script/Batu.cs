using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batu : MonoBehaviour
{

    SpriteRenderer sp;
    public float startDelete;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        StartCoroutine(LoadGameAssets());



    }

    IEnumerator LoadGameAssets()
    {
        if (startDelete > 0)
            yield return new WaitForSeconds(startDelete);
        float duration = 2f; // 消えるまでの時間
        float elapsed = 0f;

        Color c = sp.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            sp.color = c;
            yield return null;
        }

        Destroy(gameObject);
    }
}
