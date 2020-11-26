using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExploreScreen : MonoBehaviour {

    //Wood, Stone, Ironore, Sulfurore, Meat, Leather,Animalfat;
    public Text Timeleft;
    public Text neededtimestones;
    public GameObject ExplorePanel;
    public GameObject expinfopanel;
    public GameObject expWatchadpanel;
    public Text watchadtext;
    public Text expinfo;
    public Toggle read;
    public GameObject tut;

    List<int> gi = new List<int>();
    List<int> gic = new List<int>();
    int[] garmors = new int[5];
    bool blw = true;

    int[] WaMaA = new int[7];
    int[] WaMaAT = { 5, 4, 2, 3, 1, 2, 2 };
    float[] possb = {0,50,50,50,50,50,50,50,20,20,20,20,20,20,20,20,20, 60, 55, 50, 45, 15, 15, 30, 10, 8, 12, 12, 10, 9, 9, 8, 10, 10, 8, 7, 30, 7, 10, 6, 5, 4, 3, 2, 2, 3, 1, 1, 60, 60, 60, 60, 60, 60 };


    List<List<int>> gunsandammo = new List<List<int>>()
    {
        new List<int>(){8,10}, //arrow
        new List<int>(){18,25}, //pistol
        new List<int>(){19,31}, //shotgun
        new List<int>(){20,37}, //m4
        new List<int>(){36,39}, //snip
    };
    List<int> lighters = new List<int>() {11, 21};
    List<int> aimattach = new List<int>() {22, 38 };
    List<int> looters = new List<int>() {23, 46, 47 };
    int sliencer = 24;
    int selectedgunammo;


    string ettime = "day";
    string errange = "close";
    string hnchoice = "escape";
    string sschoice = "escape";
    string sachoice = "escape";

    float time=1;
    float dodge = 0.01f;    
    bool airdrop=false;
    bool raid = false;
    bool ad = false;

    void Start()
    {
        if (Main.tread[2])
        {
            tut.SetActive(true);
        }
    }

    public void tutclose()
    {
        if (read.isOn)
        {
            Main.tread[2] = false;
        }
        tut.SetActive(false);
        Save.SaveDatas();
    }

    public void usetimestones()
    {
        if ((int)Main.ext <= Main.TimeStone&&Main.ext>1)
        {
            Main.TimeStone -= (int)Main.ext;
            Main.ext = 0.01f;
            Save.SaveDatas();
        }
    }

    void Update()
    {
        if (Main.adshow)
        {
            Main.adshow = false;
            if (blw)
            {
                int t = 0;
                for (int i = 0; i < Main.Levels[12] * 7 + 18; i++)
                {
                    if (Main.HomeInventory[i] == 0 && t <= gi.Count)
                    {
                        Main.HomeInventory[i] = gi[t];
                    }
                    if (Main.HomeInventory[i] == gi[t] && t <= gi.Count)
                    {
                        Main.HomeInventoryC[i] += gic[t];
                        t++;
                    }

                }
            }
            else
            {
                int t = 0;
                for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
                {
                    if (Main.BackpackInventory[i] == 0 && t <= gi.Count)
                    {
                        Main.BackpackInventory[i] = gi[t];
                    }
                    if (Main.BackpackInventory[i] == gi[t] && t <= gi.Count)
                    {
                        Main.BackpackInventoryC[i] += gic[t];
                        t++;
                    }
                }
                for (int i = 1; i <= 4; i++)
                {
                    Main.Armors[i] = garmors[i];
                }
                Main.Armor = garmors[0];
            }
            gi = new List<int>();
            gic = new List<int>();
            garmors = new int[5];
            Save.SaveDatas();
            Main.Progress += "You took your items back\n";            
        }
        if (Main.crafttime != 0)
        {
            Main.crafttime -= Time.deltaTime;
            if (Main.crafttime < 0)
            {
                SceneManager.LoadScene(1);
            }
        }
        if (Main.ext !=0)
        {
            Timeleft.text = (int)Main.ext + " seconds left";
            Main.ext -= Time.deltaTime;
            neededtimestones.text = "Spend " + (int)Main.ext + " TimeStones to Skip";
            ExplorePanel.SetActive(true);
        }
        if (Main.ext < 0)
        {
            Main.ext = 0;
            Timeleft.text = (int)Main.ext + " seconds left";
            ExplorePanel.SetActive(false);
            events();
            switch (Main.exindex) {
                case 1:
                    MaWpressed();
                    break;                
                case 2:
                    Wpressed();
                    break;
                case 3:
                    Mpressed();
                    break;
                case 4:
                    HApressed();
                    break;
                case 5:
                    HPpressed();
                    break;
                case 6:
                    Epressed();
                    break;
            }
            Save.SaveDatas();
        }
    }

    public void ET(string time) //explore time
    {
        ettime = time;
    }

    public void ER(string range) //explore range
    {
        errange = range;
    }

    public void HN(string choice) //hearing noise
    {
        hnchoice = choice;
    }

    public void SS(string choice)//seeing someone
    {
        sschoice = choice;
    }

    public void SA(string choice)//seeing airdrop
    {
        sachoice = choice;
    } //choices

    public void expsure()
    {
        if (Main.exindex == 5)
        {
            Main.risk += 0.2f;
            Main.attackchance += 0.5f;
        }
        if (ettime == "night")
        {
            for (int z = 0; z < Main.Levels[7] * 2 + 8; z++)
            {
                if (Main.BackpackInventory[z] == lighters[1])
                {
                    time *= 0.5f;
                    Main.risk *= 2.5f;
                    Main.aim += 0.05f;
                    Main.loot += 0.5f;
                    goto endlp;
                }
            }
            for (int z = 0; z < Main.Levels[7] * 2 + 8; z++)
            {
                if (Main.BackpackInventory[z] == lighters[0])
                {
                    time *= 0.5f;
                    Main.risk *= 3;
                    Main.loot += 0.5f;
                    goto endlp;
                }
            }
            endlp:;
        }
        Main.ext = 32;
        Main.Progress = "";
        switch (ettime)
        {
            case "night":
                time *= 2;
                Main.risk *= 0.5f;
                break;
            case "day":
                break;
        }
        switch (errange)
        {
            case "far":
                time *= 2;
                Main.risk *= 2;
                Main.loot *= 3;
                break;
            case "normal":
                break;
            case "close":
                time *= 0.5f;
                Main.risk *= 0.5f;
                Main.loot *= 0.5f;
                break;
        }
        Main.ext *= time;
        Main.ext -= Main.Levels[5] * 3;
        if (Main.Hunger > 0)
        {
            Main.Hunger -= (int)(Main.ext / 30 * (10 - Main.Levels[2]));
            if (Main.Hunger < 0)
                Main.Hunger = 0;
        }
        else
        {
            Main.Health -= (int)(Main.ext / 30 * (10 - Main.Levels[2]));
            if (Main.Health <= 0)
            {
                dead();
            }
        }
        if (Main.Health < 100)
        {
            Main.Health += (int)(Main.ext / 50 * (Main.Levels[1]));
            if (Main.Health > 100)
                Main.Health = 100;
        }
        expinfopanel.SetActive(false);
        if(Main.ext<1)
            Main.ext = 0.01f;
        Save.SaveDatas();
    } 

    public void explorepressed(int index)
    {
        Main.exindex = index;
        check();
        expinfo.text += "ARE YOU SURE";
        expinfopanel.SetActive(true);
    }

    public void cancel()
    {
        expinfopanel.SetActive(false);
    }

    void events()
    {
        if (1 == UnityEngine.Random.Range(1, 11))
        {
            int earnedrandts = UnityEngine.Random.Range(1, 21);
            Main.TimeStone += earnedrandts;
            Main.Progress += "You Earned " + earnedrandts + " TimeStones\n";
        }
        if (1 == UnityEngine.Random.Range(1, 11))
        {
            switch (hnchoice)
            {
                case "search":
                    Main.risk += 0.1f;
                    break;
                case "stay":
                    Main.risk += 0.02f;
                    break;
                case "escape":
                    Main.risk *= 0.5f;
                    Main.loot *= 0.5f;
                    break;
            }
        }
        else if (1 == UnityEngine.Random.Range(1, 21))
        {
            switch (sschoice)
            {
                case "attack":
                    Main.attackchance += 0.25f; 
                    break;
                case "stay":
                    Main.risk += 0.1f;
                    break;
                case "escape":
                    Main.risk *= 0.75f;
                    Main.loot *= 0.5f;
                    break;
            }
        }
        else if (1 == UnityEngine.Random.Range(1, 101))
        {
            switch (sachoice)
            {
                case "search":
                    Main.risk += 0.7f;
                    airdrop = true;
                    break;
                case "stay":
                    Main.risk += 0.35f;
                    Main.loot *= 2;
                    break;
                case "escape":
                    Main.risk *= 0.85f;
                    Main.loot *= 0.5f;
                    break;
            }
        }
        Main.risk -= Main.Levels[3]*0.1f;
        dodge += CharacterScreen.dodge+ Main.Levels[3] * 0.01f;
        Main.loot += CharacterScreen.loot;
        Main.aim += Main.Levels[4] * 0.04f;
        ad = false;
        raid = false;
        if(6-Main.Levels[13]>=UnityEngine.Random.Range(1,145))
        {
            baselooted(Main.Levels[8]);
            if (1 == UnityEngine.Random.Range(0, 2))
            {
                watchadtext.text = "Your base looted when you explore\nYou can watch ad and take your items back";
                blw = true;
                expWatchadpanel.SetActive(true);
            }
        }
        calculate();
    }

    public void watchad()
    {
        expWatchadpanel.SetActive(false);
    }

    public void watchadcanceled()
    {
        Save.SaveDatas();
        expWatchadpanel.SetActive(false);
    }

    void check()
    {
        bool lf = false;
        expinfo.text = "";
        if(Main.Armor>0)
            expinfo.text += "You are wearing armor\n";
        if(Main.Health<100)
            expinfo.text += "Your health is under 100\n";
        if(Main.Hunger<40)
            expinfo.text += "Your hunger is under 40\n";

        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == 23)
            {
                expinfo.text += "You have " + Main.BackpackInventoryC[i] + " AntiRads and you will use it\n";
            }
        }

        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == 9)
            {
                expinfo.text += "You equipped " + CraftScreen.itemnames[Main.BackpackInventory[i]] + "\n";
                break;
            }
        }

        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == 12)
            {
                expinfo.text += "You equipped " + CraftScreen.itemnames[Main.BackpackInventory[i]] + "\n";
                break;
            }
        }

        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == 44)
            {
                expinfo.text += "You have " + Main.BackpackInventoryC[i] + " Grenade and you will use it " + "(Only works in explore choice, you will raid houses)\n";
            }
            if (Main.BackpackInventory[i] == 46)
            {
                expinfo.text += "You have " + Main.BackpackInventoryC[i] + " C4 and you will use it " + "(Only works in explore choice, you will raid houses)\n";
            }
        }
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == 47)
            {
                expinfo.text += "You have " + Main.BackpackInventoryC[i] + " Supply and you will only use one of it "+"(Only works in explore choice, it calls an airdrop)\n";                
            }
        }
        for (int x = 4; x >= 0; x--)
        {
            for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
            {
                if (Main.BackpackInventory[i] == gunsandammo[x][1])
                {
                    for (int j = 0; j < Main.Levels[7] * 2 + 8; j++)
                    {
                        if (Main.BackpackInventory[j] == gunsandammo[x][0])
                        {
                            expinfo.text += "You equipped " + CraftScreen.itemnames[Main.BackpackInventory[i]] + " and you have "+Main.BackpackInventoryC[j]+" Ammo of it\n";
                            goto gn;
                        }
                    }
                }
            }
        }
        gn:;
        if (Main.BackpackInventoryC[selectedgunammo] > 0 && Main.BackpackInventory[selectedgunammo] != 8)
        {
            for (int z = 0; z < Main.Levels[7] * 2 + 8; z++)
            {
                if (Main.BackpackInventory[z] == aimattach[1])
                {
                    expinfo.text += "You attached " + CraftScreen.itemnames[aimattach[1]] + " to your gun\n";
                    goto endl;
                }                
            }
            for (int z = 0; z < Main.Levels[7] * 2 + 8; z++)
            {
                if (Main.BackpackInventory[z] == aimattach[0])
                {
                    expinfo.text += "You attached " + CraftScreen.itemnames[aimattach[0]] + " to your gun\n";
                    goto endl;
                }
            }
            endl:;
        }
        if (Main.BackpackInventoryC[selectedgunammo] > 0 && Main.BackpackInventory[selectedgunammo] != 8)
        {
            for (int z = 0; z < Main.Levels[7] * 2 + 8; z++)
            {
                if (Main.BackpackInventory[z] == lighters[1])
                {
                    expinfo.text += "You attached " + CraftScreen.itemnames[lighters[1]] + " to your gun\n";
                    lf = true;
                    break;
                }
            }
        }
        if(!lf)
        {
            for (int z = 0; z < Main.Levels[7] * 2 + 8; z++)
            {
                if (Main.BackpackInventory[z] == lighters[0])
                {
                    expinfo.text += "You equipped " + CraftScreen.itemnames[lighters[0]] + "\n";
                    break;
                }
            }
        }
        if (Main.BackpackInventoryC[selectedgunammo] > 0 && Main.BackpackInventory[selectedgunammo] != 8)
        {
            for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
            {
                if (Main.BackpackInventory[i] == sliencer)
                {
                    expinfo.text += "You attached " + CraftScreen.itemnames[sliencer] + " to your gun\n";
                    break;
                }
            }
        }
    }

    void effects()
    {
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == 44)
            {
                Main.loot += 2 * Main.BackpackInventoryC[i];
                raid = true;
                Main.BackpackInventory[i] = 0;
                Main.BackpackInventoryC[i] = 0;
            }
            if (Main.BackpackInventory[i] == 46)
            {
                Main.loot += 10 * Main.BackpackInventoryC[i];
                raid = true;
                Main.BackpackInventory[i] = 0;
                Main.BackpackInventoryC[i] = 0;
            }
        }
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if(Main.BackpackInventory[i]==23)
            {
                Main.loot += (float)(Main.BackpackInventoryC[i]) * 0.05f;
                Main.BackpackInventory[i] = 0;
                Main.BackpackInventoryC[i] = 0;                
            }
        }
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == 47)
            {                
                Main.BackpackInventoryC[i]--;
                if(Main.BackpackInventoryC[i]==0)
                    Main.BackpackInventory[i] = 0;
                ad = true;
            }
        }
        for (int x = 4; x >= 0; x--)
        {
            for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
            {
                if (Main.BackpackInventory[i] == gunsandammo[x][1])
                {
                    for (int j = 0; j < Main.Levels[7] * 2 + 8; j++)
                    {
                        if (Main.BackpackInventory[j] == gunsandammo[x][0])
                        {
                            Main.aim += 0.1f+x*0.1f;
                            selectedgunammo = j;
                            goto exitloop;
                        }
                    }
                }
            }
        }
        exitloop:;
        if(Main.BackpackInventoryC[selectedgunammo] > 0 && Main.BackpackInventory[selectedgunammo] != 8)
        {
            for (int z = 0; z < Main.Levels[7] * 2 + 8; z++)
            {
                if (Main.BackpackInventory[z] == aimattach[1])
                {
                    Main.aim += 0.1f;
                    z= Main.Levels[7] * 2 + 8;
                    goto endloop;
                }                
            }
            for (int z = 0; z < Main.Levels[7] * 2 + 8; z++)
            {
                if (Main.BackpackInventory[z] == aimattach[0])
                {
                    Main.aim += 0.05f;
                    z = Main.Levels[7] * 2 + 8;
                    goto endloop;
                }
            }
        }
        endloop:;
        if (Main.BackpackInventoryC[selectedgunammo] > 0 && Main.BackpackInventory[selectedgunammo] != 8)
        {
            for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
            {
                if (Main.BackpackInventory[i] == sliencer)
                {
                    dodge += 0.02f;
                    Main.risk -= 0.02f;
                    return;
                }
            }
        }
    }

    void calculate()
    {
        effects();
        if (Main.attackchance * 10000f >= UnityEngine.Random.Range(1, 10000))
        {
            fight(true);
        }
        if (Main.risk *10000f>=UnityEngine.Random.Range(1,10000))
        {
            fight(false);
        }
        if(airdrop)
        {
            airdrop = false;
            if (1 == UnityEngine.Random.Range(1, 3))
                airdroplooted();
        }
    }

    void airdroplooted()
    {
        Main.Progress += "You Looted An Airdrop\n"; //52 48 36
        for (int j = 52; j >= 36; j--)
        {            
            if (j==52||j==48||j==36)
            {
                Additem(j, UnityEngine.Random.Range(50, 101));
            }
            else if (j == 44 ||j == 45 )
            {
                Additem(j, UnityEngine.Random.Range(1,6));
            }
            else if(j!=51&&j!=50&&j!=49&&j!=47)
            {
                Additem(j, 1);
            }            
        }
    }

    void fight(bool attacker)
    {
        Main.Xp += 150;
        Main.Progress += "You Fought And Earned 25 xp\n";
        if(attacker)
        {
            Main.aim += 0.05f;
        }
        else
        {
            Main.aim -= 0.1f;
        }
        if(Main.aim <=0)
        {
            dead();
            if (1 == UnityEngine.Random.Range(0, 2))
            {
                watchadtext.text = "You died when you explore\nYou can watch ad and take your items back";
                blw = false;
                expWatchadpanel.SetActive(true);
            }
        }
        else
        {
            int enemyarmor = UnityEngine.Random.Range(10, 100);
            float enemyaim = UnityEngine.Random.Range(0.25f, 0.85f); 
            float enemydodge= UnityEngine.Random.Range(0.01f, 0.2f);
            int enemyammo = UnityEngine.Random.Range(10, 50);
            int enemyhealth = 100;
            while ((Main.BackpackInventoryC[selectedgunammo]>0||enemyammo>0)&&Main.Health>0&&enemyhealth>0)
            {
                if ((Main.aim * 10000f)-(enemydodge*10000) >= UnityEngine.Random.Range(1, 10000)&& Main.BackpackInventoryC[selectedgunammo] > 0)
                {
                    enemyhealth -= (int)((Main.aim * 100) / 1 + (float)enemyarmor / 100);
                }
                if ((enemyaim * 10000f)-(dodge*10000) >= UnityEngine.Random.Range(1, 10000)&&enemyammo>0)
                {
                    Main.Health-= (int)((enemyaim * 100)/1+(float)Main.Armor/100);
                    
                }
                if(Main.Health<=0)
                {
                    dead();
                    if (1 == UnityEngine.Random.Range(0, 2))
                    {
                        watchadtext.text = "You died when you explore\nYou can watch ad and take your items back";
                        blw = false;
                        expWatchadpanel.SetActive(true);
                    }
                    return;
                }
                else if(enemyhealth<=0)
                {
                    killed();
                    return;
                }
                Main.BackpackInventoryC[selectedgunammo]--;
                if (Main.BackpackInventoryC[selectedgunammo] == 0)
                    Main.BackpackInventory[selectedgunammo] = 0;
                enemyammo--;
            }
        }
    }

    void dead()
    {
        Main.Progress += "You Died\n";
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            gi.Add(Main.BackpackInventory[i]);
            gic.Add(Main.BackpackInventoryC[i]);
            Main.BackpackInventory[i] = 0;
            Main.BackpackInventoryC[i] = 0;                  
        }
        garmors[0] = Main.Armor;
        for(int i=0;i<4;i++)
        {
            garmors[i + 1] = Main.Armors[i];
            Main.Armors[i] = 0;
        }
        Main.Health = 100;
        Main.Hunger = 100;
        Main.Armor = 0;
        Save.SaveDatas();
    }

    void baselooted(int range)
    {
        Main.Progress += "Your Base Looted\n";
        for (int i=0;i<Main.Levels[12]*7+18;i++)
        {
            if (1 == UnityEngine.Random.Range(1, range+2))
            {
                gi.Add(Main.HomeInventory[i]);
                gic.Add(Main.HomeInventoryC[i]);
                Main.HomeInventory[i] = 0;
                Main.HomeInventoryC[i] = 0;
            }
        }
        for(int i=0;i<Main.Supply.Length;i++)
        {
            Main.Supply[i] = 0;
        }
        Save.SaveDatas();
    }

    void reset()
    {
        time = 1;
        Main.risk = 0.1f;
        Main.attackchance = 0f;
        Main.loot = 1;
        dodge = 0.01f;
        Main.aim = 0.05f;
    }

    void killed()
    {
        Main.Progress += "You Killed Someone And Earned 50 xp\n";
        Main.Xp += 250;
        for (int j = 47; j >= 1; j--)
        {
            if (possb[j] + Main.loot >= UnityEngine.Random.Range(1f, 153f))
            {
                if(possb[j]>=50)
                {
                    Additem(j, UnityEngine.Random.Range(10, 31));
                }
                else if (possb[j] >= 45)
                {
                    Additem(j, UnityEngine.Random.Range(5, 21));
                }
                else if (possb[j] >= 30)
                {
                    Additem(j, UnityEngine.Random.Range(1, 11));
                }
                else
                {
                    Additem(j, 1);
                }
            }
        }
    }

    void MaWpressed() //mine and wood
    {
        for (int i = 0; i < 4; i++)
        {           
            WaMaA[i] =Convert.ToInt32(UnityEngine.Random.Range(Main.Levels[6] * 2+1,Main.Levels[6] * 2+5) * WaMaAT[i] * Main.loot)+1;
            Additem(i + 1, WaMaA[i]);
        }
        Main.Xp += 50;
        Main.Progress += "You Earned 5 xp\n";
        reset();
    }

    void Wpressed() //only wood
    {
        int wa = 0;
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == 9)
            {
                wa = 3;
            }
        }
        for (int i = 0; i < 1; i++)
        {
            WaMaA[i] = Convert.ToInt32(UnityEngine.Random.Range(Main.Levels[6] * 2 + 1 + wa, Main.Levels[6] * 2 + 5 + wa) * WaMaAT[i] * 2 * Main.loot)+1;
            Additem(i + 1, WaMaA[i]);
        }
        Main.Xp += 50;
        Main.Progress += "You Earned 5 xp\n";
        reset();
    }

    void Mpressed() //mining
    {
        int ma = 0;
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == 9)
            {
                ma = 3;
            }
        }
        for (int i = 1; i < 4; i++)
        {
            WaMaA[i] = Convert.ToInt32(UnityEngine.Random.Range(Main.Levels[6] * 2 + 1 + ma, Main.Levels[6] * 2 + 5 + ma) * WaMaAT[i] * 2 * Main.loot)+1;
            Additem(i + 1, WaMaA[i]);        
        }
        Main.Xp += 50;
        Main.Progress += "You Earned 5 xp\n";
        reset();
    }

    void HApressed() //hunt animal
    {
        for (int i = 4; i < 7; i++)
        {
            WaMaA[i] = Convert.ToInt32(UnityEngine.Random.Range(Main.Levels[6] * 2 + 1 + Main.aim *10, Main.Levels[6] * 2 + 4 + Main.aim *10) * WaMaAT[i] * 2 * Main.loot) + 1;
            Additem(i + 1, WaMaA[i]);
        }
        Main.Xp += 75;
        Main.Progress += "You Earned 10 xp\n";
        reset();
    }

    void HPpressed() //hunt player
    {
        reset();
    }

    void Epressed() //explore
    {
        Main.Xp += 50;
        Main.Progress += "You Earned 20 xp\n";
        if (raid)
        {
            for (int j = 53; j >= 1; j--)
            {
                if (possb[j] + Main.loot >= UnityEngine.Random.Range(1f, 153f)) //200 ya da 300
                {
                    if (j > 47)
                    {
                        Additem(j, UnityEngine.Random.Range(20, 51));
                    }
                    else if (j == 36 || (j >= 1 && j <= 8) || (j >= 17 && j <= 20))
                    {
                        Additem(j, UnityEngine.Random.Range(40, 81));
                    }
                    else
                    {
                        Additem(j, UnityEngine.Random.Range(1, 6));
                    }
                }
            }
        }
        else if(ad)
        {
            airdroplooted();
        }
        else
        {
            for (int j = 17; j <= 47; j++)
            {
                if (possb[j] + Main.loot >= UnityEngine.Random.Range(1f, 153f))
                {
                    if (possb[j] >= 45)
                    {
                        Additem(j, UnityEngine.Random.Range(5, 21));
                    }
                    else if (possb[j] >= 30)
                    {
                        Additem(j, UnityEngine.Random.Range(1, 11));
                    }
                    else
                    {
                        Additem(j, 1);
                    }
                }
            }
        }
        
        if ( 20 >= UnityEngine.Random.Range(1f, 153f)) //can of food
        {
            Additem(57, UnityEngine.Random.Range(1, 6));
        }
        if (20 >= UnityEngine.Random.Range(1f, 153f)) // med
        {
            Additem(55, UnityEngine.Random.Range(1, 6));
        }
        reset();
    }
    
    void Additem (int index, int count)
    {
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if(index>=54)
            {
                Main.Supply[index - 54]++;
                Main.Progress += "You Found " + count + " " + CraftScreen.itemnames[index] + "\n";
                return;
            }
            if (Main.BackpackInventory[i] == 0)
            {
                Main.BackpackInventory[i] = index;
                Main.BackpackInventoryC[i] = 0;
            }
            if (Main.BackpackInventory[i] == index)
            {
                Main.BackpackInventoryC[i] += count;
                Main.Progress += "You Found " + count + " " + CraftScreen.itemnames[index] + "\n";
                return;
            }
        }
    }
}
