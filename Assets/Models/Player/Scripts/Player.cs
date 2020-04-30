using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int xp = 0; //Actual xp
    [SerializeField] private int requiredXp = 100; //What we need to get
    [SerializeField] private int levelBase = 50;
    //[SerializeField] private List<GameObject> spots = new List<GameObject>(); //Collection of spots
    private int lvl = 1;

    private string path;

    //Getters-----------------------------------------------
    public int Xp
    {
        get { return xp; }
    }

    public int RequiredXp
    {
        get { return requiredXp; }
    }

    public int LevelBase
    {
        get { return levelBase; }
    }

    public int Lvl
    {
        get { return lvl; }
    }
    //------------------------------------------------------

    private void Start()
    {
        path = Application.persistentDataPath + "/player1.dat";
        Load();
    }

    public void AddXp(int xp)
    {
        this.xp += Mathf.Max(0, xp);
        InitLevelData();
        Save();
    }

    //Set up our level and experience
    private void InitLevelData() 
    {
        lvl = (xp / levelBase) + 1; //Minimum level is 1
        requiredXp = levelBase * lvl;
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        PlayerData data = new PlayerData(this);
        bf.Serialize(file, data);
        file.Close();
    }

    private void Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            xp = data.Xp;
            requiredXp = data.RequiredXp;
            levelBase = data.LevelBase;
            lvl = data.Lvl;
        }
        else
        {
            InitLevelData();
        }
    }

}
