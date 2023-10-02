using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public static Achievement instance;
    float elapsedTime = 0;
    private DateTime lastLoginDate;

    public RectTransform AchivementUIGroup;

    public Text[] DailyDieAmondText;
    public Image[] DailyFillAmountImageBar;
    public Text[] DailyFillAmountText;
    public Button[] DailyButton;
    public bool[] isQuestCompleted;
    public bool[] isGainDieAmond;

    public int ItemGachaCount;
    public int FusionCount;
    public int MonsterKilledCount;
    public int AchievementCount;

    public Image[] colorChange;
    private Color[] originalMainImageColors;
    private List<Color[]> originalChildImageColors = new List<Color[]>();

    public Text NowTime;
    public Text CheckTime;
    public string CurrentCheckTime;
    public bool isDateChange;
    public bool[] canClickButton = new bool[5]; // 조건 만족시 true
    public void Awake()
    {
        instance = this;
        isQuestCompleted = new bool[DailyFillAmountImageBar.Length];
        isGainDieAmond = new bool[DailyButton.Length];
        originColor();
    }
    private void Start()
    {

        if (lastLoginDate < DateTime.Today)
        {
            elapsedTime = 0;
        }

        for (int i = 0; i < DailyButton.Length; i++)
        {
            int buttonIndex = i; 
            DailyButton[i].onClick.AddListener(() => AchiveButtonClick(buttonIndex));
        }
    }
    void OnApplicationQuit()
    {
        SaveData(); 
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        FillAmountImages();
        isDailyMissionClear();
        ChangeColor();
        TimeCheck();

        if(isDateChange)
        {
            ResetData();
            SaveData();
        }
    }
    void TimeCheck()
    {
        string today = DateTime.Today.ToString();

        if (CurrentCheckTime != today)
        {
            CurrentCheckTime = today;
            isDateChange = true;
        }

        CheckTime.text = CurrentCheckTime;
        isDateChange = false;
    }
   

    public void FillAmountImages()
    {
        NowTime.text = DateTime.Now.ToString();
        DailyFillAmountImageBar[0].fillAmount = elapsedTime / 18;
        int secondsElapsed = Mathf.RoundToInt(elapsedTime);
        DailyFillAmountText[0].text = secondsElapsed.ToString() + " / 180";
        //DailyFillAmountText[1].text = secondsElapsed.ToString() + " / 180";

        DailyFillAmountImageBar[1].fillAmount = ItemGachaCount / 3f;
        DailyFillAmountText[1].text = ItemGachaCount.ToString() + " / 30";
        //DailyFillAmountText[3].text = ItemGachaCount.ToString() + " / 30";

        DailyFillAmountImageBar[2].fillAmount = FusionCount / 3f;
        DailyFillAmountText[2].text = FusionCount.ToString() + " / 30";
        //DailyFillAmountText[5].text = FusionCount.ToString() + " / 30";

        DailyFillAmountImageBar[3].fillAmount = MonsterKilledCount / 18f;
        DailyFillAmountText[3].text = MonsterKilledCount.ToString() + " / 180";
        //DailyFillAmountText[7].text = MonsterKilledCount.ToString() + " / 180";

        DailyFillAmountImageBar[4].fillAmount = AchievementCount / 4f;
        DailyFillAmountText[4].text = AchievementCount.ToString() + " / 4";
        //DailyFillAmountText[9].text = AchievementCount.ToString() + " / 4";
    }

    public void isDailyMissionClear()
    {
        for (int i = 0; i < DailyFillAmountImageBar.Length; i++)
        {
            if (DailyFillAmountImageBar[i].fillAmount >= 1 && !isGainDieAmond[i])
            {
                canClickButton[i] = true;
                if (!isQuestCompleted[i] && canClickButton[i] == true) // 퀘스트를 완료한 상태가 아니라면
                {
                    canClickButton[i] = false;
                    isQuestCompleted[i] = true; // 퀘스트 완료 상태로 변경
                    
                }
            }
        }
    }
    public void AchiveButtonClick(int index)
    {
        if (canClickButton[index] == true && !isGainDieAmond[index]) // 특정 인덱스만 확인
        {
            Player.instance.Diemond += 500;
            canClickButton[index] = false;
            isGainDieAmond[index] = true;
            AchievementCount += 1;
            if(index ==4)
            {
                AchievementCount -= 1;
            }
        }
    }
    void originColor()
    {
        originalMainImageColors = new Color[colorChange.Length];

        for (int i = 0; i < colorChange.Length; i++)
        {
            originalMainImageColors[i] = colorChange[i].color;

            Image[] childImages = colorChange[i].GetComponentsInChildren<Image>();

            Color[] childColors = new Color[childImages.Length];
            for (int j = 0; j < childImages.Length; j++)
            {
                childColors[j] = childImages[j].color;
            }

            originalChildImageColors.Add(childColors);
        }
    }

    void ChangeColor()
    {
        for (int i = 0; i < colorChange.Length; i++)
        {
            Image mainImage = colorChange[i];
            Image[] childImages = mainImage.GetComponentsInChildren<Image>();

            if (!canClickButton[i])
            {
                mainImage.color = new Color(0.4f, 0.4f, 0.4f);
                foreach (Image childImage in childImages)
                {
                    childImage.color = new Color(0.4f, 0.4f, 0.4f);
                }
            }
            else 
            {
                mainImage.color = originalMainImageColors[i];
                for (int j = 0; j < childImages.Length; j++)
                {
                    childImages[j].color = originalChildImageColors[i][j];
                }
            }
        }
    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey("LastLogin"))
        {
            lastLoginDate = DateTime.Parse(PlayerPrefs.GetString("LastLogin"));
            elapsedTime = PlayerPrefs.GetFloat("ElapsedTime");
            ItemGachaCount = PlayerPrefs.GetInt("ItemGachaCount");
            FusionCount= PlayerPrefs.GetInt("FusionCount");
            MonsterKilledCount= PlayerPrefs.GetInt("MonsterKilledCount");
            AchievementCount= PlayerPrefs.GetInt("AchievementCount");
        }
        else
        {
            lastLoginDate = DateTime.MinValue;
        }
    }
    void SaveData()
    {
        PlayerPrefs.SetString("LastLogin", DateTime.Now.ToString());
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime);
        PlayerPrefs.SetInt("ItemGachaCount", ItemGachaCount);
        PlayerPrefs.SetInt("FusionCount", FusionCount);
        PlayerPrefs.SetInt("MonsterKilledCount", MonsterKilledCount);
        PlayerPrefs.SetInt("AchievementCount", AchievementCount);

    }
    void ResetData()
    {
        elapsedTime = 0;
        ItemGachaCount = 0;
        FusionCount = 0;
        MonsterKilledCount = 0;
        AchievementCount = 0;
        for (int i = 0; i < canClickButton.Length; i++)
        {
            canClickButton[i] = false;
            isQuestCompleted[i] = false;
        }
    }
    public void AchiveMentUIOpen()
    {
        AchivementUIGroup.localPosition = new Vector3(0.0f, 130f, 0f);
    }
    public void AchiveMentUIClose()
    {
        AchivementUIGroup.localPosition = new Vector3(825f, 130f, 0f);
    }
}