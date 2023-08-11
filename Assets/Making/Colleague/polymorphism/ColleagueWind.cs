﻿using Assets.HeroEditor.Common.Scripts.Common;
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
    

    public override void Update()
    {
        base.Update();
        ColleagueStatsNameText[0].text = "공격속도";
        ColleagueStatsNameText[1].text = "이동속도";
        ColleagueStatsNameText[2].text = "Pet 코인";

    }
    public override void ColleagueStatusBuy(int index)
    {
        int price = ColleagueStatsPrice[index];
        if (Player.instance.ColleageCoinWater > price)
        {
            switch (index)
            {
                case 0:
                    First_stat_LV += 1;
                    First_stat += First_stat_LV;
                    break;
                case 1:
                    Second_stat_LV += 1;
                    Second_stat += Second_stat_LV;
                    break;
                case 2:
                    Third_stat_LV += 1;
                    Third_stat += Third_stat_LV;
                    break;
            }
            ColleagueStatsPrice[index] += 100 * (index + 1);
            ColleagueStatsPriceText[index].text = ColleagueStatsPrice[index].ToString();
        }
    }
}