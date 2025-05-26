using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetStats : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI LastHitText;
    public TextMeshProUGUI DpsText;
    public TextMeshProUGUI TotalText;

    [Header("Reset Settings")]
    public float resetAfterSeconds = 10f;
    public float dpsWindowSeconds = 1f;

    private float lastHitTime = 0f;
    private float totalDamage = 0f;
    private float lastHitDamage = 0f;

    private readonly List<(float time, float damage)> recentHits = new();

    void Update()
    {
        float t = Time.time;

        if (t - lastHitTime > resetAfterSeconds)
            ResetStats();

        float cutoff = t - dpsWindowSeconds;
        recentHits.RemoveAll(entry => entry.time < cutoff);
        float dpsSum = 0f;
        foreach (var (time, dmg) in recentHits)
            dpsSum += dmg;
        float dps = dpsSum / dpsWindowSeconds;

        LastHitText.text = $"Last Hit: {lastHitDamage}";
        DpsText.text = $"DPS: {dps:F1}";
        TotalText.text = $"Total: {totalDamage}";
    }

    public void ApplyHit(float damage)
    {
        float t = Time.time;
        lastHitTime = t;
        lastHitDamage = damage;
        totalDamage += damage;
        recentHits.Add((t, damage));
    }

    private void ResetStats()
    {
        lastHitTime = Time.time;
        lastHitDamage = 0f;
        totalDamage = 0f;
        recentHits.Clear();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 2f);
    }
}
