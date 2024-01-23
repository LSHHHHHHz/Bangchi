using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using Assets.Making.scripts;
using UnityEngine.Rendering.Universal;

public class CoinStatAbility : MonoBehaviour
{
    public float recoveryRate = 1; // 1초마다 회복되도록 설정

    public RectTransform uiGroup;
    public RectTransform[] BuyButton; //구매 버튼
    public GameObject prefab;
    public Text talkText;
    public string[] talkData;
    public int[] ablityPrice; //어빌리티 구매 가격
    public Text[] abilityPriceText;
    public GameObject[] itemObj; // 힘,체력, 회복력 등등

    public Player enterPlayer;

    bool isButtonPress = false;

    public int coinAttack;
    public int coinAttackLV;
    public int coinHP;
    public int coinHPLV;
    public int coinRecoveryHP;
    public int coinRecoveryHPLV;
    public int coinCriticalDamage;
    public int coinCriticalDamageLV;
    public float coinCriticalprobability;
    public int coinCriticalprobabilityLV;

    public Text _Attack;
    public Text _AttackLevel;
    public Text _HP;
    public Text _HPLevel;
    public Text _Recovery;
    public Text _RecoveryLevel;
    public Text _CriticalDamage;
    public Text _CriticalDamageLevel;
    public Text _Criticalprobability;
    public Text _CriticalprobabilityLevel;

