using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [Header("Game Start, Continue Switch Related")]
    public static bool gameStart;
    public static bool gameContinue;

    [Header("Game Start, Continue Player Transform Related")]
    public statSO_player playerStat;

    [Header("Scene FadeOut")]
    public Image fadeImage;
    public float fadeSpeed;

    [Header("Start Scene Animation Effect Related")]
    public Image titleImage;
    public TMP_Text titleMainName;
    public TMP_Text titleSubName;
    public float fadeTime = 1f;
    float fillTime = 0;

    public GameObject firstStartButton;
    public GameObject firstContinueButton;
    public GameObject firstQuitButton;

    [Header("Game Over Scene Animation Effect Related")]
    public GameObject gameOverUI;
    public GameObject retryButton;
    public GameObject quitButton;
    public Transform stopPos;

    private void Start()
    {
        //시작 씬
        if (titleImage != null)
        {
            SceneSoundManager.instance.OpeningSound();

            titleImage.fillAmount = 0;
            titleMainName.color = new Color(0, 0, 0, 0);
            titleSubName.color = new Color(0, 0, 0, 0);
        }

        if (firstStartButton != null)
        {
            firstStartButton.SetActive(false);
            firstContinueButton.SetActive(false);
            firstQuitButton.SetActive(false);

            StartCoroutine(gameStartUI());
        }
        
        //게임오버 씬
        if (retryButton != null)
        {
            SceneSoundManager.instance.GameOverSound();

            retryButton.SetActive(false);
            quitButton.SetActive(false);
        }

        if (gameOverUI != null)
        {
            StartCoroutine(gameOverUIAppear());
        }
    }

    IEnumerator gameStartUI()
    {
        float startMainAlpha = titleMainName.color.a;
        float startSubAlpha = titleSubName.color.a;

        float targetAlpha = 1;
        float nowTime = 0;

        while (nowTime < fadeTime)
        {
            nowTime += Time.deltaTime;
            float t = Mathf.Clamp01(nowTime / fadeTime);

            float Alpha = Mathf.Lerp(startMainAlpha, targetAlpha, t);
            float subAlpha = Mathf.Lerp(startSubAlpha, targetAlpha, t);

            titleMainName.color = new Color(0, 0, 0, Alpha);
            titleSubName.color = new Color(0, 0, 0, subAlpha);

            yield return null;
        }

        yield return new WaitForSeconds(1.8f);

        firstStartButton.SetActive(true);
        firstContinueButton.SetActive(true);
        firstQuitButton.SetActive(true);
    }

    IEnumerator gameOverUIAppear()
    {
        yield return new WaitForSeconds(1.8f);

        retryButton.SetActive(true);
        quitButton.SetActive(true);
    }

    private void Update()
    {
        if (gameOverUI != null)
        {
            gameOverUI.transform.position = Vector3.MoveTowards(gameOverUI.transform.position,
                stopPos.position, 65 * Time.deltaTime);
        }

        if (titleImage != null)
        {
            if (fillTime < 1.5f)
            {
                fillTime += Time.deltaTime;

                float t = Mathf.Clamp01(fillTime / fadeTime);

                float Alpha = Mathf.Lerp(0, 1, t);

                titleImage.fillAmount = Alpha;
            }
        }
    }

    public void startButton()
    {
        gameStart = true;
        EffectSoundManager.instance.SelectButton();
        StartCoroutine(FadeOut());
    }

    public void continueButton()
    {
        gameContinue = true;
        EffectSoundManager.instance.SelectButton();
        StartCoroutine(FadeOut());
    }

    public void exitButton()
    {
        EffectSoundManager.instance.SelectButton();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    IEnumerator FadeOut()
    {
        fadeImage.raycastTarget = true;

        float fadeTime = 0;

        while (fadeTime <= 1)
        {
            fadeTime += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, fadeTime);
            yield return null;
        }
        fadeImage.raycastTarget = false;

        SceneSoundManager.instance.musicStop();
        SceneSoundManager.instance.MainFieldSound();

        SceneManager.LoadScene("MainMap");
    }
}
