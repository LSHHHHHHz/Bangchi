using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ASKI : MonoBehaviour
{
    int a = 5;
    int b = 5;

    int sum;

    delegate void Mydelegate();
    Mydelegate mydelegate;

    private void Start()
    {
        mydelegate = Add;
        mydelegate += delegate () { print(sum); };
        mydelegate += () => print(sum); 
        mydelegate += Back;

        mydelegate();
    }



    void Add()
    {
        sum = a + b;
    }
    void Print()
    {
        print(sum);
    }

    void Back()
    {
        sum = 0;
    }
}