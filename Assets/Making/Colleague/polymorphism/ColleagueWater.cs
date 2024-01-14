using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ColleagueWater : ColleaguePoly
{
    public override void Update()
    {
        base.Update();
        ColleagueStatsNameText[0].text = "MP";
        ColleagueStatsNameText[1].text = "MP 회복";
        ColleagueStatsNameText[2].text = "추가 경험치";

        UpdateText();
    }

    public override int GetCoin()
    {
        return Player.instance.ColleageCoinWater;
    }
    public override void SetCoin(int coin)
    {
        Player.instance.ColleageCoinWater = coin;
    }
}
