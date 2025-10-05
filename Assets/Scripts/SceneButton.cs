using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public void ReloadGame()
    {
        SceneManager.LoadScene("DrivingSim"); // Replace with your main scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit! (won’t show in editor)");
    }
}