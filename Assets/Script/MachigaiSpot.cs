using UnityEngine;
using TMPro;

public class MachigaiSpot : MonoBehaviour
{
    public bool isFound = false;          // このスポットが発見済みか
    public MachigaiSpot pairSpot;         // ペアになるスポット
    private bool counted = false;         // この間違い全体がカウント済みか

    public void OnFound()
    {
        if (!isFound)
        {
            
            isFound = true;

            // まだカウントしていなければカウントする
            if (!counted)
            {
                MachigaiManager.AddCount();
                counted = true;

                // ペア側もカウント済みにしておく
                if (pairSpot != null)
                {
                    pairSpot.isFound = true;
                    pairSpot.counted = true;
                }
            }
        }
    }
}
