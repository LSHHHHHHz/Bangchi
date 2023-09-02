using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ColleagueFire : ColleaguePoly
{

    public override void Update()
    {
        base.Update();
        ColleagueStatsNameText[0].text = "추가 공격력";  
        ColleagueStatsNameText[1].text = "HP";
        ColleagueStatsNameText[2].text = "HP 회복";
    }

    public override int GetCoin()
    {
        return Player.instance.ColleageCoinFire;
    }

    public override void SetCoin(int coin)
    {
        Player.instance.ColleageCoinFire = coin;
    }
}
