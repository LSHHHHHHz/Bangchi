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
    public Text _Recovery;
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
        _HP.text = player.Current_HP + "";

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
