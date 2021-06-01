using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    public ParticleSystem buttonGlow;
    public GameObject sellCanvas;
    public static GameObject selectedButton;
    public GameObject staminaRing, healthRing;
    public GameObject purchaseTab, sellItemTab, buyButtonObj;
    public Text goldText, buyButton;
    private int playerGold, itemCost;
    private string itemName, boughtItem;
    private bool buttonSelected, sellActive;
    public GameObject player;
    private Rigidbody2D playerRB;
    private Color originalButtonColor, modifiedColor;
    private PlayerCombat pc;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        modifiedColor = Color.white;
        sellCanvas.gameObject.SetActive(false);
        modifiedColor.a = 0.5f;
        playerRB = player.GetComponent<Rigidbody2D>();
        disableParticles();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //originalButtonColor = buyButtonObj.colors.normalColor;
        //buttonGlow.enableEmission = false;
        //playerGold = SaveManager.instance.getPlayerGold();
        playerGold = 14;
    }
    void OnDisable()
    {
        var emission = buttonGlow.emission;
        emission.enabled = false;
        resetVariables();
        boughtItem = "";
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
            buttonGlow.Play();
        }

        if (selectedButton != null && (selectedButton.name != "Return" || selectedButton.name != "Purchase" || selectedButton.name != "purchaseTab" || selectedButton.name != "sellTab" || selectedButton.name != "Addgold") && buttonSelected)
        {
            switch (selectedButton.name)
            {
                case ("UpgradeHealth"):
                    itemCost = 18;
                    itemName = "UpgradeHealth";
                    if (boughtItem == "UpgradeHealth")
                    {
                        //Cant do health yet, no health variable  PlayerCombat.setheal
                    }
                    break;
                case ("UpgradeStamina"):
                    itemCost = 13;
                    itemName = "UpgradeStamina";
                    if (boughtItem == "UpgradeStamina")
                    {
                        pc.setStamina(pc.getStamina() + 15.0f);
                    }
                    break;
                case ("UpgradeDamage"):
                    itemCost = 20;
                    itemName = "UpgradeDamage";
                    if (boughtItem == "UpgradeDamage")
                    {
                        pc.dmgLight =+ 3;
                        pc.dmgHeavy =+ 3;
                    }
                    break;
                case ("UpgradeAttackSpeed"):
                    itemCost = 33;
                    itemName = "UpgradeAttackSpeed";
                    if (boughtItem == "UpgradeAttackSpeed")
                    {
                        pc.setAttackRate(pc.getAttackRate() + 0.1f);
                    }
                    break;
                case ("UpgradeRunSpeed"):
                    itemCost = 47;
                    itemName = "UpgradeRunSpeed";
                    if (boughtItem == "UpgradeRunSpeed")
                    {
                        playerController.movementSpeed += 0.5f;
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
                    itemCost = 98;
                    itemName = "StaminaRingButton";
                    if (boughtItem == "StaminaRingButton")
                    {
                        pc.setStaminaRegen(pc.getStaminaRegen() - 0.2f);
                        staminaRing.SetActive(false);
                    }
                    break;
                case ("HealthRingButton"):
                    itemCost = 115;
                    itemName = "HealthRingButton";
                    if (boughtItem == "HealthRingButton")
                    {
                        //Cant do yet
                        healthRing.SetActive(false);

                    }
                    break;
            }
        }
        else
        {
            disableParticles();
        }

        if(boughtItem != "")
        {
            boughtItem = "";
            resetVariables();
        }

        if (playerGold < itemCost)
        {
            buyButtonObj.gameObject.SetActive(false);
        }
        else
        {
            buyButtonObj.gameObject.SetActive(true);
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
            buyButtonObj.gameObject.SetActive(true);
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
        buyButton.gameObject.SetActive(true);
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
        if (!sellActive)
        {
            if (playerGold >= itemCost)
            {
                playerGold = playerGold - itemCost;
                boughtItem = itemName;
                buttonSelected = true;
            }
            else
            {
                resetVariables();
            }
            disableParticles();
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
