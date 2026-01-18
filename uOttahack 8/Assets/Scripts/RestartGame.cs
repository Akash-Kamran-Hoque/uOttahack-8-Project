using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void LoadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
        Time.timeScale = 1; //Unpause game
    }
}
