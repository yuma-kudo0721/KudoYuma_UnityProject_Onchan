using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TimeManager : MonoBehaviour
{
    //カウントダウン
    public float countDown = 63f;
    public GameObject time_Object; // Textオブジェクト
    private TextMeshProUGUI timelimit_Text;




    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        // オブジェクトからTextコンポーネントを取得
        timelimit_Text = time_Object.GetComponent<TextMeshProUGUI>();
        //countDown = Mathf.Clamp(countDown, 0, 93);
        // テキストの表示を入れ替える
        timelimit_Text.text = countDown.ToString("f0");

    }


}
