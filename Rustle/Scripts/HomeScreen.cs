using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HomeScreen : MonoBehaviour {


    public Text SkillsA,Progress, Xpprogress;
    public Text[] txtLevels;
    public Text TimeStones;
    public Image pb;
    public Toggle read;
    public GameObject tut;

    void Update()
    {
        if (Main.ext != 0)
        {
            Main.ext -= Time.deltaTime;
            if (Main.ext < 0)
            {
                SceneManager.LoadScene(2);
            }
        }
        if (Main.crafttime != 0)
        {
            Main.crafttime -= Time.deltaTime;
            if (Main.crafttime < 0)
            {
                SceneManager.LoadScene(1);
            }
        }
        if (Main.adshow)
        {
            Main.TimeStone += 200;
            Main.Progress += "You Earned 200 Timestone";
            Progress.text = Main.Progress.ToString();
            TimeStones.text = "" + Main.TimeStone;
            Save.SaveDatas();
            Main.adshow = false;
        }        
    }
	void Start ()
    {
        try{Main.Set();}finally{
        if (Main.tread[0])
        {
            tut.SetActive(true);
        }
        manualUpdate();        
        pb.fillAmount=((float)Main.Xp/ (float)((Main.Levels[0] * 100) + 100));}
    }

    public void tutclose()
    {
        if(read.isOn)
        {
            Main.tread[0] = false;
        }
        tut.SetActive(false);
        Save.SaveDatas();
    }

    public void manualUpdate()
    {        
        while (Main.Xp >= (Main.Levels[0] * 100) + 100)
        {
            Main.Xp -= (Main.Levels[0] * 100) + 100;
            Main.Levels[0]++;
            Main.SkillsA++;
            Main.TimeStone += 75;
            Save.SaveDatas();
        }
        TimeStones.text = "" + Main.TimeStone;
        Xpprogress.text =Main.Xp.ToString() + "/" + ((Main.Levels[0] * 100) + 100).ToString();        
        SkillsA.text = Main.SkillsA.ToString();
        for (int i = 0; i < txtLevels.Length; i++)
        {
            txtLevels[i].text = "Level "+Main.Levels[i + 1].ToString();
        }
        Progress.text = Main.Progress.ToString();
    }

    public void LevelU(int index)
    {
        if (Main.SkillsA > 0&&Main.Levels[index]<5)
        {
            Main.Levels[index]++;
            Main.SkillsA--;
            manualUpdate();
            Save.SaveDatas();
        }
    }
}
