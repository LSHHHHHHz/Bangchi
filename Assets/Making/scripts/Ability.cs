using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public RectTransform uiGroup;
    public Text talkText;
    public string[] talkData;
    public int[] ablityPrice; //어빌리티 구매 가격
    public GameObject[] itemObj; // 힘,체력, 회복력 등등

    public Text[] abilityPriceText;

    public Player enterPlayer;

    public Ability()
    {
        ablityPrice = new int[5]; // 길이가 5인 배열을 생성

    }
    private void Start()
    {
        //InvokeRepeating("Recovery", 0f, 1f);
    }
    void Update()
    {
        abilityPriceText[0].text = ablityPrice[0].ToString();
        abilityPriceText[1].text = ablityPrice[1].ToString();
        abilityPriceText[2].text = ablityPrice[2].ToString();
        abilityPriceText[3].text = ablityPrice[3].ToString();
        abilityPriceText[4].text = ablityPrice[4].ToString();
    }
    public void EnterAblity() 
    {
        uiGroup.gameObject.SetActive(true);
        uiGroup.anchoredPosition = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    public void ExitAblity()
    {
        uiGroup.gameObject.SetActive(false);
        uiGroup.anchoredPosition = new Vector3(550, -700, 0);
    }

    


    public void Buy(int index) //index는 어떤 물건인지 확인함
    {
        int price = ablityPrice[index];
        if (price > enterPlayer.Coin)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }
        if (index == 0)
        {
            enterPlayer.Coin -= price;
            enterPlayer.Current_Attack += enterPlayer.AttackLevel;
            enterPlayer.AttackLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text에 가격 정보 보여주기

        }
        else if (index == 1)
        {
            enterPlayer.Coin -= price;
            enterPlayer.Current_HP += enterPlayer.HPLevel;
            enterPlayer.HPLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text에 가격 정보 보여주기
        
        }
        else if (index == 2)
        {
            enterPlayer.Coin -= price;
            enterPlayer.Current_Recovery += enterPlayer.RecoveryLevel;
            enterPlayer.RecoveryLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text에 가격 정보 보여주기

        }
        else if (index == 3)
        {
            enterPlayer.Coin -= price;
            enterPlayer.Current_CriticalDamage += 0.1f;
            enterPlayer.CriticalDamageLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text에 가격 정보 보여주기

        }
        else if (index == 4)
        {
            enterPlayer.Coin -= price;
            enterPlayer.Current_Criticalprobability += enterPlayer.CriticalprobabilityLevel;
            enterPlayer.CriticalprobabilityLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text에 가격 정보 보여주기

        }
        enterPlayer.statDataSave();
    }

    IEnumerator Talk()
    {

        talkText.text = talkData[0];
        yield return null;
    }


    public void Attack()
    {
        if (IsCriticalHit())
        {
            enterPlayer.Current_Attack *= enterPlayer.Current_CriticalDamage; // 치명타 발생 시 데미지를 2배로 적용
            Console.WriteLine("Critical hit!");
        }
    }

    private bool IsCriticalHit()
    {
        System.Random random = new System.Random();
        int roll = random.Next(1, 100); // 1~100 사이의 무작위 정수 생성
        return roll <= enterPlayer.Current_Criticalprobability; // 무작위 값이 치명타 확률보다 작거나 같으면 치명타 발생
    }


    //회복하는거 
    public void Recovery()
    {
        enterPlayer.currentDotTime -= Time.deltaTime;
        if(enterPlayer.Current_HP<enterPlayer.Max_HP)
        {
            if(enterPlayer.currentDotTime<=0)
            {
                enterPlayer.Current_HP += enterPlayer.RecoveryHP;
                if(enterPlayer.Current_HP > enterPlayer.Max_HP)
                {
                    enterPlayer.Current_HP = enterPlayer.Max_HP;
                    if (enterPlayer.currentDotTime <= -1f)
                    {
                        enterPlayer.currentDotTime = enterPlayer.dotTime;
                    }
                }
                
            }
        }


    }
}
