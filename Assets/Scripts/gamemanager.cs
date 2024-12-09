using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // UI References
    public TextMeshProUGUI startScreen;

    void Awake()
    {
        // Ensure only one instance of the GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameManager between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowStartScreen()
    {
        if (startScreen != null)
        {
            startScreen.gameObject.SetActive(true);
        }
    }

    public void HideStartScreen()
    {
        if (startScreen != null)
        {
            startScreen.gameObject.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToStartScene()
    {
        SceneManager.LoadScene("StartScreen"); 
    }
}
