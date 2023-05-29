using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateExemple1 : MonoBehaviour
{
    //��������Ʈ�� �Լ��� �ѱ����� ����־ ������ �� ����
    public delegate void ChanFunction(int value);
    ChanFunction chain;

    int power;
    int defence;

    public void SetPower(int value)
    {
        power += value;

        print("power�� ����" + value + "��ŭ �����߽��ϴ�. �� power�� �� = " + power);
    }
    public void SetDefence(int value)
    {
        defence += value;

        print("defence ����" + value + "��ŭ �����߽��ϴ�. �� defence�� �� = " + defence);
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