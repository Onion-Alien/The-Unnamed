using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    // A Test behaves as an ordinary method
    [Test]
    public void canJumpTest()
    {
        var player = new GameObject().AddComponent<PlayerController>();
        player.isGrounded = true;
        player.amountOfJumps = 1;
        Assert.IsTrue(player.canJump);
    }

    [Test]
    public void HPTest()
    {

    }
}
