using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    
    public AudioSource audioSource;

    public void Start()
    {
        
    }

    public void OnStartGameButton()
    {
        string playerName = nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            // 前回の名無し番号を取得
            int lastNum = PlayerPrefs.GetInt("LastNanashiNum", 0);
            lastNum++; // 番号を進める
            PlayerPrefs.SetInt("LastNanashiNum", lastNum);

            playerName = "名無し" + lastNum;
        }
        CheckTextCount();


        // 名前を保存（どのシーンからでも読める）
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        // ゲームシーンへ移動
        SceneManager.LoadScene("Game");
    }
    
     public void CheckTextCount()
    {
        

        if (nameInputField.text.Length > 6)
        {
            nameInputField.text = nameInputField.text[..6];
        }
    }
}
