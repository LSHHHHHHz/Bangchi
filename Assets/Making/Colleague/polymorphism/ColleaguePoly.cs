using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public abstract class ColleaguePoly : MonoBehaviour
{
    public int[] ColleagueStatsPrice;
    public Text[] ColleagueStatsPriceText;
    public Text[] ColleagueStatsLVText;
    public Text[] ColleagueStatsNameText;
    public Text[] ColleagueStatsInfoNameText;


    public ColleagueType colleagueType;

    public int First_stat;
    public int First_stat_LV;
    public int Second_stat;
    public int Second_stat_LV;
    public int Third_stat;
    public int Third_stat_LV;


    public virtual void Update()
    {
        ColleagueStatsPriceText[0].text = ColleagueStatsPrice[0].ToString();
        ColleagueStatsPriceText[1].text = ColleagueStatsPrice[1].ToString();
        ColleagueStatsPriceText[2].text = ColleagueStatsPrice[2].ToString();

        ColleagueStatsLVText[0].text = "LV" + First_stat_LV.ToString();
        ColleagueStatsLVText[1].text = "LV" + Second_stat_LV.ToString();
        ColleagueStatsLVText[2].text = "LV" + Third_stat_LV.ToString();

        ColleagueStatsInfoNameText[0].text = ColleagueStatsNameText[0].text + "+" + First_stat.ToString();
        ColleagueStatsInfoNameText[1].text = ColleagueStatsNameText[1].text + "+" + Second_stat.ToString();
        ColleagueStatsInfoNameText[2].text = ColleagueStatsNameText[2].text + "+" + Third_stat.ToString();

    }
    public abstract void ColleagueStatusBuy(int index);
   

    
}