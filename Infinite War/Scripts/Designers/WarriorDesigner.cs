using System.Collections.Generic;
using UnityEngine;

public abstract class WarriorDesigner : MonoBehaviour
{
    public int specId;

    List<Transform> armPaths;
    IArm[] arms;
    List<GameObject> armObjects;

    int emptyArmCounter = 0;

    void Start()
    {
        Load();
    }

    public void Load() //Load arms
    {
        if (armObjects != null)
        {
            foreach (GameObject o in armObjects)
            {
                Destroy(o);
            }
        }
        armPaths = new List<Transform>();
        armObjects = new List<GameObject>();
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            if (t.name != "Canvas")
            {
                armPaths.Add(t);
            }
        }
        arms = new IArm[armPaths.Count];
        for (int i = 0; i < armPaths.Count; i++)
        {
            armObjects.Add(null);
            arms[i] = (GlobalAttributes.specs[Id()][specId].arms[i]);
            if (GlobalAttributes.specs[Id()][specId].arms[i].Object() != null)
            {
                armObjects[i] = (Instantiate(arms[i].Object(), armPaths[i], false));
            }
        }

        GlobalAttributes.warriorDesigners[Id(), specId] = this;
    }

    void CheckArms()
    {
        emptyArmCounter = 0;
        for (int i = 0; i < armObjects.Count; i++) //Get number of empty arms to prevent a combination with empty arms
        {
            if (armObjects[i] == null)
                emptyArmCounter++;
        }
    }

    public void ChangeArm(int selectedArm, int armIndex)
    {
        CheckArms();
        if (armIndex % WarriorType().Arms().Count == WarriorType().Arms().Count - 1 && emptyArmCounter == WarriorType().NumberOfArms() - 1) //Change arm
        {
            armIndex++;
        }
        Destroy(armObjects[selectedArm]); //Destroy old arm
        if (WarriorType().Arms()[armIndex % WarriorType().Arms().Count].Object() != null)
            armObjects[selectedArm] = Instantiate(WarriorType().Arms()[armIndex % WarriorType().Arms().Count].Object(), armPaths[selectedArm], false);//Instantiate new one
        arms[selectedArm] = WarriorType().Arms()[armIndex % WarriorType().Arms().Count];
    }

    public void Save() //Save current arms
    {
        WarriorSpecBehaviour spec = new WarriorSpecBehaviour(arms);
        GlobalAttributes.specs[Id()][GlobalAttributes.selectedSpec] = spec;
        GlobalAttributes.SaveDatas();
    }

    public abstract WarriorBehaviour WarriorType();

    public abstract int Id();
}