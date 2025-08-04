using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameScreenManager : MonoBehaviour
{
    public void MainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Main Menu");
    }
}

