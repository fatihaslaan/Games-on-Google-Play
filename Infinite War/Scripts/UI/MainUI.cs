using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    GameObject pnl_Specs, pnl_Arm, pnl_Arms, pnl_Save, btn_Play, btn_Save, btn_Reset, btn_Edit, pnl_Name, pnl_Unlock, pnl_AddStars;

    [SerializeField]
    Text txt_armName, txt_armCost, txt_totalCost, txt_Star;

    Camera camera;

    Animation cameraAnimation;

    int selectedArm;
    int currentArmIndex = 0;

    bool madeChanges;

    float totalCost = 0f;

    void Awake()
    {
        Time.timeScale = 1f;
        camera = Camera.main;
        cameraAnimation = camera.GetComponent<Animation>();
        try
        {
            GlobalAttributes.LoadDatas();
        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    void Update()
    {
        txt_Star.text = GlobalAttributes.star.ToString();
    }

    WarriorDesigner CurrentWarrior()
    {
        return GlobalAttributes.warriorDesigners[GlobalAttributes.selectedWarriorType, GlobalAttributes.selectedSpec];
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeType(int selected)
    {
        if (madeChanges)
        {
            Save();
            return;
        }

        if (selected == 1 && GlobalAttributes.alienUnlocked == 0)
        {
            pnl_Unlock.SetActive(true);
            return;
        }

        GlobalAttributes.selectedWarriorType = selected;

        camera.transform.position = new Vector3(-16 + (selected * 30f), 7f, -12f);

        pnl_Specs.SetActive(false);
        pnl_Arm.SetActive(false);
        pnl_Arms.SetActive(false);
        btn_Play.SetActive(true);
        pnl_Name.SetActive(true);
        btn_Edit.SetActive(true);
    }

    public void SelectSpec(int specId) //Select spec with index 
    {
        if (madeChanges)
        {
            Save();
            return;
        }
        GlobalAttributes.selectedSpec = specId;

        pnl_Arms.SetActive(true);
        pnl_Arm.SetActive(false);

        for (int i = 0; i < pnl_Arms.transform.childCount; i++)
        {
            if (i < CurrentWarrior().WarriorType().NumberOfArms())
            {
                pnl_Arms.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                pnl_Arms.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (GlobalAttributes.selectedWarriorType == 0)
            camera.transform.position = new Vector3(-14f + (specId * 2f), camera.transform.position.y, camera.transform.position.z);
        else
            camera.transform.position = new Vector3(8f + (specId * 2.75f), camera.transform.position.y, camera.transform.position.z);

        totalCost = GlobalAttributes.specs[CurrentWarrior().Id()][GlobalAttributes.selectedSpec].cost;
    }

    public void ChangeArm() //Change weapon of that arm
    {
        totalCost -= CurrentWarrior().WarriorType().Arms()[currentArmIndex % CurrentWarrior().WarriorType().Arms().Count].Cost();

        currentArmIndex++;
        CurrentWarrior().ChangeArm(selectedArm, currentArmIndex);

        totalCost += CurrentWarrior().WarriorType().Arms()[currentArmIndex % CurrentWarrior().WarriorType().Arms().Count].Cost();

        txt_totalCost.text = "Total Cost: " + totalCost;
        txt_armName.text = CurrentWarrior().WarriorType().Arms()[currentArmIndex % CurrentWarrior().WarriorType().Arms().Count].Name();
        txt_armCost.text = "Cost: " + CurrentWarrior().WarriorType().Arms()[currentArmIndex % CurrentWarrior().WarriorType().Arms().Count].Cost();

        if (!madeChanges)
        {
            madeChanges = true;
            btn_Reset.SetActive(true);
            btn_Save.SetActive(true);
        }
    }

    public void SelectArm(int armId) //Select arm to change its weapon
    {
        pnl_Arm.SetActive(true);

        selectedArm = armId;

        txt_totalCost.text = "Total Cost: " + totalCost;
        txt_armName.text = GlobalAttributes.specs[CurrentWarrior().Id()][GlobalAttributes.selectedSpec].arms[armId].Name();
        txt_armCost.text = "Cost: " + GlobalAttributes.specs[CurrentWarrior().Id()][GlobalAttributes.selectedSpec].arms[armId].Cost();

        for (int i = 0; i < CurrentWarrior().WarriorType().Arms().Count; i++)
            if (CurrentWarrior().WarriorType().Arms()[i].Object() == GlobalAttributes.specs[CurrentWarrior().Id()][GlobalAttributes.selectedSpec].arms[selectedArm].Object()) //Find current arm index
                currentArmIndex = i;
    }

    public void Edit()
    {
        if (GlobalAttributes.selectedWarriorType == 0)
        {
            cameraAnimation.Play("HumanCamera");
        }
        else
        {
            cameraAnimation.Play("AlienCamera");
        }

        pnl_Specs.SetActive(true);
        btn_Play.SetActive(false);
        pnl_Name.SetActive(false);
        btn_Edit.SetActive(false);
    }

    public void Save()
    {
        pnl_Save.SetActive(true);
    }

    public void CloseSave()
    {
        pnl_Save.SetActive(false);
    }

    public void SaveDesign()
    {
        if (GlobalAttributes.star >= 2)
        {
            GlobalAttributes.star -= 2;
            txt_Star.text = GlobalAttributes.star.ToString();
        }
        else
        {
            return;
        }

        CloseSave();

        pnl_Arm.SetActive(false);
        pnl_Arms.SetActive(false);
        btn_Save.SetActive(false);
        btn_Reset.SetActive(false);

        madeChanges = false;

        CurrentWarrior().Save();
        GlobalAttributes.SaveDatas();

        Edit();
    }

    public void Reset()
    {
        madeChanges = false;
        btn_Save.SetActive(false);
        btn_Reset.SetActive(false);
        CurrentWarrior().Load();
        SelectSpec(GlobalAttributes.selectedSpec);
    }

    public void AddStars()
    {
        pnl_AddStars.SetActive(true);
    }

    public void CloseAddStars()
    {
        pnl_AddStars.SetActive(false);
    }

    public void CloseUnlock()
    {
        pnl_Unlock.SetActive(false);
    }

    public void UnlockAlien() //Unlock alien side with stars
    {
        if (GlobalAttributes.star >= 10)
        {
            GlobalAttributes.star -= 10;
            pnl_Unlock.SetActive(false);
            txt_Star.text = GlobalAttributes.star.ToString();
            GlobalAttributes.alienUnlocked = 1;
            GlobalAttributes.SaveDatas();
        }
    }
}