using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    
    
   public static void Save(PlayerMove player)
    {
        BinaryFormatter form = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.me";
        FileStream str = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        form.Serialize(str, data);
        str.Close();
    }

    public static PlayerData Load()
    {
        string path = Application.persistentDataPath + "/player.me";
        if(File.Exists(path))
        {
            BinaryFormatter form = new BinaryFormatter();
            FileStream str = new FileStream(path, FileMode.Open);
          PlayerData data =  form.Deserialize(str) as PlayerData;
            str.Close();
            return data;
        }
        else
        {
            return null;
        }

       
    }

    public static void SaveWorld(PlayerMove player, int level)
    {
        BinaryFormatter form = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player"+level.ToString();
        FileStream str = new FileStream(path, FileMode.Create);

        WorldData data = new WorldData(player);

        form.Serialize(str, data);
        str.Close();
    }

    public static WorldData LoadWorld(int level)
    {
        string path = Application.persistentDataPath + "/player" + level.ToString();
        if (File.Exists(path))
        {
            BinaryFormatter form = new BinaryFormatter();
            FileStream str = new FileStream(path, FileMode.Open);
           WorldData data = form.Deserialize(str) as WorldData;
            str.Close();
            return data;
        }
        else
        {
            return null;
        }
    }


    public static void ClearMemory()
    {
        for(int i =0;i<25; i++)
        {
            string path = Application.persistentDataPath + "/player" + i.ToString();
            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
    }

    public static void ClearMemoryForPlayer()
    {
        string path = Application.persistentDataPath + "/player.me";
        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }







}
