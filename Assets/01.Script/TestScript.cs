using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript 
{
    private int abc;
    public int def;

    public TestScript(int abc, int def)
    {
        this.abc = abc;
        this.def = def;
    }

    public void PrintABC()
    {
        Debug.Log(abc);
    }

    public void TestA()
    {
        Debug.Log("TestA");

    }
    public void TestB()
    {
        Debug.Log("TestB");
    }
    public void TestC()
    {
        Debug.Log("TestC");
    }
}
