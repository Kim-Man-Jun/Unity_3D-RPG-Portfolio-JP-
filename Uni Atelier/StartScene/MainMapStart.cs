using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMapStart : MonoBehaviour
{
    [Header("Scene FadeIn")]
    public Image fadeImage;
    public float fadeSpeed;

    void Awake()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Time.timeScale = 0;

        fadeImage.color = new Color(0, 0, 0, 1);

        float fadeTime = 1;

        while (fadeTime > 0)
        {
            //timescale에 영향받지 않게 unscaledDeltaTime 사용
            fadeTime -= Time.unscaledDeltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, fadeTime);
            yield return null;
        }

        Time.timeScale = 1;
    }
}
