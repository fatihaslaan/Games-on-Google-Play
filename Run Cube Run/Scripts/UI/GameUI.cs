using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}