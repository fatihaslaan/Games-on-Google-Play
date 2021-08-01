using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GlobalAttributes
{
    public static int star = 0;
    public static int alienUnlocked = 0;

    public static int[] maxWarriorLimit = new int[2] { 10, 12 }; //Max limit of both sides

    public static int selectedSpec = 0; //Index of selected warrior
    public static int selectedWarriorType = 0; //Selected side

    public enum Target //Target of arms
    {
        Warrior,
        Castle
    }

    public enum Type //Type of arms
    {
        Weapon,
        Shield,
        Explosive,
        None
    }

    public static WarriorDesigner[,] warriorDesigners = new WarriorDesigner[2, 5];

    public static CastleController[] castleControllers = new CastleController[2];

    public static List<IArm> humanArms = new List<IArm>() //Available arms
    {
        new SwordBehaviour(),
        new ShieldBehaviour(),
        new DynamiteBehaviour(),
        new CrossbowBehaviour(),
        new WandBehaviour(),
        new EmptyArmBehaviour()
    };
    public static List<IArm> alienArms = new List<IArm>()
    {
        new AlienArmBehaviour(),
        new AlienSharpArmBehaviour(),
        new AlienShieldArmBehaviour(),
        new AlienBombArmBehaviour(),
        new AlienDartArmBehaviour(),
        new EmptyArmBehaviour()
    };

    static List<List<IArm[]>> defaultArms = new List<List<IArm[]>>() //Default arms of both sides
    {
        new List<IArm[]>()
        {
        new IArm[2] { new SwordBehaviour(), new ShieldBehaviour() },
        new IArm[2] { new DynamiteBehaviour(), new DynamiteBehaviour() },
        new IArm[2] { new WandBehaviour(), new EmptyArmBehaviour() },
        new IArm[2] { new CrossbowBehaviour(), new CrossbowBehaviour() },
        new IArm[2] { new SwordBehaviour(), new EmptyArmBehaviour() }
        },
        new List<IArm[]>()
        {
        new IArm[4] { new AlienArmBehaviour(), new AlienArmBehaviour(), new AlienShieldArmBehaviour(), new AlienShieldArmBehaviour() },
        new IArm[4] { new AlienBombArmBehaviour(), new AlienBombArmBehaviour(), new AlienBombArmBehaviour(), new AlienBombArmBehaviour() },
        new IArm[4] { new AlienSharpArmBehaviour(), new AlienSharpArmBehaviour(), new AlienSharpArmBehaviour(), new AlienSharpArmBehaviour() },
        new IArm[4] { new AlienDartArmBehaviour(), new AlienDartArmBehaviour(), new AlienDartArmBehaviour(), new AlienDartArmBehaviour() },
        new IArm[4] { new AlienArmBehaviour(), new AlienArmBehaviour(), new AlienArmBehaviour(), new AlienArmBehaviour() }
        }
    };

    public static List<WarriorSpecBehaviour[]> specs = new List<WarriorSpecBehaviour[]> //Specs of sides warriors
    {
        new WarriorSpecBehaviour[5]
        {
            new WarriorSpecBehaviour(defaultArms[0][0]),
            new WarriorSpecBehaviour(defaultArms[0][1]),
            new WarriorSpecBehaviour(defaultArms[0][2]),
            new WarriorSpecBehaviour(defaultArms[0][3]),
            new WarriorSpecBehaviour(defaultArms[0][4])
        },
        new WarriorSpecBehaviour[5]
        {
            new WarriorSpecBehaviour(defaultArms[1][0]),
            new WarriorSpecBehaviour(defaultArms[1][1]),
            new WarriorSpecBehaviour(defaultArms[1][2]),
            new WarriorSpecBehaviour(defaultArms[1][3]),
            new WarriorSpecBehaviour(defaultArms[1][4])
        }
    };

    public static Vector3 GetSpawnPoint(Collider target, float y) //Get spawn point, related to castle position
    {
        Bounds bounds = target.bounds;
        Vector3 center = bounds.center;
        float z = Random.Range(center.z - bounds.extents.z, center.z + bounds.extents.z);

        return new Vector3(bounds.center.x, y, z);
    }

    public static void SaveDatas() //Save changes
    {
        PlayerPrefs.SetInt("star", star);
        PlayerPrefs.SetInt("alien", alienUnlocked);
        string path = Application.persistentDataPath + "/InfiniteWar_Saves.bn"; //Path of save location
        using (Stream stream = File.OpenWrite(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, specs);
        }
    }

    public static void LoadDatas() //Load changes
    {
        star = PlayerPrefs.GetInt("star");
        alienUnlocked = PlayerPrefs.GetInt("alien");
        string path = Application.persistentDataPath + "/InfiniteWar_Saves.bn";
        using (Stream stream = File.OpenRead(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            specs = (List<WarriorSpecBehaviour[]>)bf.Deserialize(stream);
        }
    }
}