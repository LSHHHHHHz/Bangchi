using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using Assets.Making.scripts;

public class Ability : MonoBehaviour
{
    public float recoveryRate = 1; // 1�ʸ��� ȸ���ǵ��� ����

    public RectTransform uiGroup;
    public RectTransform[] BuyButton;
    public GameObject prefab;
    public Text talkText;
    public string[] talkData;
    public int[] ablityPrice; //�����Ƽ ���� ����
    public GameObject[] itemObj; // ��,ü��, ȸ���� ���

    public Text[] abilityPriceText;

    public Player enterPlayer;

    bool isButtonPress = false;
    public Ability()
    {
        ablityPrice = new int[5]; // ���̰� 5�� �迭�� ����

    }
    private void Start()
    {
        // InvokeRepeating("Recovery", 0f, 1f);
        StartCoroutine(RecoveryRoutine());
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
        UIManager.instance.OnBottomButtonClicked();
        uiGroup.gameObject.SetActive(true);
        uiGroup.anchoredPosition = new Vector3(283.8f, 231.5f, 0f);
    }

    // Update is called once per frame
    public void ExitAblity()
    {
        uiGroup.gameObject.SetActive(false);
        uiGroup.anchoredPosition = new Vector3(550, -700, 0);
    }


    private void PlayPrefabEffect(Vector3 position)
    {
        // ���� ������Ʈ �ν��Ͻ� ����
        GameObject effectInstance = Instantiate(prefab, position, Quaternion.identity);

        // ���⼭ �߰����� ������ �ʿ��ϸ� �ۼ�
    }

    public void OnbuttonPressDown(int index)
    {
        isButtonPress = true;
        StartCoroutine(repeatedBuy(index));
    }
    public void OnbuttonPressUP(int index)
    {
        isButtonPress = false;
    }

    private IEnumerator repeatedBuy(int index)
    {
        while (isButtonPress)
        {
            Buy(index);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Buy(int index) //index�� � �������� Ȯ����
    { 
        //RectTransform buyButton = BuyButton.GetComponent<RectTransform>();
        RectTransform buyButton = BuyButton[index];

        // DoTween Sequence�� ����ϴ�.
        Sequence sequence = DOTween.Sequence();

        sequence.Append(buyButton.transform.DOScale(1.2f, 0.1f)); // 1.2�� ũ��� 0.1�� ���� Ȯ��
        sequence.Append(buyButton.transform.DOScale(1f, 0.1f)); // ���� ũ��� 0.1�� ���� ���


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
            enterPlayer.Max_HP += enterPlayer.HPLevel;
            enterPlayer.HPLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text�� ���� ���� �����ֱ�
        
        }
        else if (index == 2)
        {
            enterPlayer.Coin -= price;
            enterPlayer.RecoveryHP += enterPlayer.RecoveryLevel;
            enterPlayer.RecoveryLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text�� ���� ���� �����ֱ�

        }
        else if (index == 3)
        {
            enterPlayer.Coin -= price;
            enterPlayer.Current_CriticalDamage += enterPlayer.CriticalDamageLevel;
            enterPlayer.CriticalDamageLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text�� ���� ���� �����ֱ�

        }
        else if (index == 4)
        {
            enterPlayer.Coin -= price;
            enterPlayer.Current_Criticalprobability += 0.1f;
            enterPlayer.CriticalprobabilityLevel += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();// UI Text�� ���� ���� �����ֱ�

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
            enterPlayer.Current_Attack *= enterPlayer.Current_CriticalDamage; // ġ��Ÿ �߻� �� �������� 2��� ����
            Console.WriteLine("Critical hit!");
        }
    }

    private bool IsCriticalHit()
    {
        System.Random random = new System.Random();
        int roll = random.Next(1, 100); // 1~100 ������ ������ ���� ����
        return roll <= enterPlayer.Current_Criticalprobability; // ������ ���� ġ��Ÿ Ȯ������ �۰ų� ������ ġ��Ÿ �߻�
    }


    //ȸ���ϴ°� 
    /* public void Recovery()
     {
         enterPlayer.currentDotTime -= Time.deltaTime;
         if(enterPlayer.Current_HP < enterPlayer.Max_HP)
         {
             if(enterPlayer.currentDotTime<=0)
             {
                 enterPlayer.Current_HP += enterPlayer.RecoveryHP;
                 if(enterPlayer.Current_HP > enterPlayer.Max_HP)
                 {
                     enterPlayer.Current_HP = enterPlayer.Max_HP;

                 }
                 enterPlayer.currentDotTime = 1;
             }
         }
     }*/

    private IEnumerator RecoveryRoutine()
        {
        while (true)
        {
            yield return new WaitForSeconds(recoveryRate);

            // HP ȸ��
            if (enterPlayer.Current_HP < enterPlayer.Max_HP)
            {
                enterPlayer.Current_HP += enterPlayer.RecoveryHP;
                if (enterPlayer.Current_HP > enterPlayer.Max_HP)
                {
                    enterPlayer.Current_HP = enterPlayer.Max_HP;
                }
            }

            // MP ȸ��
            if (enterPlayer.Current_MP < enterPlayer.Max_MP)
            {
                enterPlayer.Current_MP += enterPlayer.RecoveryMP;
                if (enterPlayer.Current_MP > enterPlayer.Max_MP)
                {
                    enterPlayer.Current_MP = enterPlayer.Max_MP;
                }
            }
        }
    }
}
