using UnityEngine;
using TMPro;

public class MachigaiManager : MonoBehaviour
{
     public static MachigaiManager Instance { get; private set; }
    public static int totalCount = 0;
    public static TextMeshProUGUI machigaiText;

    public TextMeshProUGUI machigaiText_Instance;
    [SerializeField] AudioSource ad;
    [SerializeField]  AudioClip seikaiSE;
    

     void Awake()
    {
        Instance = this; // シングルトン設定
    }

    void Start()
    {
        totalCount = 0; // ゲーム開始時にリセット
        machigaiText = machigaiText_Instance;
        UpdateText();
    }

    public static void AddCount()
    {
         if (Instance != null)
        {
            Instance.ad.PlayOneShot(Instance.seikaiSE);
        }
       

        totalCount++;
        UpdateText();
        Debug.Log("現在の間違いカウント: " + totalCount);
    }

    static void UpdateText()
    {
        if (machigaiText != null)
            machigaiText.text = "みつけたまちがい " + totalCount;
    }
}
