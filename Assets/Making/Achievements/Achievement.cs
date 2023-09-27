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
    public void Awake()
    {
        instance = this;
        isQuestCompleted = new bool[DailyFillAmountImageBar.Length];
        isGainDieAmond = new bool[DailyButton.Length];
        originColor();
        ButtonActiveFalse();
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
        AchiveButton();
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
        DailyFillAmountImageBar[0].fillAmount = elapsedTime / 180;
        int secondsElapsed = Mathf.RoundToInt(elapsedTime);
        DailyFillAmountText[0].text = secondsElapsed.ToString() + " / 180";
        //DailyFillAmountText[1].text = secondsElapsed.ToString() + " / 180";

        DailyFillAmountImageBar[1].fillAmount = ItemGachaCount / 30f;
        DailyFillAmountText[1].text = ItemGachaCount.ToString() + " / 30";
        //DailyFillAmountText[3].text = ItemGachaCount.ToString() + " / 30";

        DailyFillAmountImageBar[2].fillAmount = FusionCount / 30f;
        DailyFillAmountText[2].text = FusionCount.ToString() + " / 30";
        //DailyFillAmountText[5].text = FusionCount.ToString() + " / 30";

        DailyFillAmountImageBar[3].fillAmount = MonsterKilledCount / 180f;
        DailyFillAmountText[3].text = MonsterKilledCount.ToString() + " / 180";
        //DailyFillAmountText[7].text = MonsterKilledCount.ToString() + " / 180";

        DailyFillAmountImageBar[4].fillAmount = AchievementCount / 4f;
        DailyFillAmountText[4].text = AchievementCount.ToString() + " / 4";
        //DailyFillAmountText[9].text = AchievementCount.ToString() + " / 4";
    }

    void ButtonActiveFalse() //������ �� ��Ȱ��ȭ -> 00�ÿ� ��Ȱ��ȭ ���Ѿ���
    {
        for (int i = 0; i < DailyButton.Length; i++)
        {
            DailyButton[i].enabled = false;
        }
    }
    public void AchiveButton()
    {
        for (int i = 0; i < DailyFillAmountImageBar.Length; i++)
        {
            if (!isQuestCompleted[i] && DailyFillAmountImageBar[i].fillAmount >= 1 && !isGainDieAmond[i])
            {
                AchievementCount += 1;
                DailyButton[i].enabled = true;
                isQuestCompleted[i] = true;
            }
        }
    }
    public void AchiveButtonClick(int index)
    {
        Player.instance.Diemond += 500;
        DailyButton[index].enabled = false;
        isQuestCompleted[index] = false;
        isGainDieAmond[index] = true;

    }
    void originColor()
    {
        // �̹����� ���� ������ ������ �迭�� �ʱ�ȭ�մϴ�.
        originalMainImageColors = new Color[colorChange.Length];

        // ��� colorChange �̹����� ��ȸ�մϴ�.
        for (int i = 0; i < colorChange.Length; i++)
        {
            // �� ���� �̹����� ���� ������ �����մϴ�.
            originalMainImageColors[i] = colorChange[i].color;

            // ���� ���� �̹����� ��� �ڽ� �̹����� �����ɴϴ�.
            Image[] childImages = colorChange[i].GetComponentsInChildren<Image>();

            // �� �ڽ� �̹����� ������ ������ �迭�� �ʱ�ȭ�մϴ�.
            Color[] childColors = new Color[childImages.Length];

            // ��� �ڽ� �̹����� ��ȸ�մϴ�.
            for (int j = 0; j < childImages.Length; j++)
            {
                // �� �ڽ� �̹����� ���� ������ �����մϴ�.
                childColors[j] = childImages[j].color;
            }

            // �ڽ� �̹����� ���� �迭�� ����Ʈ�� �߰��մϴ�.
            originalChildImageColors.Add(childColors);
        }
    }

    void ChangeColor()
    {
        // ��� colorChange �̹����� ��ȸ�մϴ�.
        for (int i = 0; i < colorChange.Length; i++)
        {
            // ���� ���� �̹����� �ڽ� �̹����� �����ɴϴ�.
            Image mainImage = colorChange[i];
            Image[] childImages = mainImage.GetComponentsInChildren<Image>();

            // ����Ʈ�� �Ϸ���� �ʾҴٸ�
            if (!isQuestCompleted[i])
            {
                // ���� �̹����� �ڽ� �̹����� ������ �����մϴ�.
                mainImage.color = new Color(0.4f, 0.4f, 0.4f);
                foreach (Image childImage in childImages)
                {
                    childImage.color = new Color(0.4f, 0.4f, 0.4f);
                }
            }
            else // ����Ʈ�� �Ϸ�Ǿ��ٸ�
            {
                // ���� �̹����� �ڽ� �̹����� ������ ���� �������� �ǵ����ϴ�.
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