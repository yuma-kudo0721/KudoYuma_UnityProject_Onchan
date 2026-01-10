using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;  // 正解マーク（〇）
    [SerializeField] private GameObject crossPrefab;   // 間違いマーク（×）


    public int seikaiFound;
    public int machigaiCount;
    [SerializeField]  AudioClip noSeikaiSE;
    [SerializeField] AudioSource ad;
    


    void Update()
    {
        //#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }
        //#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleInput(Input.GetTouch(0).position);
        }
        //#endif
    }

    void HandleInput(Vector2 screenPosition)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));

        Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);
        RaycastHit2D hit = Physics2D.Raycast(worldPos2D, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<MachigaiSpot>(out var machigai))
            {
                if (!machigai.isFound)
                {
                    machigai.OnFound();
                    seikaiFound++;

                    // 左右両方に〇を出す
                    Instantiate(circlePrefab, machigai.transform.position, Quaternion.identity);
                    if (machigai.pairSpot != null)
                    {
                        Instantiate(circlePrefab, machigai.pairSpot.transform.position, Quaternion.identity);
                    }
                }
            }
        }
        else
        {
            machigaiCount++;
            ad.PlayOneShot(noSeikaiSE);
            Instantiate(crossPrefab, worldPos, Quaternion.identity);
        }
    }
}
