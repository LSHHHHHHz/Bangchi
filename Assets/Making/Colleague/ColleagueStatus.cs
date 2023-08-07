using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ColleagueStatus : MonoBehaviour
{
    public int[] ColleagueStatsPrice;
    public Text[] ColleagueStatsPriceText;
    public Text[] ColleagueStatsLVText;
    public ColleagueType colleagueType;

    //Water
    public int Water_MP;
    public int Water_MP_LV = 1;
    public int Water_MP_Recovery;
    public int Water_MP_Recovery_LV = 1;
    public int Water_PlusExp;
    public int Water_PlusExp_LV = 1;

    //Soil
    public int Soil_CriticalDamage;
    public int Soil_CriticalDamage_LV;
    public int Soil_FinalTotalDamage;
    public int Soil_FinalTotalDamage_LV;
    public int Soil_PlusCoin;
    public int Soil_PlusCoin_LV;

    //Wind
    public int Soil_AttackSpeed;
    public int Soil_AttackSpeed_LV;
    public int Soil_MoveSpeed;
    public int Soil_MoveSpeed_LV;
    public int Soil_PlusPetCoin;
    public int Soil_PlusPetCoin_LV;

    //Fire
    public int Soil_Attack;
    public int Soil_Attack_LV;
    public int Soil_HP;
    public int Soil_HP_LV;
    public int Soil_HP_Recovery;
    public int Soil_HP_Recovery_LV;

    public void Update()
    {
        ColleagueStatsPriceText[0].text = ColleagueStatsPrice[0].ToString();
        ColleagueStatsPriceText[1].text = ColleagueStatsPrice[1].ToString();
        ColleagueStatsPriceText[2].text = ColleagueStatsPrice[2].ToString();

        ColleagueStatsLVText[0].text = Water_MP_LV.ToString();
        ColleagueStatsLVText[1].text = Water_MP_Recovery_LV.ToString();
        ColleagueStatsLVText[2].text = Water_PlusExp_LV.ToString();
    }
    public void ColleagueStatusBuy(int index)
    {
        int price = ColleagueStatsPrice[index];

        if (Player.instance.ColleageCoinWater > price && colleagueType == ColleagueType.Water)
        {
            Player.instance.ColleageCoinWater -= price;
            if(index == 0)
            {
                Water_MP_LV += 1;
                Water_MP += Water_MP_LV;
                ColleagueStatsPrice[index] += 100;
                ColleagueStatsPriceText[index].text = ColleagueStatsPrice[index].ToString();
            }
            if (index == 1)
            {
                Water_MP_Recovery_LV += 1;
                Water_MP_Recovery += Water_MP_Recovery_LV;
                ColleagueStatsPrice[index] += 200;
                ColleagueStatsPriceText[index].text = ColleagueStatsPrice[index].ToString();
            }
            if (index == 2)
            {
                Water_PlusExp_LV += 1;
                Water_PlusExp += Water_PlusExp_LV;
                ColleagueStatsPrice[index] += 300;
                ColleagueStatsPriceText[index].text = ColleagueStatsPrice[index].ToString();
            }
        }
        else if(Player.instance.ColleageCoinSoil > price && colleagueType == ColleagueType.Soil)
        {
            Player.instance.ColleageCoinSoil -= price;
        }
        else if (Player.instance.ColleageCoinWind > price && colleagueType == ColleagueType.Wind)
        {
            Player.instance.ColleageCoinWind -= price;
        }
        else if (Player.instance.ColleageCoinFire > price && colleagueType == ColleagueType.Fire)
        {
            Player.instance.ColleageCoinFire -= price;
        }


        else
        {
            return;
        }


    }

}