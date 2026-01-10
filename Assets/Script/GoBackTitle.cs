using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 追加
public class GoBackTitle : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("Title");
    }
}