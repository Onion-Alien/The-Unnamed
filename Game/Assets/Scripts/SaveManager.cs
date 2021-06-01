using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    private string playerName = "debug";
    private string currentLevel;
    private int levelsUnlocked = 3;
    private int goldCount;
    private int fragmentCount;
    private int redF1;
    private int redF2;
    private int greenF1;
    private int greenF2;
    
    public int RedF1
    {
        get => redF1;
        set => redF1 = value;
    }

    public int RedF2
    {
        get => redF2;
        set => redF2 = value;
    }

    public int GreenF1
    {
        get => greenF1;
        set => greenF1 = value;
    }

    public int GreenF2
    {
        get => greenF2;
        set => greenF2 = value;
    }

    public int FragmentCount
    {
        get => fragmentCount;
        set => fragmentCount = value;
    }

    public int GoldCount
    {
        get => goldCount;
        set => goldCount = value;
    }

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
            levelsUnlocked = data.levelsUnlocked;
            goldCount = data.goldCount;
            redF1 = data.redF1;
            redF2 = data.redF2;
            greenF1 = data.greenF1;
            greenF2 = data.greenF2;
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
        data.levelsUnlocked = levelsUnlocked;
        data.goldCount = goldCount;
        data.redF1 = redF1;
        data.redF2 = redF2;
        data.greenF1 = greenF1;
        data.greenF2 = greenF2;
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
        public int levelsUnlocked;
        public int goldCount;
        public int redF1;
        public int redF2;
        public int greenF1;
        public int greenF2;
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

    public void UnlockNextLevel()
    {
        levelsUnlocked++;
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
