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
    public int First_stat;
    public int First_stat_LV;
    public int Second_stat;
    public int Second_stat_LV;
    public int Third_stat;
    public int Third_stat_LV;
    public int[] ColleagueStatsPrice;
}
public abstract class ColleaguePoly : MonoBehaviour
{
    private string dataPath;
    private void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "colleagueData.json");
    }
    public void Save()
    {
        ColleagueData data = new ColleagueData
        {
            First_stat = this.First_stat,
            First_stat_LV = this.First_stat_LV,
            Second_stat = this.Second_stat,
            Second_stat_LV = this.Second_stat_LV,
            Third_stat = this.Third_stat,
            Third_stat_LV = this.Third_stat_LV,
            ColleagueStatsPrice = this.ColleagueStatsPrice
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(dataPath, json);
    }

    public void Load()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            ColleagueData data = JsonUtility.FromJson<ColleagueData>(json);

            First_stat = data.First_stat;
            First_stat_LV = data.First_stat_LV;
            Second_stat = data.Second_stat;
            Second_stat_LV = data.Second_stat_LV;
            Third_stat = data.Third_stat;
            Third_stat_LV = data.Third_stat_LV;
            ColleagueStatsPrice = data.ColleagueStatsPrice;
        }
    }

    private void Start()
    {
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
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