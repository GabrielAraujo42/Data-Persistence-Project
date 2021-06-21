using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int menuScene = 0;
    [SerializeField] int gameScene = 1;
    [SerializeField] int scoresScene = 2;

    public void LoadMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void LoadGame()
    {
        if (FindObjectOfType<HighScores>().StartGame())
        {
            SceneManager.LoadScene(gameScene);
        }
        else
        {
            Debug.Log("Problem");
        }
    }

    public void LoadScores()
    {
        SceneManager.LoadScene(scoresScene);
    }
}
