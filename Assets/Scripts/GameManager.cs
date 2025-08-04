using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    public PlayerController player;
    public HUDManager hud;
    
    [HideInInspector] public float elapsedTime = 0f;
    private bool gameEnded = false;

    public bool isAgePaused = false;

    private void UpdateHUD_Age(int newAge)
    {
        if (hud != null)
            hud.UpdateAge(newAge);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);


        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
            if (player == null) Debug.LogError("GameManager: PlayerController not found in scene.");
            player.OnAgeChanged += UpdateHUD_Age;
        }
        if (hud == null)
        {
            hud = FindObjectOfType<HUDManager>();
            if (hud == null) Debug.LogError("GameManager: HUDManager not found in scene.");
        }
    }

    void Update()
    {
        if (!gameEnded) elapsedTime += Time.deltaTime;
        if (player != null && hud != null)
        {
            hud.UpdateHealth(player.currentHealth, player.maxHealth);
            hud.UpdateAge(player.age);
            hud.UpdateDamage(player.dpsMultiplier);
        }
    }
    public void EndGame()
    {
        gameEnded = true;
    }

}