using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartTime : MonoBehaviour
{
    [SerializeField] float startCountDown = 3f;
    [SerializeField] GameObject time_Object;
    [SerializeField] GameObject poi;
    [SerializeField] GameObject touchMana;
    [SerializeField] GameObject gameCount;
    private bool hasStarted = false;
    private bool hasSwitched = false;  // 2番目のBGMに切り替えたかどうかのフラグ
    private TextMeshProUGUI timelimit_Text;

    [SerializeField] AudioSource countDownSE;
    [SerializeField] AudioSource bgmAudioSource;   // 1番目のBGM
    [SerializeField] AudioSource bgmAudioSource2;  // 2番目のBGM
    [SerializeField] GameObject owariSE;

    private int lastDisplayedTime = -1;

    // ゲーム開始回数カウント
    private static int startCount = 0;

    void Start()
    {
        poi.SetActive(false);
        touchMana.SetActive(false);
        gameCount.SetActive(false);
        owariSE.SetActive(false);
        bgmAudioSource.Stop();
        bgmAudioSource2.Stop();

        timelimit_Text = time_Object.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!hasStarted)
        {
            startCountDown -= Time.deltaTime;
            startCountDown = Mathf.Clamp(startCountDown, 0, 3);

            int currentTime = Mathf.CeilToInt(startCountDown);

            if (currentTime != lastDisplayedTime && currentTime > 0)
            {
                lastDisplayedTime = currentTime;
                countDownSE.Play();
            }

            if (startCountDown > 0)
            {
                timelimit_Text.text = currentTime.ToString();
            }
            else
            {
                timelimit_Text.text = "START";
                poi.SetActive(true);
                touchMana.SetActive(true);
                Invoke("HideText", 1f);
                gameCount.SetActive(true);

                bgmAudioSource.Play();

                hasStarted = true;
                startCount++; // 回数更新
            }
        }
        else
        {
            // 1回目のBGMが終了して、まだ切り替えていなければ2番目のBGMを再生
            if (!bgmAudioSource.isPlaying && !hasSwitched)
            {
                bgmAudioSource2.Play();
                hasSwitched = true;
            }
        }
    }

    void HideText()
    {
        timelimit_Text.enabled = false;
    }
}
