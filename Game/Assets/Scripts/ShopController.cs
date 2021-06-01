using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    public ParticleSystem buttonGlow;
    public GameObject sellCanvas, craftCanvas;
    public static GameObject selectedButton;
    public GameObject staminaRing, healthRing;
    public GameObject purchaseTab, sellItemTab, craftTab, buyButtonObj,craftHP,craftStamina;
    public Text goldText, buyButton;
    private int playerGold, itemCost;
    private string itemName, boughtItem;
    private bool buttonSelected, sellActive, craftActive;
    public GameObject player;
    private Rigidbody2D playerRB;
    private Color originalButtonColor, modifiedColor;
    private PlayerCombat pc;

    // Start is called before the first frame update
    void Start()
    {
        craftActive = false;
        gameObject.SetActive(false);
        modifiedColor = Color.white;
        sellCanvas.gameObject.SetActive(false);
        craftCanvas.gameObject.SetActive(false);
        modifiedColor.a = 5f;
        playerRB = player.GetComponent<Rigidbody2D>();
        disableParticles();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();

        //originalButtonColor = buyButtonObj.colors.normalColor;
        //buttonGlow.enableEmission = false;
        //playerGold = SaveManager.instance.getPlayerGold();
        playerGold = 14;
    }
    void OnDisable()
    {
        buttonGlow.enableEmission = false;
        resetVariables();
        boughtItem = "";
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezePosition;
        goldText.text = "Your Gold: " + playerGold.ToString();
        string temp;
        if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null) {
             temp = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        }
        else
        {
            temp = "Purchase";
        }
        if(temp != "Return" && temp != "Purchase" && buttonSelected){
            buyButton.text = "Buy Item";
            selectedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            buttonGlow.enableEmission = true;
            buttonGlow.emissionRate = 5.0f;
            buttonGlow.Play();
        }

        if (selectedButton != null && (selectedButton.name != "Return" || selectedButton.name != "Purchase" || selectedButton.name != "purchaseTab" || selectedButton.name != "sellTab" || selectedButton.name != "Addgold" || selectedButton.name != "craftTab" ||
            selectedButton.name != "craftHP" || selectedButton.name != "craftStamina" || selectedButton.name != "noHPCraft" || selectedButton.name != "noStamCraft") && buttonSelected)
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
                        pc.dmgLight = +3;
                        pc.dmgHeavy = +3;
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
                       // PlayerController.movementSpeed += 0.5f;
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

        if (boughtItem != "")
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
            craftTab.transform.GetComponent<Image>().color = sellItemTab.transform.GetComponent<Image>().color;
            sellItemTab.transform.GetComponent<Image>().color = Color.white;
            sellCanvas.gameObject.SetActive(true);
            buyButton.text = "Sell Item";
            sellActive = true;

            resetVariables();
        }
    }
    public void switcCraftTab()
    {
        if (!craftActive)
        {


            craftTab.transform.GetComponent<Image>().color = Color.white;
            sellItemTab.transform.GetComponent<Image>().color = craftTab.transform.GetComponent<Image>().color;
            purchaseTab.transform.GetComponent<Image>().color = craftTab.transform.GetComponent<Image>().color;
            craftCanvas.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
            craftActive = true;
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

    public void craft()
    {

        if (FragmentCount.fc.redF1 >= 2 && FragmentCount.fc.redF2 >= 2)
        {
            FragmentCount.fc.redF1 -= 2;
            FragmentCount.fc.redF2 -= 2;
            FragmentCount.fc.redPotion++;


        }
        else if (FragmentCount.fc.greenF1 >= 2 && FragmentCount.fc.greenF2 >= 2)
        {

            FragmentCount.fc.greenF1 -= 2;
            FragmentCount.fc.greenF2 -= 2;
            FragmentCount.fc.greenPotion++;


        }
        else if (FragmentCount.fc.redF1 < 2 || FragmentCount.fc.redF2 < 2)
        {
            craftHP.gameObject.SetActive(false);
        }
        else if (FragmentCount.fc.greenF1 < 2 || FragmentCount.fc.greenF2 < 2)
        {
            craftStamina.gameObject.SetActive(false);
        }



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
