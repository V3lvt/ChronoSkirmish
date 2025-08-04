using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class EndgameZone : MonoBehaviour
{
    public GameObject endgameScreen;
    public CanvasGroup endgameCanvasGroup;
    public float fadeDuration = 1f;

    public TMP_Text timerText;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.EndGame();

            endgameScreen.SetActive(true);
            StartCoroutine(FadeIn());
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            float time = GameManager.Instance.elapsedTime;
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";

        }
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        endgameCanvasGroup.alpha = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            endgameCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        endgameCanvasGroup.alpha = 1f;
    }
}