    public CoinStatAbility()
    {
        ablityPrice = new int[5]; // 길이가 5인 배열을 생성

    }
    private void Awake()
    {
        statDataLoad();
    }
    private void Start()
    {
    }
    void Update()
    {

        abilityPriceText[0].text = ablityPrice[0].ToString();
        abilityPriceText[1].text = ablityPrice[1].ToString();
        abilityPriceText[2].text = ablityPrice[2].ToString();
        abilityPriceText[3].text = ablityPrice[3].ToString();
        abilityPriceText[4].text = ablityPrice[4].ToString();
        ablityUpdate();
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

    public void Buy(int index) 
    { 
        RectTransform buyButton = BuyButton[index];

        Sequence sequence = DOTween.Sequence();

        sequence.Append(buyButton.transform.DOScale(1.2f, 0.1f)); // 1.2배 크기로 0.1초 동안 확장
        sequence.Append(buyButton.transform.DOScale(1f, 0.1f)); // 원래 크기로 0.1초 동안 축소


        int price = ablityPrice[index];
        if (price > enterPlayer.Coin)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }
        if (index == 0)
        {
            int originalAttack = coinAttack;
            enterPlayer.Coin -= price;
            coinAttack += coinAttackLV;
            coinAttackLV += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();
            enterPlayer.Current_Attack += (coinAttack - originalAttack);

        }
        else if (index == 1)
        {
            int originalHP = coinHP;
            enterPlayer.Coin -= price;
            coinHP += coinHPLV;
            coinHPLV += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();
            enterPlayer.Current_HP += (coinHP - originalHP);
        }
        else if (index == 2)
        {
            int originalRecoveryHP = coinRecoveryHP;
            enterPlayer.Coin -= price;
            coinRecoveryHP += coinRecoveryHPLV;
            coinRecoveryHPLV += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();
            enterPlayer.RecoveryHP += (coinRecoveryHP -originalRecoveryHP);
        }
        else if (index == 3)
        {
            int originalCriticalDamage = coinCriticalDamage;
            enterPlayer.Coin -= price;
            coinCriticalDamage += coinCriticalDamageLV;
            coinCriticalDamageLV += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();
            enterPlayer.Current_CriticalDamage += (coinCriticalDamage - originalCriticalDamage) ;
        }
        else if (index == 4)
        {
            int originalCriticalProbability = (int)coinCriticalprobability;
            enterPlayer.Coin -= price;
            coinCriticalprobability += 0.1f;
            coinCriticalprobabilityLV += 1;
            ablityPrice[index] += 100;
            abilityPriceText[index].text = ablityPrice[index].ToString();
            enterPlayer.Current_Criticalprobability += (coinCriticalprobability - originalCriticalProbability);
        }
        statDataSave();
    }
    void ablityUpdate()
    {
        _Attack.text = coinAttack + " → " + (coinAttack + coinAttackLV);
        _AttackLevel.text = "LV" + coinAttackLV;

        _HP.text = coinHP + " → " + (coinHP + coinHPLV);
        _HPLevel.text = "LV" + coinHPLV;

        _Recovery.text = coinRecoveryHP + " → " + (coinRecoveryHP + coinRecoveryHPLV);
        _RecoveryLevel.text = "LV" + coinRecoveryHPLV;

        _CriticalDamage.text = $"{coinCriticalDamage:F1} → {(0.1f + coinCriticalDamage):F1}";
        _CriticalDamageLevel.text = "LV" + coinCriticalDamageLV;

        _Criticalprobability.text = coinCriticalprobability.ToString("F1")+"%" + " → " + (coinCriticalprobability + 0.1f).ToString("F1")+ "%";
        _CriticalprobabilityLevel.text = "LV" + coinCriticalprobabilityLV;

       
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
    public void statDataSave()
    {
        PlayerPrefs.SetInt(nameof(coinAttack), coinAttack);
        PlayerPrefs.SetInt(nameof(coinAttackLV), coinAttackLV);

        PlayerPrefs.SetInt(nameof(coinHP), coinHP);
        PlayerPrefs.SetInt(nameof(coinHPLV), coinHPLV);

        PlayerPrefs.SetInt(nameof(coinRecoveryHP), coinRecoveryHP);
        PlayerPrefs.SetInt(nameof(coinRecoveryHPLV), coinRecoveryHPLV);

        PlayerPrefs.SetInt(nameof(coinCriticalDamage), coinCriticalDamage);
        PlayerPrefs.SetInt(nameof(coinCriticalDamageLV), coinCriticalDamageLV);

        PlayerPrefs.SetFloat(nameof(coinCriticalprobability), coinCriticalprobability);
        PlayerPrefs.SetInt(nameof(coinCriticalprobabilityLV), coinCriticalprobabilityLV);

        int abliltyCoinLengh = ablityPrice.Length;
        PlayerPrefs.SetInt("abliltyCoinLengh", abliltyCoinLengh);
        for (int i = 0; i < abliltyCoinLengh; i++)
        {
            string key = "abliltyCoin_" + i;
            int value = ablityPrice[i];
            PlayerPrefs.SetInt(key, value);
        }

        PlayerPrefs.Save();
    }
    public void statDataLoad()
    {
        coinAttack = PlayerPrefs.GetInt(nameof(coinAttack), 1);
        coinAttackLV = PlayerPrefs.GetInt(nameof(coinAttackLV), 1);

        coinHP = PlayerPrefs.GetInt(nameof(coinHP), 1);
        coinHPLV = PlayerPrefs.GetInt(nameof(coinHPLV), 1);

        coinRecoveryHP = PlayerPrefs.GetInt(nameof(coinRecoveryHP), 1);
        coinRecoveryHPLV = PlayerPrefs.GetInt(nameof(coinRecoveryHPLV), 1);

        coinCriticalDamage = PlayerPrefs.GetInt(nameof(coinCriticalDamage), 1);
        coinCriticalDamageLV = PlayerPrefs.GetInt(nameof(coinCriticalDamageLV), 1);

        coinCriticalprobability = PlayerPrefs.GetFloat(nameof(coinCriticalprobability), 1);
        coinCriticalprobabilityLV = PlayerPrefs.GetInt(nameof(coinCriticalprobabilityLV), 1);

        if (PlayerPrefs.HasKey("abliltyCoinLengh"))
        {
            int abliltyCoinLengh = PlayerPrefs.GetInt(nameof(abliltyCoinLengh), 0);
            ablityPrice = new int[abliltyCoinLengh];
            for (int i = 0; i < abliltyCoinLengh; i++)
            {
                string key = "abliltyCoin_" + i;
                int value = PlayerPrefs.GetInt(key, 0);
                ablityPrice[i] = value;
            }
        }
    }
}
