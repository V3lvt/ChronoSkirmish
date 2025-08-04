using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TooltipUIManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TMP_Text tooltipText; // или public Text tooltipText;
    public float fadeDuration = 0.5f;

    private Coroutine fadeCoroutine;

    void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public void ShowTooltip(string message)
    {
        tooltipText.text = message;
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(1f));
    }

    public void HideTooltip()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(0f));
    }

    private IEnumerator FadeCanvasGroup(float target)
    {
        float start = canvasGroup.alpha;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, target, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = target;
    }
}