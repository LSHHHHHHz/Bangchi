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
    public ColleagueType colleagueType;

    public int First_stat;
    public int First_stat_LV;
    public int Second_stat;
    public int Second_stat_LV;
    public int Third_stat;
    public int Third_stat_LV;


    public abstract void Update();

    public abstract void ColleagueStatusBuy(int index);
   

    
}