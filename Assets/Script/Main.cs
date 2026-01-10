using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;




public class Main : MonoBehaviour
{
    [SerializeField] TimeManager timeManager;
    [SerializeField] TouchManager touchManager;
    [SerializeField] MachigaiManager machigaiManager;
    [SerializeField] OffLineRanking offLineRanking;

    [SerializeField] GameObject finishTxt;
    [SerializeField] GameObject explanationTxt1;
    [SerializeField] GameObject explanationTxt2;
    [SerializeField] GameObject explanationTxt3;
    [SerializeField] GameObject explanationTxt4;
    [SerializeField] GameObject owariImage;
    [SerializeField] GameObject BGM;
    [SerializeField] GameObject BGM2;

    [SerializeField] private List<GameObject> illustrations;
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject effect2;
    [SerializeField] private Transform rightStartPos; // 右から入る開始位置
    [SerializeField] private Transform centerPos;     // 中央の表示位置（終点）
    [SerializeField] private Transform leftEndPos;    // 左へスライドして消える位置
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] GameObject owariSE; // 効果音のAudioSource



    private int currentIndex = 0;
    private GameObject currentObj;
    private GameObject nextObj;




    public Transform _camTransform;


    [SerializeField] AudioSource ad;

    [SerializeField] AudioClip cluckerSE;

    [SerializeField] private Button myButton;
    [SerializeField] private float cooldownTime = 2f; // 押せなくなる秒数




    enum Mode
    {
        Title, Game, Finish
    };

    Mode mode = Mode.Title;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = Random.Range(0, illustrations.Count);
        currentObj = Instantiate(illustrations[currentIndex], transform);
        currentObj.transform.position = centerPos.position;
        illustrations.RemoveAt(currentIndex);

        canvas2.gameObject.SetActive(false);



    }


    public void SlideToNext()
    {
        if (illustrations.Count <= 0)
        {
            mode = Mode.Finish;
        }
        else
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Mark");
            //currentIndex++;
            currentIndex = Random.Range(0, illustrations.Count);

            // 次の画像は右の開始位置にセット
            nextObj = Instantiate(illustrations[currentIndex], transform);
            nextObj.transform.position = rightStartPos.position;

            // 現在の画像を左の終了位置にスライド
            currentObj.transform.DOMove(leftEndPos.position, slideDuration);
            illustrations.RemoveAt(currentIndex);

            // 次の画像を中央にスライド
            nextObj.transform.DOMove(centerPos.position, slideDuration).OnComplete(() =>
            {
                Destroy(currentObj);
                currentObj = nextObj;
                nextObj = null;
            });




        }


    }


    public void SetText()
    {
        if (currentObj == null) return;

        if (currentObj.CompareTag("Part1"))
        {
            explanationTxt1.SetActive(true);
            explanationTxt2.SetActive(false);
            explanationTxt3.SetActive(false);
            explanationTxt4.SetActive(false);
        }
        else if (currentObj.CompareTag("Part2"))
        {
            explanationTxt1.SetActive(false);
            explanationTxt2.SetActive(true);
            explanationTxt3.SetActive(false);
            explanationTxt4.SetActive(false);
        }
        else if (currentObj.CompareTag("Part3"))
        {
            explanationTxt1.SetActive(false);
            explanationTxt2.SetActive(false);
            explanationTxt3.SetActive(true);
            explanationTxt4.SetActive(false);
        }
        else if (currentObj.CompareTag("Part4"))
        {
            explanationTxt1.SetActive(false);
            explanationTxt2.SetActive(false);
            explanationTxt3.SetActive(false);
            explanationTxt4.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case Mode.Title:
                Title();
                break;
            case Mode.Game:
                Game();
                break;
            case Mode.Finish:
                Finish();

                break;

        }

        SetText();
    }

    public void SkipToNext()
    {
        StartCoroutine(Cooldown());

        // マークを全て消す
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Mark");
        foreach (GameObject obj in objects)
        {
            Destroy(obj); // 即削除にするのがオススメ
        }

        // カウントをリセット
        touchManager.seikaiFound = 0;
        touchManager.machigaiCount = 0;

        // 次の問題へ進む
        StartCoroutine(WaitAndSlide(0.5f));


    }

    private IEnumerator Cooldown()
    {
        myButton.interactable = false; // ボタン無効化
        yield return new WaitForSeconds(cooldownTime);
        myButton.interactable = true;  // ボタン有効化
    }


    public void Title()
    {
        owariImage.SetActive(false);
        finishTxt.SetActive(false);
        owariSE.SetActive(false);


        mode = Mode.Game;

    }

    private IEnumerator WaitAndSlide(float delay)
    {
        yield return new WaitForSeconds(delay);
        SlideToNext();
    }

    public void Game()
    {
        if (timeManager.countDown <= 0)
        {


            mode = Mode.Finish;


        }
        else if (touchManager.seikaiFound >= 10)
        {

            ad.PlayOneShot(cluckerSE);
            Instantiate(effect, centerPos);
            Instantiate(effect2, centerPos);




            GameObject[] objects = GameObject.FindGameObjectsWithTag("Mark");

            // 各オブジェクトを削除
            foreach (GameObject obj in objects)
            {
                Destroy(obj, 1f);
            }
            touchManager.seikaiFound = 0;
            touchManager.machigaiCount = 0;
            StartCoroutine(WaitAndSlide(1f)); // ← ここで1.5秒くらい待つ
            //SlideToNext();
        }
        else if (currentObj.CompareTag("Part2") && touchManager.seikaiFound >= 1)
        {
            ad.PlayOneShot(cluckerSE);
            Instantiate(effect, centerPos);
            Instantiate(effect2, centerPos);




            GameObject[] objects = GameObject.FindGameObjectsWithTag("Mark");

            // 各オブジェクトを削除
            foreach (GameObject obj in objects)
            {
                Destroy(obj, 1f);
            }
            touchManager.seikaiFound = 0;
            touchManager.machigaiCount = 0;
            StartCoroutine(WaitAndSlide(1f)); // ← ここで1.5秒くらい待つ

        }
        else if (currentObj.CompareTag("Part3") && touchManager.seikaiFound >= 1)
        {
            ad.PlayOneShot(cluckerSE);
            Instantiate(effect, centerPos);
            Instantiate(effect2, centerPos);




            GameObject[] objects = GameObject.FindGameObjectsWithTag("Mark");

            // 各オブジェクトを削除
            foreach (GameObject obj in objects)
            {
                Destroy(obj, 1f);
            }
            touchManager.seikaiFound = 0;
            touchManager.machigaiCount = 0;
            StartCoroutine(WaitAndSlide(1f)); // ← ここで1.5秒くらい待つ

        }
        else if (currentObj.CompareTag("Part4") && touchManager.seikaiFound >= 3)
        {
            ad.PlayOneShot(cluckerSE);
            Instantiate(effect, centerPos);
            Instantiate(effect2, centerPos);




            GameObject[] objects = GameObject.FindGameObjectsWithTag("Mark");

            // 各オブジェクトを削除
            foreach (GameObject obj in objects)
            {
                Destroy(obj, 1f);
            }
            touchManager.seikaiFound = 0;
            touchManager.machigaiCount = 0;
            StartCoroutine(WaitAndSlide(1f));

        }



    }
    public void OnGameEnd()
    {
        OffLineRanking ranking = FindObjectOfType<OffLineRanking>();
        ranking.UpdateRanking();
    }


    bool isFinished = false;
    public void Finish()
    {
        if (isFinished) return;
        isFinished = true;


        OnGameEnd();
        owariImage.SetActive(true);

        owariSE.SetActive(true);

        touchManager.enabled = false;
        BGM.SetActive(false);
        BGM2.SetActive(false);
        Invoke("ResultMove", 3f);


    }
    public Transform canvas2Position; // Canvas2 を映す位置
    public Canvas canvas1;
    public Canvas canvas2;

    public void ResultMove()
    {
        canvas1.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(true);
        _camTransform.DOKill();
        _camTransform.DOMove(canvas2Position.position, 1f).SetUpdate(true);
    }

    public void RePlay()
    {
        // Spaceキー、タッチ、またはマウスクリックがあったかチェック
        /* bool isRestartKeyPressed = Input.GetKeyDown(KeyCode.Space);
         bool isTouchRestart = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
         bool isMouseClick = Input.GetMouseButtonDown(0); // 左クリック

         if (!isRestartKeyPressed && !isTouchRestart && !isMouseClick)
         {
             return;
         }*/

        SceneManager.LoadScene("Title");
        mode = Mode.Title;
    }
}
