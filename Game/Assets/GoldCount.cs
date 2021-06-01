using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCount : MonoBehaviour
{
    public Text gCount;

    public int gold = 0;

    public static GoldCount gc;

    private void Awake()
    {
        gc = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void addGold(int drop)
    {
        gold += drop;
        gCount.text = "Gold: " + gold.ToString();
    }

    public int increaseGold(int drop)
    {
        gold += drop;
        return this.gold;
    }
}
