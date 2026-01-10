using UnityEngine;
using TMPro;

public class MachigaiSpot1 : MonoBehaviour
{
    public bool isFound = false;




    public void OnFound()
    {
        if (isFound) return;//見つけてたら何もしない
        isFound = true;
        MachigaiManager.AddCount();

        Debug.Log("間違いをクリックしました！");
        // 間違いの演出など
    }
}
