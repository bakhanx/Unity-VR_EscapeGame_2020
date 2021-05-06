using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeinOut : MonoBehaviour
{
    private CanvasGroup cg;
    public float fadeTime = 1f;
    float accumTime = 0f;

    private Coroutine fadeCor;
    public Image fadeImg;

    private void Awake()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        
    }

    private void OnEnable()
    {
        StartFadeOut();
    }

    public void StartFadeIn()
    {
        GetComponent<Image>().color = new Color(0, 0, 0); // 투명 > 검정
        if (fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeIn());

    }

    public void StartFadeOut()
    {
        if (fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeOut());

    }

    private IEnumerator FadeIn()
    {
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 1f;
    }


    private IEnumerator FadeOut()
    {
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;
    }
}