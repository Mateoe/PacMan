using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoad
{
    public static void Save<T>(T data,string Path, string Filename)
    {
        string fullPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/" + Path + "/";

        bool FolderExist = Directory.Exists(fullPath);

        if (FolderExist == false)
        {
            Directory.CreateDirectory(fullPath);
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(fullPath + Filename + ".json",json);

    }

    public static T Load<T>(string Path, string Name)
    {
        string fullPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/" + Path + "/"+ Name + ".json";
        
        if(File.Exists(fullPath))
        {
            string text = File.ReadAllText(fullPath);
            var obj = JsonUtility.FromJson<T>(text);
            return obj;
        }
        else
        {
            return default;
        }
    }
}
