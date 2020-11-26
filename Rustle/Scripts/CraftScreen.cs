using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CraftScreen : MonoBehaviour
{
    public static bool learnclicked = false;
    public static float learnpos = 0;

    public GameObject tut;
    public Toggle read;
    public Text timeleft;
    public Text neededtimestones;
    public GameObject CraftPanel;
    public GameObject[] CraftShows;
    public GameObject[] CraftSlots;
    public GameObject[] CraftableItems;
    public GameObject Learn;
    public GameObject[] Salve;
    public Text[] NeededTexts;
    public Text[] NeededHomeTexts;
    public Text[] NameTexts;
    public Text[] Numbers;
    public Text[] Homelevels;
    public Canvas cnvs;
    GameObject[] clones=new GameObject[5];
    GameObject[] lclones = new GameObject[5];
    List<List<int>> craftings = new List<List<int>>()
    {
        new List<int>(){1,10,48},  //use 10 '1' to create '48' 
        new List<int>(){3,1,1,5,51},//use 1 '3' and 5 '1' to create '51' also create coal
        new List<int>(){51,10,52},
        new List<int>(){4,1,1,5,50},//coal
        new List<int>(){7,6,5,5,53},

        new List<int>(){5,1,1,5,56}, //coal
        new List<int>(){1,2,6,10,4,2,9},
        new List<int>(){1,2,6,12,6,4,12},
        new List<int>(){1,2,6,2,8},
        new List<int>(){1,6,20,10,10},

        new List<int>(){49,50,2,2,17},
        new List<int>(){17,51,14,6,18},
        new List<int>(){17,51,16,8,19},
        new List<int>(){17,51,18,10,20},
        new List<int>(){17,51,20,12,36},

        new List<int>(){52,20,25},
        new List<int>(){6,20,13},
        new List<int>(){6,30,15},
        new List<int>(){6,50,16},
        new List<int>(){6,20,14},

        new List<int>(){52,30,31},
        new List<int>(){6,7,51,10,10,10,26},
        new List<int>(){6,7,51,15,15,15,28},
        new List<int>(){6,7,51,25,25,25,29},
        new List<int>(){6,7,51,10,10,10,27},

        new List<int>(){52,40,37},
        new List<int>(){52,6,5,12,32},
        new List<int>(){52,6,10,24,34},
        new List<int>(){52,6,10,24,35},
        new List<int>(){52,6,5,12,33},

        new List<int>(){52,50,39},
        new List<int>(){52,6,7,10,15,10,40},
        new List<int>(){52,6,7,15,20,15,42},
        new List<int>(){52,6,7,15,20,15,43},
        new List<int>(){52,6,7,10,15,10,41},

        new List<int>(){1,6,7,8,4,6,11},
        new List<int>(){52,5,21},
        new List<int>(){52,5,22},
        new List<int>(){52,5,24},
        new List<int>(){52,5,38},

        new List<int>(){6,10,54},
        new List<int>(){6,7,51,8,20,10,55},
        new List<int>(){17,51,52,50,10,10,44},
        new List<int>(){7,53,50,50,50,30,45},
        new List<int>(){45,52,51,17,8,50,10,100,46},
    };
    List<List<int>> upgradehomeparts = new List<List<int>>()
    {
        new List<int>(){48,20}, //use 20 '48'
        new List<int>(){1,100},
        new List<int>(){1,2,50,50}, //use 50 of '1' and '2'
        new List<int>(){2,53,100,10},
        new List<int>(){1,200},
        new List<int>(){1,250},
    };
    ArrayList usalvableitems = new ArrayList() { 0, 1, 2, 3, 4, 5, 10, 11, 12, 13, 14, 21, 22, 23, 24, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44 };
    public static string[] itemnames = new string[58]{ "","Wood","Stone","Iron Ore","Sulfur Ore","Meat","Leather","Animal Fat","Arrow","Axe","Bow","Torch","Pickaxe","Leather Boot","Leather Helmet","Leather Pants","Leather Chest","Gunpowder","9mm","Shotgun Ammo","5.56","Flashlight","Holosight","AntiRad","Sliencer","Pistol","Radboot","Radhelmet","Radpant","Radchest","Research Kit","Shotgun","Mil.boot","Milhelmet","Milpant","Milchest","7.62","M4","Sniper Sight","Sniper Rifle","Specboot","Spechelmet","Specpant","Specchest","Grenade","Explosive","C4","Supply","Woodplank","Charcoal","Sulfur","Iron","Forged Iron","Low Grade Fuel","Bandage","Medkit","Cooked Meat","Can of Food"};

    public void usetimestones()
    {
        if((int)Main.crafttime <=Main.TimeStone && Main.crafttime > 1)
        {
            Main.TimeStone -= (int)Main.crafttime;
            Main.crafttime = 0.01f;
            Save.SaveDatas();
        }
    }

    void Start()
    {
        if (Main.tread[1])
        {
            tut.SetActive(true);
        }
        Numbers[Main.SP - 1].fontSize = 300;
        for(int i=0;i<Homelevels.Length;i++)
            Homelevels[i].text = "" + Main.Levels[i + 8];
        manualUpdate();
        for(int i=0;i<6;i++)
        {
            int a = (upgradehomeparts[i].Count) / 2;
            for (int j = 0; j < a; j++)
            {
                NeededHomeTexts[i].text += "You need " + upgradehomeparts[i][j + a] + " " + itemnames[upgradehomeparts[i][j]] + " ";
            }
        }
	}

    public void tutclose()
    {
        if (read.isOn)
        {
            Main.tread[1] = false;
        }
        tut.SetActive(false);
        Save.SaveDatas();
    }

    void Update ()
    {
        if (Main.ext != 0)
        {
            Main.ext -= Time.deltaTime;
            if (Main.ext < 0)
            {
                SceneManager.LoadScene(2);
            }
        }
        if (Main.crafttime!=0)
        {
            timeleft.text = (int)Main.crafttime + " seconds left";
            Main.crafttime -= Time.deltaTime;
            neededtimestones.text = "Spend " + (int)Main.crafttime + " TimeStones to Skip";
            CraftPanel.SetActive(true);
        }
        if(Main.crafttime <0)
        {
            Main.crafttime = 0;
            timeleft.text = (int)Main.crafttime + " seconds left";
            CraftPanel.SetActive(false);
            produce(Main.item);
            Save.SaveDatas();
        }
        if(learnclicked)
        {
            learnclicked = false;
            for(int i=0;i<lclones.Length;i++)
            {
                try
                {
                    if (learnpos == lclones[i].transform.position.y)
                    {
                        if (clones[i].transform.name == "Medkit")
                        {
                            for (int x = 0; x < Main.Levels[12] * 7 + 18; x++)
                            {
                                if (Main.HomeInventory[x] == 30&& Main.Supply[1]>0)
                                {
                                    Main.HomeInventoryC[x] -= 1;
                                    Main.Supply[1]--;
                                    Destroy(lclones[i], 0.00001f);
                                    Main.unlearneditems.Remove((Main.SP - 1) * 5 + i);
                                    Save.SaveDatas();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < Main.Levels[12] * 7 + 18; j++)
                            {
                                if (itemnames[Main.HomeInventory[j]].Replace(" ", "") + "(Clone)" == clones[i].transform.name)
                                {
                                    for (int x = 0; x < Main.Levels[12] * 7 + 18; x++)
                                    {
                                        if (Main.HomeInventory[x] == 30)
                                        {
                                            Main.HomeInventoryC[x] -= 1;
                                            Main.HomeInventoryC[j] -= 1;
                                            Destroy(lclones[i], 0.00001f);
                                            Main.unlearneditems.Remove((Main.SP - 1) * 5 + i);
                                            Save.SaveDatas();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }
    }

    public void manualUpdate()
    {
        for (int i = 0; i < 5; i++)
            Salve[i].SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            Destroy(clones[i], 0.00001f);
            Destroy(lclones[i], 0.00001f);
        }
        for (int i = (Main.SP - 1) * 5; i < Main.SP * 5; i++)
        {
            NeededTexts[i%5].text = "";
            int a = (craftings[i].Count - 1) / 2;
            for (int j=0;j<a;j++)
            {
                NeededTexts[i%5].text+="You need "+craftings[i][j+a]+" "+ itemnames[craftings[i][j]] + " ";
            }
            clones[i%5]=Instantiate(CraftableItems[i], new Vector2(CraftSlots[i%5].transform.position.x, CraftSlots[i%5].transform.position.y), Quaternion.identity);
            NameTexts[i % 5].text = itemnames[craftings[i][craftings[i].Count - 1]];
            if(usalvableitems.Contains(i))
            {
                Salve[i % 5].SetActive(false);
            }
            if (Main.unlearneditems.Contains(i))
            {
                lclones[i % 5]=Instantiate(Learn, new Vector2(CraftShows[i % 5].transform.position.x, CraftShows[i % 5].transform.position.y), Quaternion.identity);
            }
        }

    }

    void produce(bool item)
    {
        if (item)
        {
            for (int j = 0; j < Main.neededitemcount; j++)
            {
                useitems(craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][j], craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][j + Main.neededitemcount]);
            }
            additem(craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][craftings[((Main.SP - 1) * 5) + Main.itemindex - 1].Count - 1], 1);
            if (((Main.SP - 1) * 5) + Main.itemindex - 1 == 1 || ((Main.SP - 1) * 5) + Main.itemindex - 1 == 3 || ((Main.SP - 1) * 5) + Main.itemindex - 1 == 5)
            {
                additem(49, 5);
            }
        }
        else
        {
            for (int i = 0; i < upgradehomeparts[Main.itemindex - 1].Count / 2; i++)
            {
                useitems(upgradehomeparts[Main.itemindex - 1][i], upgradehomeparts[Main.itemindex - 1][i + upgradehomeparts[Main.itemindex - 1].Count / 2]);               
            }
            Main.Levels[Main.itemindex + 7]++;
            Homelevels[Main.itemindex - 1].text = "" + Main.Levels[Main.itemindex + 7];
        }
    }

    void additem(int item,int count)
    {
        for (int i = 0; i < Main.Levels[12] * 7 + 18; i++)
        {
            if (!(item > 53))
            {
                if (Main.HomeInventory[i] == 0)
                    Main.HomeInventory[i] = item;
                if (Main.HomeInventory[i] == item)
                {
                    Main.HomeInventoryC[i] += count;
                    return;
                }
            }
            else
            {
                Main.Supply[item - 54] += count;
                return;
            }
        }
    }

    public void upgrade(int index)
    {
        if(isupgradable(upgradehomeparts[index - 1].Count / 2,upgradehomeparts[index-1])&&Main.Levels[index+7]<5)
        {
            if (index + 7 == 8|| Main.Levels[index + 7] < Main.Levels[8])
            {
                Main.item = false;
                Main.itemindex = index;
                Main.crafttime = 60;
                Save.SaveDatas();
            }            
        }
    }

    public void craft(int index)
    {
        Main.neededitemcount = (craftings[((Main.SP - 1) * 5) + index - 1].Count - 1) / 2;
        Main.itemindex = index;
        if (iscraftable())
        {
            Main.crafttime = 10* Main.SP -(Main.SP *Main.Levels[9]);
            if(Main.SP==3)
            {
                Main.crafttime = 10 - Main.Levels[9];
            }
            if((Main.SP==1 && (index==1||index==3))||(Main.SP==3&&index==1))
            {
                Main.crafttime -= Main.Levels[9];
            }
            if ((Main.SP == 1 ) && (index == 2||index==4))
                Main.crafttime -= Main.Levels[11];
            if ((Main.SP == 2) && (index == 1))
                Main.crafttime -= Main.Levels[10]* Main.SP;
            if ((Main.SP == 9 || Main.SP == 8) && index == 1)
                Main.crafttime = 10 - Main.Levels[9];
            Main.item = true;
            Main.Xp += 15;
            if(Main.crafttime<1)
                Main.crafttime = 0.01f;
            Save.SaveDatas();
        }
    }

    public void salve(int index)
    {
        int itc= (craftings[((Main.SP - 1) * 5) + index - 1].Count - 1) / 2;
        int t = 0;
        if(isthere(craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][craftings[((Main.SP - 1) * 5) + Main.itemindex - 1].Count-1],1))
        {
            useitems(craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][craftings[((Main.SP - 1) * 5) + Main.itemindex - 1].Count - 1], 1);
            for (int i = 0; i < Main.Levels[12] * 7 + 18; i++)
            {
                if(t>=itc)
                {
                    Save.SaveDatas();
                    return;
                }
                if (Main.HomeInventory[i] == 0)
                {
                    Main.HomeInventory[i] = craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][t];
                }
                if(Main.HomeInventory[i] == craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][t])
                {
                    Main.HomeInventoryC[i] += craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][t + itc]/2;
                    t++;
                }
            }
        }
    }

    bool iscraftable()
    {
        for (int j = 0; j < Main.neededitemcount; j++)
        {
            if (!(isthere(craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][j], craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][j + Main.neededitemcount])))
                return false;
        }
        for (int i = 0; i < Main.Levels[12] * 7 + 18; i++)
        {
            if (Main.HomeInventory[i] == 0 || Main.HomeInventory[i] == craftings[((Main.SP - 1) * 5) + Main.itemindex - 1][craftings[((Main.SP - 1) * 5) + Main.itemindex - 1].Count - 1])
            {
                return true;
            }
        }
        return false;
    }

    bool isupgradable(int needc,List<int> neededitems)
    {
        for (int j = 0; j < needc; j++)
        {
            if (!(isthere(neededitems[j],neededitems[j + needc])))
                return false;
        }
        return true;
    }

    bool isthere(int item,int count)
    {
        for(int i=0;i<Main.Levels[12]*7+18;i++)
        {
            if(Main.HomeInventory[i]==item)
            {
                count -= Main.HomeInventoryC[i];
            }
        }
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == item)
            {
                count -= Main.BackpackInventoryC[i];
            }
        }
        if (count <= 0)
            return true;
        return false;
    }

    void useitems(int whichitem,int howmany)
    {
        for (int i = 0; i < Main.Levels[12] * 7 + 18; i++)
        {
            if (Main.HomeInventory[i] == whichitem)
            {
                if(Main.HomeInventoryC[i]>= howmany)
                {
                    Main.HomeInventoryC[i] -= howmany;
                    if (Main.HomeInventoryC[i] == 0)
                        Main.HomeInventory[i] = 0;
                    return;
                }
                else
                {
                    howmany -= Main.HomeInventoryC[i];
                    Main.HomeInventoryC[i] = 0;
                    Main.HomeInventory[i] = 0;
                }
            }
        }
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] == whichitem)
            {
                Main.BackpackInventoryC[i] -= howmany;
                if (Main.BackpackInventoryC[i] == 0)
                    Main.BackpackInventory[i] = 0;
                return;
            }
        }
    }

    public void ChangeP(int pn)
    {
        Numbers[Main.SP -1].fontSize = 220;
        Main.SP = pn;
        Numbers[pn-1].fontSize = 300;
        manualUpdate();
    }

    public void CP(bool next)
    {
        if (next && Main.SP < 9)
        {
            Numbers[Main.SP -1].fontSize = 220;
            Main.SP++;
            Numbers[Main.SP -1].fontSize = 300;
        }
        if (!next && Main.SP > 1)
        {
            Numbers[Main.SP -1].fontSize = 220;
            Main.SP--;
            Numbers[Main.SP -1].fontSize = 300;
        }
        manualUpdate();
    }

}
