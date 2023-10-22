using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



public class RefTest : MonoBehaviour
{
    private TestScript _test;

    private void Start()
    {
        _test = new TestScript(10, 20);

        var type = _test.GetType();

        //MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

        MethodInfo prinABCMethod = type.GetMethod("PrintABC");
        FieldInfo field = type.GetField("def");

        field.SetValue(_test,100);
        TestScript b = new TestScript(15, 25);
        if(prinABCMethod != null)
        {
            prinABCMethod.Invoke(_test, null);
            prinABCMethod.Invoke(b, null);
        }
        //foreach (MethodInfo method in methods)
        //{
        //    Debug.Log(method.Name);
        //}
    }
}
