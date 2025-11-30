using UnityEngine; // Fixed typo here
using UnityEngine.UI; // Fixed typo here
using UnityEngine.SceneManagement; // Required to load scenes

public class PlayButton : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadMainScene);
    }

    // Method to load the main game scene
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main"); // Ensure this matches your scene name in Build Settings
        Time.timeScale = 1f;
    }
}
