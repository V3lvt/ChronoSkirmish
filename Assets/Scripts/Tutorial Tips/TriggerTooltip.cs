using UnityEngine;

public class TriggerTooltip : MonoBehaviour
{
    [TextArea] public string message; // Текст подсказки

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TooltipUIManager tooltip = FindObjectOfType<TooltipUIManager>();
            if (tooltip != null)
                tooltip.ShowTooltip(message);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TooltipUIManager tooltip = FindObjectOfType<TooltipUIManager>();
            if (tooltip != null)
                tooltip.HideTooltip();
        }
    }
}
