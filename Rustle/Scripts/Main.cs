using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main: MonoBehaviour
{
    public static bool adshow = false;

    public static bool[] tread= {true, true, true, true};
    public static int TimeStone = 350;
    public static int[] HomeInventory=new int[53];
    public static int[] HomeInventoryC = new int[53];
    public static int[] BackpackInventory=new int[18];
    public static int[] BackpackInventoryC = new int[18];
    public static int[] Armors = new int[4];
    public static int[] Supply = new int[4];
    public static int[] Levels = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static int Health = 100;
    public static int Hunger = 100;
    public static int Armor = 0;
    public static int Xp = 0;
    public static int SkillsA = 0;
    public static ArrayList unlearneditems = new ArrayList() { 12, 13, 14, 15, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 36, 37, 38, 39, 41, 42, 43, 44 };
    public static string Progress="";
    public static float crafttime = 0; 
    public static float ext = 0;
    public static int neededitemcount = 0;
    public static int itemindex = 0;
    public static bool item=true;
    public static int exindex = 0;
    public static float risk = 0.1f;
    public static float attackchance = 0f;
    public static float loot = 1;
    public static float aim = 0.05f;
    public static int SP = 1;

    public static void Set()
    {
        MD datas = Save.GetData();
        tread = datas.tread;
        TimeStone = datas.TimeStone;
        HomeInventory = datas.HomeInventory;
        HomeInventoryC = datas.HomeInventoryC;
        BackpackInventory = datas.BackpackInventory;
        BackpackInventoryC = datas.BackpackInventoryC;
        Armors = datas.Armors;
        Supply = datas.Supply;
        Levels = datas.Levels;
        Health = datas.Health;
        Hunger = datas.Hunger;
        Armor = datas.Armor;
        Xp = datas.Xp;
        SkillsA = datas.SkillsA;
        unlearneditems = datas.unlearneditems;
        Progress = datas.Progress;
        crafttime = datas.crafttime;
        ext = datas.ext;
        neededitemcount = datas.neededitemcount;
        itemindex = datas.itemindex;
        item = datas.item;
        exindex = datas.exindex;
        risk = datas.risk;
        attackchance = datas.attackchance;
        loot = datas.loot;
        aim = datas.aim;
        SP = datas.SP;
    }

}
