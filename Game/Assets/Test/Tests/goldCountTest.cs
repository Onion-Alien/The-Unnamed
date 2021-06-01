using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
   
    
    [UnityTest]
    public IEnumerator goldCountCheck()
    {

        var gameObject = new GameObject();
       var goldCount = gameObject.AddComponent<GoldCount>();

        goldCount.increaseGold(2);

        Assert.AreEqual(5, goldCount.increaseGold(2));

        
        yield return null;

    }
}
