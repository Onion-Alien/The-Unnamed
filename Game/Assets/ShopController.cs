using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public ParticleSystem buttonGlow;
    public GameObject sellCanvas;
    public static GameObject selectedButton;
    public GameObject staminaRing, healthRing;
    public Button purchaseTab, sellItemTab;
    public Text goldText, buyButton;
    private int playerGold, itemCost;
    private string itemName, boughtItem;
    private bool buttonSelected, sellActive;
    public GameObject player;
    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        disableParticles();
        //  buttonGlow.enableEmission = false;
        sellCanvas.SetActive(false);
        // playerGold = SaveManager.instance.getPlayerGold();
        playerGold = 14;
    }
    void OnDisable()
    {
        buttonGlow.enableEmission = false;
        resetVariables();
        boughtItem = "";
        playerRB.constraints = RigidbodyConstraints2D.None;
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezePosition;
        goldText.text = "Your Gold: " + playerGold.ToString();
        string temp = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        
        if (temp != "Return" && temp != "Purchase" && buttonSelected)
        {
            buyButton.text = "Buy Item";
            selectedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            buttonGlow.enableEmission = true;
            buttonGlow.emissionRate = 1.0f;
        }
            
        if (selectedButton != null && (selectedButton.name != "Return" || selectedButton.name != "Purchase" || selectedButton.name != "purchaseTab" || selectedButton.name != "sellTab" || selectedButton.name != "Addgold") && buttonSelected)
        {
            switch (selectedButton.name)
            {
                case ("UpgradeHealth"):
                    itemCost = 18;
                    itemName = "UpgradeHealth";
                    if(boughtItem == "UpgradeHealth") {
                      //Cant do health yet, no health variable  PlayerCombat.setheal
                    }
                    break;
                case ("UpgradeStamina"):
                    itemCost = 13;
                    itemName = "UpgradeStamina";
                    if (boughtItem == "UpgradeStamina")
                    {
                        PlayerCombat.setStamina(PlayerCombat.getStamina() + 15.0f);
                    }
                    break;
                case ("UpgradeDamage"):
                    itemCost = 20;
                    itemName = "UpgradeDamage";
                    if (boughtItem == "UpgradeDamage")
                    {
                        int[] tempArr = PlayerCombat.getDMG();
                        for(int i = 0; i <= 2; i++)
                        {
                            tempArr[i] += 3;
                        }
                        PlayerCombat.setDMG(tempArr);
                    }
                    break;
                case ("UpgradeAttackSpeed"):
                    itemCost = 33;
                    itemName = "UpgradeAttackSpeed";
                    if (boughtItem == "UpgradeAttackSpeed")
                    {
                        PlayerCombat.setAttackRate(PlayerCombat.getAttackRate() + 0.1f);
                    }
                    break;
                case ("UpgradeRunSpeed"):
                    itemCost = 47;
                    itemName = "UpgradeRunSpeed";
                    if (boughtItem == "UpgradeRunSpeed")
                    {
                        PlayerController.movementSpeed += 0.5f;
                    }
                    break;
                case ("HealthPotButton"):
                    itemCost = 14;
                    itemName = "HealthPotButton";
                    if (boughtItem == "HealthPotButton")
                    {
                        
                    }
                    break;
                case ("StaminaPotButton"):
                    itemCost = 12;
                    itemName = "StaminaPotButton";
                    if (boughtItem == "StaminaPotButton")
                    {

                    }
                    break;
                case ("StaminaRingButton"):
                    itemCost = 115;
                    itemName = "StaminaRingButton";
                    if (boughtItem == "StaminaRingButton")
                    {
                        PlayerCombat.setStaminaRegen(PlayerCombat.getStaminaRegen() - 0.2f);
                        staminaRing.SetActive(false);
                    }
                    break;
                case ("HealthRingButton"):
                    itemCost = 98;
                    itemName = "HealthRingButton";
                    if (boughtItem == "HealthRingButton")
                    {
                        //Cant do yet

                    }
                    break;
            }
            
        }
        if(boughtItem != "")
        {
            boughtItem = "";
            resetVariables();
        }

        if (playerGold >= itemCost)
        {
            buttonGlow.startColor = Color.green;
        }
        else
        {
            buttonGlow.startColor = Color.red;
        }
    }


    public void toggleButtonSelected()
    {
        buttonSelected = true;
    }

    public void addGold()
    {
        playerGold += 50;
    }

    public void switchSellTab()
    {
        if (!sellActive)
        {
            buttonGlow.enableEmission = false;
            disableParticles();
            purchaseTab.transform.GetComponent<Image>().color = sellItemTab.transform.GetComponent<Image>().color;
            sellItemTab.transform.GetComponent<Image>().color = Color.white;
            sellCanvas.gameObject.SetActive(true);
            buyButton.text = "Sell Item";
            sellActive = true;

            resetVariables();
        }
    }

    public void switchPurchaseTab()
    {
        if (sellActive)
        {
            sellItemTab.transform.GetComponent<Image>().color = purchaseTab.transform.GetComponent<Image>().color;
            sellCanvas.gameObject.SetActive(false);
            purchaseTab.transform.GetComponent<Image>().color = Color.white;
            buyButton.text = "Buy Item";
            sellActive = false;
        }
    }

    public void disableParticles()
    {
        buttonGlow.emissionRate = 0.0f;
    }

    public void buyItem()
    {
        if(playerGold >= itemCost && buyButton.text != "Sell Item")
        {
            playerGold = playerGold - itemCost;
            boughtItem = itemName;
            disableParticles();
            buttonSelected = true;
        }
        else
        {
            buyButton.text = "Not Enough Money!";
            disableParticles();
            resetVariables();
        }
    }

    public void resetVariables()
    {
        selectedButton = null;
        buttonSelected = false;
        itemCost = 0;
        boughtItem = "";
    }
}
