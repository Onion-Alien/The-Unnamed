using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopTest : MonoBehaviour
{
    public static GameObject purchaseTab, sellItemTab, buyButtonObj;
    public static Text goldText, buyButton;
    public static int playerGold, itemCost;
    private static string itemName, boughtItem;
    private static bool buttonSelected;

    // Start is called before the first frame update

    public static bool buyItem()
    {
        //Tons of code removed for unit testing purposes, for original method see ShopController.cs

            if (playerGold >= itemCost)
            {
                playerGold = playerGold - itemCost;
                boughtItem = itemName;
                buttonSelected = true;
                return true;
            }
            else
            {
            return false;
            }
        }
    }