using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSceneUI : MonoBehaviour
{
    [SerializeField]
    List<Text> txt_Specs;
    [SerializeField]
    List<Transform> object_Locations;
    [SerializeField]
    Text txt_Gold, txt_SkillCost;

    void Start()
    {
        Camera.main.transform.position = new Vector3(-16 + (GlobalAttributes.selectedWarriorType * 32), 7, -12);
        for (int i = 0; i < txt_Specs.Count; i++)
        {
            txt_Specs[i].text = GlobalAttributes.specs[GlobalAttributes.selectedWarriorType][i].cost.ToString();

            GameObject tmp = Instantiate(GlobalAttributes.castleControllers[GlobalAttributes.selectedWarriorType].Warrior(), object_Locations[i], false); //Load 5 warrior template with WarriorDesigner class to make a template

            if (GlobalAttributes.selectedWarriorType == 0)
                tmp.AddComponent<HumanDesigner>().specId = i;
            else
                tmp.AddComponent<AlienDesigner>().specId = i;
            Destroy(tmp.GetComponent<NavMeshAgent>());
            tmp.transform.localScale = new Vector3(24, 24, 24);
        }
        txt_SkillCost.text = GlobalAttributes.castleControllers[GlobalAttributes.selectedWarriorType].skillCost.ToString();
    }

    void Update()
    {
        txt_Gold.text = GlobalAttributes.castleControllers[GlobalAttributes.selectedWarriorType].gold.ToString();
    }

    public void ChooseWarrior(int index) //Spawn selected warrior
    {
        if (GlobalAttributes.castleControllers[GlobalAttributes.selectedWarriorType].gold >= GlobalAttributes.specs[GlobalAttributes.selectedWarriorType][index].cost)
        {
            if (BattleSceneManager.Instance().warriors[GlobalAttributes.selectedWarriorType].Count < GlobalAttributes.maxWarriorLimit[GlobalAttributes.selectedWarriorType])
            {
                GlobalAttributes.castleControllers[GlobalAttributes.selectedWarriorType].currentWarrior = index;
                GlobalAttributes.castleControllers[GlobalAttributes.selectedWarriorType].SpawnWarrior();
                GlobalAttributes.castleControllers[GlobalAttributes.selectedWarriorType].gold -= GlobalAttributes.specs[GlobalAttributes.selectedWarriorType][index].cost;
            }
        }
    }

    public void Skill() //Castle Skill
    {
        GlobalAttributes.castleControllers[GlobalAttributes.selectedWarriorType].SkillAnimation();
    }

    public void GameOver()
    {
        GlobalAttributes.star++;
        SceneManager.LoadScene(0);
    }
}