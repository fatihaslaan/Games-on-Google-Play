using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterScreen : MonoBehaviour {

    public static float selectedx,selectedy=-100;
    public static float itemx, itemy,item2x,item2y=-100f;
    public static bool isselected,carried = false;
    public static float dodge = 0f;
    public static float loot = 0f;    
    bool notremoved = true;
    bool ishome = false;
    bool isone = false;
    bool avp = true;
    int carrieditem=0;
    int carrieditemc = 0;
    List<List<int>> allarmors = new List<List<int>>()
    {        
        new List<int>() { 14, 27, 41, 33},
        new List<int>() { 16, 29, 43, 35},
        new List<int>() { 15, 28, 42, 34 },
        new List<int>() { 13, 26, 40, 32 }
    };
    List<int> boots = new List<int>() { 13, 26, 40, 32 }; // 4 6 10 8 
    List<int> helmets = new List<int>() { 14, 27, 41, 33}; // 8 12 20 16
    List<int> pants = new List<int>() { 15, 28, 42, 34 }; // 12 18 30 24 
    List<int> chests = new List<int>() { 16, 29, 43, 35}; // 16 24 40 32 
    public static int test = 0;
    public Text Health, Hunger, Armor, BandageC, MedC, CMC, COFC;
    public Text[] InventoryC;
    public Text[] HomeC;
    public GameObject[] InventorySlots;
    public GameObject[] HomeSlots;
    public GameObject[] ArmorSlots;
    public GameObject DeleteSlot;
    public GameObject[] AllItems;
    public GameObject[] BackpackItems;
    public GameObject[] HomeItems;
    public GameObject[] ArmorItems;
    public GameObject Selected;
    public GameObject Block;
    public Canvas cnvs;
    public GameObject tut;
    public Toggle read;

    void Start ()
    {
        if (Main.tread[3])
        {
            tut.SetActive(true);
        }
        notremoved = true;
        if (Main.ext != 0)
            Block.SetActive(true);
        usei(0);
        Load();
    }

    public void tutclose()
    {
        if (read.isOn)
        {
            Main.tread[3] = false;
        }
        tut.SetActive(false);
        Save.SaveDatas();
    }

    void Update ()
    {
        if (Main.crafttime != 0)
        {
            Main.crafttime -= Time.deltaTime;
            if (Main.crafttime < 0)
            {
                SceneManager.LoadScene(1);
            }
        }
        if (Main.ext!=0)
        {
            Main.ext -= Time.deltaTime;
            if(Main.ext<0)
            {
                Block.SetActive(false);
                SceneManager.LoadScene(2);
            }
        }
        if (test != 0)
            test--;
        Selected.transform.position = new Vector2(selectedx, selectedy);
        if (carried)
        {
            carried = false;
            carry(itemx, itemy, item2x, item2y);
            item2x = 0;itemx = 0;item2y = 0;itemy = 0;
            Save.SaveDatas();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void carryall()
    {
        if (Main.ext != 0)
            return;
        for(int i=0;i<Main.Levels[7]*2+8;i++)
        {
            int a = 0;
            if (Main.BackpackInventory[i] != 0)
            {
                while (Main.Levels[12] * 2 + 18 > a)
                {
                    if (Main.HomeInventory[a] == 0)
                    {
                        Main.HomeInventory[a] = Main.BackpackInventory[i];
                        Main.HomeInventoryC[a] = 0;
                    }
                    if (Main.HomeInventory[a] == Main.BackpackInventory[i])
                    {
                        Main.HomeInventoryC[a] += Main.BackpackInventoryC[i];
                        Main.BackpackInventoryC[i] = 0;
                        Main.BackpackInventory[i] = 0;
                        a=100;
                    }
                    a++;
                }
            }
        }
        Save.SaveDatas();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void usei(int index)
    {
        if(index==1&&Main.Supply[0]>0&& Main.Health < 100)
        {
            Main.Supply[0]--;
            Main.Health += 10;
            if (Main.Health > 100)
                Main.Health = 100;
        }
        else if (index == 2 && Main.Supply[1] > 0 && Main.Health < 100)
        {
            Main.Supply[1]--;
            Main.Health += 30;
            if (Main.Health > 100)
                Main.Health = 100;
        }
        else if (index == 3 && Main.Supply[2] > 0 && Main.Hunger < 100)
        {
            Main.Supply[2]--;
            Main.Hunger += 10;
            if (Main.Hunger > 100)
                Main.Hunger = 100;
        }
        else if (index == 4 && Main.Supply[3] > 0 && Main.Hunger < 100)
        {
            Main.Supply[3]--;
            Main.Hunger += 30;
            if (Main.Hunger > 100)
                Main.Hunger = 100;
        }
        Armor.text = "" + Main.Armor;
        Health.text = "" + Main.Health;
        Hunger.text = "" + Main.Hunger;
        BandageC.text = "" + Main.Supply[0];
        MedC.text = "" + Main.Supply[1];
        CMC.text = "" + Main.Supply[2];
        COFC.text = "" + Main.Supply[3];
        Save.SaveDatas();
    }

    void carry(float tx,float ty,float t2x,float t2y)
    {
        int index=0;
        for(int i=0;i<4;i++)
        {
            if(tx==ArmorSlots[i].transform.position.x&&ty==ArmorSlots[i].transform.position.y)
            {
                removearmor(i);
                return;
            }
        }
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (tx==InventorySlots[i].transform.position.x&&ty==InventorySlots[i].transform.position.y)
            {
                carrieditem=Main.BackpackInventory[i];
                carrieditemc=Main.BackpackInventoryC[i];
                index = i;
            }
        }
        for (int i = 0; i < Main.Levels[12] * 7 + 18; i++)
        {
            if (tx == HomeSlots[i].transform.position.x && ty == HomeSlots[i].transform.position.y)
            {
                carrieditem = Main.HomeInventory[i];
                carrieditemc = Main.HomeInventoryC[i];
                index = i;
                ishome = true;
            }
        }
        if(t2x==DeleteSlot.transform.position.x&&t2y==DeleteSlot.transform.position.y)
        {
            if (ishome)
            {
                Main.HomeInventoryC[index] = 0;
                Main.HomeInventory[index] = 0;                
                ishome = false;
            }
            else
            {
                Main.BackpackInventoryC[index] = 0;
                Main.BackpackInventory[index] = 0;
            }
        }
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (t2x == InventorySlots[i].transform.position.x && t2y == InventorySlots[i].transform.position.y)
            {
                if (tx == t2x && ty == t2y)
                {
                    int a = 0;
                    isone = true;
                    while (Main.Levels[12] * 2 + 18 > a)
                    {
                        if (Main.HomeInventory[a] == 0)
                        {
                            Main.HomeInventory[a] = carrieditem;
                            Main.HomeInventoryC[a] = 0;
                        }
                        if (Main.HomeInventory[a] == carrieditem)
                        {
                            Main.HomeInventoryC[a] += carrieditemc;
                            Main.BackpackInventoryC[index] = 0;
                            Main.BackpackInventory[index] = 0;
                            return;
                        }
                        a++;
                    }
                }
                if (ishome)
                {
                    if (Main.HomeInventory[index] == Main.BackpackInventory[i])
                    {
                        Main.HomeInventoryC[index] = 0;
                        Main.HomeInventory[index] = 0;
                    }
                    else
                    {
                        Main.HomeInventoryC[index] = Main.BackpackInventoryC[i];
                        Main.HomeInventory[index] = Main.BackpackInventory[i];
                    }                                  
                    ishome = false;
                }
                else
                {
                    if (Main.BackpackInventory[index] == Main.BackpackInventory[i])
                    {
                        Main.BackpackInventoryC[index] = 0;
                        Main.BackpackInventory[index] = 0;
                    }
                    else
                    {
                        Main.BackpackInventoryC[index] = Main.BackpackInventoryC[i];
                        Main.BackpackInventory[index] = Main.BackpackInventory[i];
                    }
                    
                }

                if (!isone)
                {
                    if (Main.BackpackInventory[i] == carrieditem)
                    {
                        Main.BackpackInventoryC[i] += carrieditemc;
                    }
                    else
                    {
                        Main.BackpackInventoryC[i] = carrieditemc;
                    }
                    Main.BackpackInventory[i] = carrieditem;
                }
                else
                {
                    isone = false;
                }
                
            }
        }
        for (int i = 0; i < Main.Levels[12] * 7 + 18; i++)
        {
            if (t2x == HomeSlots[i].transform.position.x && t2y == HomeSlots[i].transform.position.y)
            {
                if(tx==t2x&&ty==t2y)
                {
                    int a = 0;
                    isone = true;
                    while (Main.Levels[7] * 2 + 8 > a)
                    {
                        if (Main.BackpackInventory[a] == 0)
                        {
                            Main.BackpackInventory[a] = carrieditem;
                            Main.BackpackInventoryC[a] = 0;
                        }
                        if (Main.BackpackInventory[a] == carrieditem)
                        {
                            Main.BackpackInventoryC[a] += 1;
                            Main.HomeInventoryC[index] -= 1;
                            return;
                        }
                        a++;
                    }
                }
                else if (ishome)
                {
                    if (Main.HomeInventory[index] == Main.HomeInventory[i])
                    {
                        Main.HomeInventoryC[index] = 0;
                        Main.HomeInventory[index] = 0;
                    }
                    else
                    {
                        Main.HomeInventoryC[index] = Main.HomeInventoryC[i];
                        Main.HomeInventory[index] = Main.HomeInventory[i];
                    }                    
                    ishome = false;
                }
                else
                {
                    if (Main.BackpackInventory[index] == Main.HomeInventory[i])
                    {
                        Main.BackpackInventory[index]  = 0;
                        Main.BackpackInventoryC[index] = 0;
                    }
                    else
                    {
                        Main.BackpackInventoryC[index] = Main.HomeInventoryC[i];
                        Main.BackpackInventory[index]  = Main.HomeInventory[i];
                    }
                    
                }

                if (!isone)
                {
                    if (Main.HomeInventory[i] == carrieditem)
                    {
                        Main.HomeInventoryC[i] += carrieditemc;
                    }
                    else
                    {
                        Main.HomeInventoryC[i] = carrieditemc;
                    }
                    Main.HomeInventory[i] = carrieditem;
                }
                else
                {
                    isone = false;
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (allarmors[i].Contains(carrieditem))
            {
                if (t2x == ArmorSlots[i].transform.position.x && t2y == ArmorSlots[i].transform.position.y)
                {
                    if (Main.Armors[i] > 0)
                    {
                        avp = false;
                        removearmor(i);
                    }
                    if (avp)
                    {
                        if (ishome)
                        {
                            if (carrieditemc > 1)
                                Main.HomeInventoryC[index]--;
                            else
                            {
                                Main.HomeInventoryC[index] = 0;
                                Main.HomeInventory[index]  = 0;
                            }
                        }
                        else
                        {
                            if (carrieditemc > 1)
                                Main.BackpackInventoryC[index]--;
                            else
                            {
                                Main.BackpackInventoryC[index] = 0;
                                Main.BackpackInventoryC[index] = 0;
                            }
                        }
                        Main.Armors[i] = carrieditem;
                        armorcalculate(carrieditem, false);
                    }
                }
            }
        }
    }

    void Load()
    {
        for(int i=0;i<4;i++)
        {
            if (Main.Armors[i] > 0)
            {
                ArmorItems[i] = Instantiate(AllItems[Main.Armors[i]], new Vector2(ArmorSlots[i].transform.position.x, ArmorSlots[i].transform.position.y), Quaternion.identity);
            }

        }
        for (int i = 0; i < Main.Levels[7] * 2 + 8; i++)
        {
            if (Main.BackpackInventory[i] != 0 && Main.BackpackInventoryC[i] <= 0)
                Main.BackpackInventory[i] = 0;
            if (i >= 1)
            {
                if (i % 3 == 0)
                {
                    InventorySlots[i] = Instantiate(InventorySlots[0], new Vector2(InventorySlots[0].transform.position.x, InventorySlots[i - 1].transform.position.y - 0.6f), Quaternion.identity);
                }
                else
                {
                    InventorySlots[i] = Instantiate(InventorySlots[0], new Vector2(InventorySlots[i - 1].transform.position.x + 0.62f, InventorySlots[i - 1].transform.position.y), Quaternion.identity);
                }
                InventoryC[i] = Instantiate(InventoryC[i - 1], new Vector2(InventorySlots[i].transform.position.x + 0.008f, InventorySlots[i].transform.position.y + 0.204f), Quaternion.identity);
                InventoryC[i].transform.SetParent(cnvs.transform, false);
                InventoryC[i].text = "0";
            }
            if (Main.BackpackInventory[i] != 0 && Main.BackpackInventoryC[i] > 0)
            {
                BackpackItems[i] = Instantiate(AllItems[Main.BackpackInventory[i]], new Vector2(InventorySlots[i].transform.position.x, InventorySlots[i].transform.position.y), Quaternion.identity);
                InventoryC[i].text = Main.BackpackInventoryC[i].ToString();
            }
        }
        for (int i = 0; i < Main.Levels[12] * 7 + 18; i++)
        {
            if (Main.HomeInventory[i] != 0 && Main.HomeInventoryC[i] <= 0)
                Main.HomeInventory[i] = 0;
            if (i >= 1)
            {
                if (i % 5 == 0)
                {
                    HomeSlots[i] = Instantiate(HomeSlots[0], new Vector2(HomeSlots[0].transform.position.x, HomeSlots[i - 1].transform.position.y - 0.665f), Quaternion.identity);
                }
                else
                {
                    HomeSlots[i] = Instantiate(HomeSlots[0], new Vector2(HomeSlots[i - 1].transform.position.x + 0.64f, HomeSlots[i - 1].transform.position.y), Quaternion.identity);
                }
                HomeC[i] = Instantiate(HomeC[i - 1], new Vector2(HomeSlots[i].transform.position.x + 0.008f, HomeSlots[i].transform.position.y + 0.204f), Quaternion.identity);
                HomeC[i].transform.SetParent(cnvs.transform, false);
                HomeC[i].text = "0";
            }
            if (Main.HomeInventory[i] != 0 && Main.HomeInventoryC[i] > 0 )
            {
                HomeItems[i] = Instantiate(AllItems[Main.HomeInventory[i]], new Vector2(HomeSlots[i].transform.position.x, HomeSlots[i].transform.position.y), Quaternion.identity);
                HomeC[i].text = Main.HomeInventoryC[i].ToString();
            }
        }
    }

    void armorcalculate(int armor,bool remove)
    {
        int[] armorlistindexchanger = { 1, 3, 2, 0 };
        int index = 0;
        int j = 0;
        int arm=0;
        float dodgei = 0f;
        float looti = 0f;
        for (int i = 0; i < 4; i++)
        {
            if (allarmors[i].Contains(armor))
            {
                index = allarmors[i].IndexOf(armor);
                j = i;
            }
        }
        if (index == 0)
        {
            arm = (armorlistindexchanger[j] * 4) + 4;
        }
        if(index==1)
        {
            looti = (armorlistindexchanger[j] * 0.1f) + 0.1f;
            arm = (armorlistindexchanger[j] * 6) + 6;
        }
        if(index==2)
        {
            dodgei = (armorlistindexchanger[j] * 0.1f) + 0.1f;
            arm= (armorlistindexchanger[j] * 8) + 8;
        }
        if(index==3)
        {
            arm = (armorlistindexchanger[j] * 10) + 10;
        }
        if (remove&&notremoved)
        {
            Main.Armor -= arm;
            dodge -= dodgei;
            loot -= looti;
            notremoved = false;
        }
        else if(!remove)
        {
            Main.Armor += arm;
            dodge += dodgei;
            loot += looti;
        }
    }

    void removearmor(int i)
    {
        int a = 0;
        while (Main.Levels[12] * 7 + 18 > a)
        {
            if (Main.HomeInventory[a] == 0)
            {
                Main.HomeInventory[a] = Main.Armors[i];
                Main.HomeInventoryC[a] = 0;
            }
            if (Main.HomeInventory[a] == Main.Armors[i])
            {
                Main.HomeInventoryC[a] += 1;
                armorcalculate(Main.Armors[i], true);
                Main.Armors[i] = 0;
                avp = true;
            }
            a++;
        }
    }

}
