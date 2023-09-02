using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateExemple1 : MonoBehaviour
{
    //델리게이트는 함수를 한군데에 집어넣어서 관리할 수 있음
    public delegate void ChanFunction(int value);
    ChanFunction chain;

    int power;
    int defence;

    public void SetPower(int value)
    {
        power += value;

        print("power의 값이" + value + "만큼 증가했습니다. 총 power의 값 = " + power);
    }
    public void SetDefence(int value)
    {
        defence += value;

        print("defence 값이" + value + "만큼 증가했습니다. 총 defence의 값 = " + defence);
    }

    void Start()
    {
        //SetPower(5)
        //SetDefence(5)
        chain += SetPower;
        chain += SetDefence;

        chain(5);
    }


    delegate int MyDelegate();

    public DelegateExemple1()
    {
        ShowMenu(GetAge_Korea);
    }
    void ShowMenu(MyDelegate Getage)
    {
        int age = Getage();
        if (age >= 20)
        {

        }       
    }

    public int GetAge_Korea()
    {
        return 0;
    }

    public int GetAge_Japan()
    {
        return 0;
    }
}