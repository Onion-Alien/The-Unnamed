using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string playerName;
    private int currentLevel;

    public static SaveManager instance {get; private set; }

    //Object has DontDestroyOnload so it can be used by all scenes
    private void Awake()
    {
        playerName = "temp";
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }

    //Load strings from txt file playerInfo.dat if it exists
    public void Load()
    {

        if (checkSaveExist()) { 
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            playerName = data.playerName;
            currentLevel = data.currentLevel;
            file.Close();
        }
    }

    //Save the game, converts string data to binary to be more efficient
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();
        data.playerName = this.playerName;
        data.currentLevel = this.currentLevel;

        bf.Serialize(file, data);
        file.Close();
    }

    [Serializable]
    
    //inner class to represent save file data
    class PlayerData_Storage
    {
        public string playerName;
        public int currentLevel;
    }

    public void saveName(string n)
    {
        this.playerName = n;
    }

    public void saveLevel(int n)
    {
        this.currentLevel = n;
    }

    public bool checkSaveExist()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            return true;
        }
        return false;
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public string getName()
    {
        return playerName;
    }
}
