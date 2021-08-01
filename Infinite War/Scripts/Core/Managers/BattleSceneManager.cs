using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public List<WarriorController>[] warriors = new List<WarriorController>[2] { new List<WarriorController>(), new List<WarriorController>() }; //Warriors on battlefield

    public GameObject humanCastle, alienCastle;
    public GameObject human, alien;

    public GameObject GameOver, GameOverText;

    static BattleSceneManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public static BattleSceneManager Instance()
    {
        return instance;
    }
}