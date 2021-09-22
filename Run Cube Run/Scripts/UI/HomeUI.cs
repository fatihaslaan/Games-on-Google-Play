using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    public List<GameObject> levels,buttons;
    public GameObject levelsPanel;

    GameObject temp;

    public void Awake()
    {
        Time.timeScale = 1f;
        try
        {
            GlobalAttributes.Load(); //We will try to load our levels
        }
        catch { }
        finally
        {
            if(GlobalAttributes.currentLevel<1)
                GlobalAttributes.currentLevel=1;
                
            temp = Instantiate(levels[GlobalAttributes.currentLevel-1], Vector3.zero, Quaternion.identity); //Load level to Home Screen
            Destroy(temp.transform.Find("Individuals").gameObject); //No need to see Player and Enemies
            temp.SetActive(true);
            for(int i=0;i<levels.Count;i++)
            {
                if(i+1<=GlobalAttributes.maxLevel)
                {
                    buttons[i].SetActive(true); //Active buttons if we unlocked that level
                }
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Levels()
    {
        levelsPanel.SetActive(true);
    }

    public void CloseLevelPanel()
    {
        levelsPanel.SetActive(false);
    }

    public void SelectLevel(int i)
    {
        GlobalAttributes.currentLevel=i;
        GlobalAttributes.Save();
        SceneManager.LoadScene(0);
    }
}