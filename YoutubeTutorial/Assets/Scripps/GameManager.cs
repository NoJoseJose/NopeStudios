using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded;
    public int restartDelay = 2;

    public GameObject completeLevelUI;

    public void WinGame()
    {
        PlayerMovement pm = FindObjectOfType<PlayerMovement>();
        pm.StopPlayer();

        completeLevelUI.SetActive(true);
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
