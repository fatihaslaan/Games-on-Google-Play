using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Save : MonoBehaviour
{
    public static void SaveDatas()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Runner_Saves.bn";
        FileStream stream = new FileStream(path, FileMode.Create);
        GD datas = new GD(Global.unlockedshop,Global.colorindex,Global.unlockedmodes,Global.modeindex,Global.flash,Global.flashlight,Global.englang,Global.music,Global.sound,Global.highscore,Global.tut);
        formatter.Serialize(stream, datas);
        stream.Close();
    }

    public static GD GetData()
    {
        string path = Application.persistentDataPath + "/Runner_Saves.bn";
        FileStream stream = new FileStream(path, FileMode.Open);
        if (File.Exists(path)&&stream.Length>0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            GD datas = formatter.Deserialize(stream) as GD;
            stream.Close();
            return datas;
        }
        else
        {
            return null;
        }
    }
}