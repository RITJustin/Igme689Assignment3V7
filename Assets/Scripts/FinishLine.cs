using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public string winSceneName = "WinScene";
    public string loseSceneName = "LoseScene";
    public float timeLimit = 300f; // 5 minutes in seconds

    private float timer;
    private bool gameEnded = false;

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        if (!gameEnded)
        {
            timer -= Time.deltaTime;

            // Lose if timer runs out
            if (timer <= 0f)
            {
                LoseGame();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameEnded) return;

        if (other.CompareTag("Player"))
        {
            WinGame();
        }
    }

    void WinGame()
    {
        gameEnded = true;
        Debug.Log("You Win!");
        SceneManager.LoadScene(winSceneName);
    }

    void LoseGame()
    {
        gameEnded = true;
        Debug.Log("Time’s Up! You Lose!");
        SceneManager.LoadScene(loseSceneName);
    }
}