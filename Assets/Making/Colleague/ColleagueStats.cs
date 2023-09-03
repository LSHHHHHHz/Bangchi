using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ColleagueStats : MonoBehaviour
{
    public int[][] ColleagueStatsPrice;
    public Text[][] ColleagueStatsPriceText;
    public Text[][] ColleagueStatsLVText;
    public ColleagueType colleagueType;

    public int[] First_stat;
    public int[] First_stat_LV;
    public int[] Second_stat;
    public int[] Second_stat_LV;
    public int[] Third_stat;
    public int[] Third_stat_LV;

    public void Update()
    {
        int typeIndex = (int)colleagueType;

        ColleagueStatsPriceText[typeIndex][0].text = ColleagueStatsPrice[0].ToString();
        ColleagueStatsPriceText[typeIndex][1].text = ColleagueStatsPrice[1].ToString();
        ColleagueStatsPriceText[typeIndex][2].text = ColleagueStatsPrice[2].ToString();

        ColleagueStatsLVText[typeIndex][0].text = First_stat_LV[typeIndex].ToString();
        ColleagueStatsLVText[typeIndex][1].text = Second_stat_LV[typeIndex].ToString();
        ColleagueStatsLVText[typeIndex][2].text = Third_stat_LV[typeIndex].ToString();

    }
    public void ColleagueStatusBuy(int statButtonIndex)
    {
        int typeIndex = (int)colleagueType;
        int price = ColleagueStatsPrice[typeIndex][statButtonIndex];

        if (Player.instance.ColleageCoinWater > price)
        {
            switch (statButtonIndex)
            {
                case 0:
                    First_stat_LV[typeIndex] += 1;
                    First_stat[typeIndex] += First_stat_LV[typeIndex];
                    break;
                case 1:
                    Second_stat_LV[typeIndex] += 1;
                    Second_stat[typeIndex] += Second_stat_LV[typeIndex];
                    break;
                case 2:
                    Third_stat_LV[typeIndex] += 1;
                    Third_stat[typeIndex] += Third_stat_LV[typeIndex];
                    break;
            }
            ColleagueStatsPrice[typeIndex][statButtonIndex] += 100 * (statButtonIndex + 1);
            ColleagueStatsPriceText[typeIndex][statButtonIndex].text = ColleagueStatsPrice[typeIndex][statButtonIndex].ToString();
        }

        else
        {
            return;
        }
    }
}