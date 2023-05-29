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
    public int[] ablityPrice;
    public GameObject[] itemObj; // ��,ü��, ȸ���� ���

    public Text[] abilityPriceText;

    public Player enterPlayer;


    private void Start()
    {
        InvokeRepeating("Recovery", 0f, 1f);
    }
    void Update()
    {
        abilityPriceText[0].text = ablityPrice[0].ToString();
        abilityPriceText[1].text = ablityPrice[1].ToString();
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

    


    public void Buy(int index) //index�� � �������� Ȯ����
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
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text�� ���� ���� �����ֱ�

        }
        else if (index == 1)
        {
            enterPlayer.Coin -= price;
            enterPlayer.Current_HP += enterPlayer.HPLevel;
            enterPlayer.HPLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text�� ���� ���� �����ֱ�
        
        }
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
            enterPlayer.Attack *= enterPlayer.Critical_Damage; // ġ��Ÿ �߻� �� �������� 2��� ����
            Console.WriteLine("Critical hit!");
        }
    }

    private bool IsCriticalHit()
    {
        System.Random random = new System.Random();
        int roll = random.Next(1, 101); // 1~100 ������ ������ ���� ����
        return roll <= enterPlayer.Critical_value; // ������ ���� ġ��Ÿ Ȯ������ �۰ų� ������ ġ��Ÿ �߻�
    }

    public void Recovery()
    {
        enterPlayer.currentDotTime -= Time.deltaTime;
        if(enterPlayer.HP<enterPlayer.MaxHP)
        {
            if(enterPlayer.currentDotTime<=0)
            {
                enterPlayer.HP += enterPlayer.RecoveryHP;
                if(enterPlayer.HP > enterPlayer.MaxHP)
                {
                    enterPlayer.HP = enterPlayer.MaxHP;
                    if (enterPlayer.currentDotTime <= -1f)
                    {
                        enterPlayer.currentDotTime = enterPlayer.dotTime;
                    }
                }
                
            }
        }


    }
}
