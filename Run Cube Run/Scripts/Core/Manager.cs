using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public List<GameObject> levels;

    public Text gameOverText, playButtonText;

    public PlayerController player;

    public FloatingJoystick joystick;

    public GameObject gameOverPanel;

    static Manager instance;

    void Awake()
    {
        Time.timeScale = 1f;
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        levels[GlobalAttributes.currentLevel-1].SetActive(true); //We are activating current level
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); //We find our player on active level
    }

    public static Manager GetInstance()
    {
        return instance;
    }

    public void ChangeScene(bool nextLevel) //Change scene to play again or to play next level
    {
        if (nextLevel)
        {
            if (GlobalAttributes.currentLevel == levels.Count)
            {

                gameOverText.text = "You Have Completed The Game! Well Done!";
                playButtonText.text = "Play Again";
            }
            else
            {
                gameOverText.text = "Level " + GlobalAttributes.currentLevel + " Passed!";
                GlobalAttributes.currentLevel++;
                if(GlobalAttributes.currentLevel>GlobalAttributes.maxLevel)
                    GlobalAttributes.maxLevel=GlobalAttributes.currentLevel;
                GlobalAttributes.Save();
                playButtonText.text = "Next Level";
            }
        }
        else
        {
            gameOverText.text = "Try Again!";
            playButtonText.text = "Play Again";
        }
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
}