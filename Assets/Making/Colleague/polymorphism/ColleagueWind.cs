using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ColleagueWind : ColleaguePoly
{
    private void Start()
    {
        ColleagueStatsNameText[0].text = "공격속도";
        ColleagueStatsNameText[1].text = "이동속도";
        ColleagueStatsNameText[2].text = "Pet 코인";
    }
    private void Update()
    {
        UpdateText();
    }
    public override void ColleagueStatusBuy(int index)
    {
        int price = ColleagueStatsPrice[index];
        if (GetCoin() < price)
        {
            return;
        }
        switch (index)
        {
            case 0:
                float originstat_index1 = First_stat;
                First_stat_LV += 1;
                First_stat += First_stat_LV;
                Player.instance.AttackSpeed += (First_stat - originstat_index1);
                PostBuyProcess(index, price);
                break;
            case 1:
                float originstat_index2 = Second_stat;
                Second_stat_LV += 1;
                Second_stat += Second_stat_LV;
                Player.instance.playerSpeed += (Second_stat - originstat_index2);
                PostBuyProcess(index, price);
                break;
            case 2:
                int originstat_index3 = Third_stat;
                Third_stat_LV += 1;
                Third_stat += Third_stat_LV;
                Player.instance.PetCoin += (Third_stat - originstat_index3);
                PostBuyProcess(index, price);
                break;
        }
    }
    public override int GetCoin()
    {
        return Player.instance.ColleageCoinWind;
    }
    public override void SetCoin(int coin)
    {
        Player.instance.ColleageCoinWind = coin;
    }
}