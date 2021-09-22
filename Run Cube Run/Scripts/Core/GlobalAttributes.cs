using UnityEngine;

public static class GlobalAttributes
{
    public static int currentLevel = 1;
    public static int maxLevel = 1; //How many levels we have unlocked

    public static void Save()
    {
        PlayerPrefs.SetInt("currentLevel",currentLevel);
        PlayerPrefs.SetInt("maxLevel",maxLevel);
    }

    public static void Load()
    {
        currentLevel=PlayerPrefs.GetInt("currentLevel");
        maxLevel=PlayerPrefs.GetInt("maxLevel");
    }
}