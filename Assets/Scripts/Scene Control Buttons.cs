using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtons : MonoBehaviour
{
    // This method restarts the current scene

    public string nextLevel;
    public void RestartScene()
    {
        // Get the active scene and reload it
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevel);
    }
    public void ReturnToHome(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
    public void GoToLevelSelect(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level Select");
    }
}
