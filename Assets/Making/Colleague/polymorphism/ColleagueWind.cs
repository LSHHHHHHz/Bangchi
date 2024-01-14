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
    public override void Update()
    {
        base.Update();
        ColleagueStatsNameText[0].text = "공격속도";
        ColleagueStatsNameText[1].text = "이동속도";
        ColleagueStatsNameText[2].text = "Pet 코인";

        UpdateText();
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