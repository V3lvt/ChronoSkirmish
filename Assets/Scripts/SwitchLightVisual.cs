using UnityEngine;

[ExecuteAlways]
public class SwitchLightVisual : MonoBehaviour
{
    [Tooltip("Point Light, который будет загораться при открытой двери")]
    public Light pointLight;

    [Tooltip("Рендерер для дополнительной индикации (например, эмиссия)")]
    public Renderer indicatorRenderer;

    [ColorUsage(true, true)]
    public Color onColor = Color.green;

    private void Reset()
    {
        if (indicatorRenderer != null)
        {
            var mats = indicatorRenderer.sharedMaterials;
            foreach (var m in mats)
            {
                if (m != null)
                    m.EnableKeyword("_EMISSION");
            }
        }
    }

    private void Start()
    {
        SetState(false);
    }

    public void SetState(bool on)
    {
        if (pointLight != null)
        {
            pointLight.enabled = on;
            if (on)
            {
                pointLight.color = onColor;
            }
        }

        if (indicatorRenderer != null)
        {
            foreach (var mat in indicatorRenderer.materials)
            {
                if (on)
                    mat.SetColor("_EmissionColor", onColor);
                else
                    mat.SetColor("_EmissionColor", Color.black);
            }
        }
    }
}
