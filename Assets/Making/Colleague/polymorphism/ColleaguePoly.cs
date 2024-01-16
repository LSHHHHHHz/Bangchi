using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class ColleagueData
{
    public float First_stat;
    public int First_stat_LV;
    public float Second_stat;
    public int Second_stat_LV;
    public int Third_stat;
    public int Third_stat_LV;
    public int[] ColleagueStatsPrice;
}
public abstract class ColleaguePoly : MonoBehaviour
{
    private void Start()
    {
        Load();
        UpdateText();
    }

    private void OnApplicationQuit()
    {
        save();
    }
    public int[] ColleagueStatsPrice;
    public Text[] ColleagueStatsPriceText;
    public Text[] ColleagueStatsLVText;
    public Text[] ColleagueStatsNameText;
    public Text[] ColleagueStatsInfoNameText;

    public ColleagueType colleagueType;

    public float First_stat;
    public int First_stat_LV;
    public float Second_stat;
    public int Second_stat_LV;
    public int Third_stat;
    public int Third_stat_LV;
    public virtual void Update()
    {

    }
    protected void UpdateText()
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
    public void ColleagueStatusBuy(int index)
    {
        int price = ColleagueStatsPrice[index];
        if (GetCoin() > price)
        {
            switch (index)
            {
                case 0:
                    if(colleagueType == ColleagueType.Water)
                    {
                        float originstat = First_stat;
                        First_stat_LV += 1;
                        First_stat += First_stat_LV;
                        Player.instance.Max_MP += (First_stat - originstat);
                    }
                    if (colleagueType == ColleagueType.Soil)
                    {
                        float originstat = First_stat;
                        First_stat_LV += 1;
                        First_stat += First_stat_LV;
                        Player.instance.Current_CriticalDamage += (First_stat - originstat);
                    }
                    if (colleagueType == ColleagueType.Wind)
                    {
                        float originstat = First_stat;
                        First_stat += 0.01f;
                        First_stat_LV += 1;
                        Player.instance.AttackSpeed += (First_stat - originstat);
                    }
                    if (colleagueType == ColleagueType.Fire)
                    {
                        float originstat = First_stat;
                        First_stat_LV += 1;
                        First_stat += First_stat_LV;
                        Player.instance.Current_Attack += (First_stat - originstat);
                    }
                    break;
                case 1:
                    if (colleagueType == ColleagueType.Water)
                    {
                        float originstat = Second_stat;
                        Second_stat_LV += 1;
                        Second_stat += Second_stat_LV;
                        Player.instance.RecoveryMP += (Second_stat - originstat);
                    }
                    if (colleagueType == ColleagueType.Soil)
                    {
                        float originstat = Second_stat;
                        Second_stat_LV += 1;
                        Second_stat += Second_stat_LV;
                        Player.instance.Current_Attack += (Second_stat - originstat);
                    }
                    if (colleagueType == ColleagueType.Wind)
                    {
                        float originstat = Second_stat;
                        Second_stat_LV += 1;
                        Second_stat += 0.01f;
                        Player.instance.playerSpeed += (Second_stat - originstat);
                    }
                    if (colleagueType == ColleagueType.Fire)
                    {
                        float originstat = Second_stat;
                        Second_stat_LV += 1;
                        Second_stat += Second_stat_LV;
                        Player.instance.Current_HP += (Second_stat - originstat);
                    }
                    break;
                case 2:
                    if (colleagueType == ColleagueType.Water)
                    {
                        int originstat = Third_stat;
                        Third_stat_LV += 1;
                        Third_stat += Third_stat_LV;
                        Player.instance.AddExp += (Third_stat - originstat);
                    }
                    if (colleagueType == ColleagueType.Soil)
                    {
                        int originstat = Third_stat;
                        Third_stat_LV += 1;
                        Third_stat += Third_stat_LV;
                        Player.instance.AddCoin += (Third_stat - originstat);
                    }
                    if (colleagueType == ColleagueType.Wind)
                    {
                        int originstat = Third_stat;
                        Third_stat_LV += 1;
                        Third_stat += Third_stat_LV;
                        Player.instance.PetCoin += (Third_stat - originstat);
                    }
                    if (colleagueType == ColleagueType.Fire)
                    {
                        int originstat = Third_stat;
                        Third_stat_LV += 1;
                        Third_stat += Third_stat_LV;
                        Player.instance.RecoveryHP += (Third_stat - originstat);
                    }
                    break;
            }
            ColleagueStatsPrice[index] += 100 * (index + 1);
            UpdateText();
            SetCoin(GetCoin() - price);
            save();
        }
    }
    public abstract int GetCoin();
    public abstract void SetCoin(int coin);
    public void save()
    {
        var colleagueData = new ColleagueData();

        colleagueData.First_stat = this.First_stat;
        colleagueData.First_stat_LV = this.First_stat_LV;
        colleagueData.Second_stat = this.Second_stat;
        colleagueData.Second_stat_LV = this.Second_stat_LV;
        colleagueData.Third_stat_LV = this.Third_stat_LV;
        colleagueData.Third_stat = this.Third_stat;
        colleagueData.ColleagueStatsPrice = this.ColleagueStatsPrice;
        string json = JsonUtility.ToJson(colleagueData);

        PlayerPrefs.SetString("colleagueData" + colleagueType.ToString(), json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        string json = PlayerPrefs.GetString("colleagueData" + colleagueType.ToString());
        if (string.IsNullOrEmpty(json) == false)
        {
            var colleagueData = JsonUtility.FromJson<ColleagueData>(json);

            First_stat = colleagueData.First_stat;
            First_stat_LV = colleagueData.First_stat_LV;
            Second_stat = colleagueData.Second_stat;
            Second_stat_LV = colleagueData.Second_stat_LV;
            Third_stat = colleagueData.Third_stat;
            Third_stat_LV = colleagueData.Third_stat_LV;
            ColleagueStatsPrice = colleagueData.ColleagueStatsPrice;
        }

    }
}