using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Item1;
using Unity.VisualScripting;
using System;
public class EXP_Cristal : MonoBehaviour
{
    public BossUI bossUI;
    public GameObject BackGround;

    public int Cristal_HP = 1000000;
    public int Current_Cristal_HP;

    public float duration = 10f;
    float elapsedTime = 0f;

    public Image Time_Bar;
    public Image HP_Bar;

    public void Awake()
    {
        BackGround.transform.position = new Vector3(2.44f, 0.16f, 0f);
    }
    private void Start()
    {
        Current_Cristal_HP = Cristal_HP;
    }
    private void Update()
    {
        if (bossUI.isTimerActive)
        {
            HP_Bar.fillAmount = (float)Current_Cristal_HP / Cristal_HP;
            if (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                Time_Bar.fillAmount = 1f - (elapsedTime / duration);
            }
            if(Time_Bar.fillAmount < 0.01 || Current_Cristal_HP<0)
            {
                bossUI.Exit_Exp_Boss();
            }

        }
        if(!bossUI.isTimerActive)
        {
            Current_Cristal_HP = Cristal_HP;
            elapsedTime = 0f;
           
        }
        
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Melee")
        {
            Weapons weapon = other.GetComponent<Weapons>();
            Current_Cristal_HP -= (int)weapon.Current_totalDamage;
            Debug.Log(Current_Cristal_HP);
        }
    }
}
