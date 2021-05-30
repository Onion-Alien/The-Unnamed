using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class ShopPurchaseTest
{
    //This class tests the purchaseItem function of the Shop.
    [Test]
    public void TestCanBuy()
    {
        ShopTest.playerGold = 14;
        ShopTest.itemCost = 14;
        bool result = ShopTest.buyItem();

        Assert.IsTrue(result);
    }

    [Test]
    public void PowerupPickup()
    {
        ShopTest.playerGold = 14;
        ShopTest.itemCost = 14;
        bool result = ShopTest.buyItem();

        Assert.IsTrue(result);
    }
}
