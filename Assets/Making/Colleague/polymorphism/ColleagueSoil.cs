using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ColleagueSoil : ColleaguePoly
{
    public override void Update()
    {
        base.Update();
        ColleagueStatsNameText[0].text = "크리티컬 데미지";
        ColleagueStatsNameText[1].text = "전체 공격력 추가";
        ColleagueStatsNameText[2].text = "추가 Coin";

        UpdateText();
    }

    public override int GetCoin()
    {
        return Player.instance.ColleageCoinSoil;
    }
    public override void SetCoin(int coin)
    {
        Player.instance.ColleageCoinSoil = coin;

    }
}
