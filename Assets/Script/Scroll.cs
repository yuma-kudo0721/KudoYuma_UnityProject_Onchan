using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 startPosition;

    void Start()
    {
        // GameObject（Transform）の初期位置を記録
        startPosition = transform.position;
    }

    void Update()
    {
        // 左下方向にスクロール（斜め）
        transform.position += new Vector3(-1f, -1f, 0f) * speed * Time.deltaTime;

        // 一定距離動いたらリセット（ループ）
        if (Vector3.Distance(transform.position, startPosition) > 6f)
        {
            transform.position = startPosition;
        }
    }
}
