using System.Collections;
using System.Collections.Generic;
using ScarFramework.Button;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    [ButtonAttribute("Test")]
    public void TestBtn()
    {
        Debug.Log("TEST!!!!");
    }
}
