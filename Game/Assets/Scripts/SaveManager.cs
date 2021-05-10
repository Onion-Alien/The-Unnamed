using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string playerName;
    private string currentLevel;
    public float x, y;

    public static SaveManager instance {get; private set; }

    //Object has DontDestroyOnload so it can be used by all scenes
    private void Awake()
    {
        playerName = "temp";
        if (instance != null && instance != this)
            Destroy(gameObject);
        else if(instance == null)
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
            x = data.x; y = data.y;
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
        data.x = x;data.y = y;
        bf.Serialize(file, data);
        file.Close();
    }

    public void checkPointSave(GameObject player)
    {
        Debug.Log("player x = " + player.transform.position.x + "player y = " + player.transform.position.y);

        x = player.transform.position.x;
        y = player.transform.position.y;
        Save();
    }

    [Serializable]
    
    //inner class to represent save file data
    class PlayerData_Storage
    {
        public string playerName;
        public string currentLevel;
        public float x, y;
    }

    public void savePlayerPosition(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public void saveName(string n)
    {
        this.playerName = n;
    }

    public void saveLevel(string s)
    {
        this.currentLevel = s;
    }

    public bool checkSaveExist()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            return true;
        }
        return false;
    }

    public string getCurrentLevel()
    {
        return currentLevel;
    }

    public string getName()
    {
        return playerName;
    }

    public float getCharX()
    {
        return x;
    }

    public float getCharY()
    {
        return y;
    }
}
