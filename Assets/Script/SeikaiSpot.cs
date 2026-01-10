using UnityEngine;

public class SeikaiSpot : MonoBehaviour
{
    private bool isFound = false;


    public void OnFound()
    {

        if (isFound) return;
        isFound = true;

        Debug.Log("正解を見つけました！");
        // 必要ならアニメーションやスコア処理など
    }
}
