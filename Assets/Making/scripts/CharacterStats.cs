using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterStats : MonoBehaviour
{
    //public Player player;
    [SerializeField]
    private Player player;

    public Text _Attack;
    public Text _HP;
    public Text _HPRecovery;
    public Text _CriticalDamage;
    public Text _Criticalprobability;
    public Text _MP;
    public Text _MPRecovery;
    public Text _Coin;
    public Text _Exp;

    public RectTransform characterUI;
    public RectTransform characterUIClose;
    void Start()
    {
    }
    void Update()
    {
        Text_Stats();
    }

    void Text_Stats()
    {
        _Attack.text = player.Current_Attack + ""; //이방법 말고 다른방법있는지
        _HP.text = player.Max_HP + "";
        _HPRecovery.text = player.RecoveryHP + "";
        _CriticalDamage.text = player.Current_CriticalDamage + "";
        _Criticalprobability.text = player.Current_Criticalprobability + "";
        _MP.text = player.Max_MP + "";
        _MPRecovery.text = player.RecoveryMP + "";

    
        _Coin.text = player.Coin + "";
    }

    public void characterOpen()
    {
        characterUI.localPosition = new Vector3(310, 455, 0);
    }

    public void characterClose()
    {
        characterUI.localPosition = new Vector3(-782, 455, 0);
    }

}

