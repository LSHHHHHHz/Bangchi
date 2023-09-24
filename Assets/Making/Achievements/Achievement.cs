using Assets.HeroEditor.Common.Scripts.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public static Achievement instance;
    float elapsedTime = 0;

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

    public void Awake()
    {
        instance = this;
        isQuestCompleted = new bool[DailyFillAmountImageBar.Length];
        isGainDieAmond = new bool[DailyButton.Length];
        originColor();
        ButtonActiveFalse();
    }

    private void Update()
    {
        FillAmountImages();
        AchiveButton();
        ChangeColor();
    }

    public void FillAmountImages()
    {
        elapsedTime += Time.deltaTime;
        DailyFillAmountImageBar[0].fillAmount = elapsedTime / 180;
        int secondsElapsed = Mathf.RoundToInt(elapsedTime);
        DailyFillAmountText[0].text = secondsElapsed.ToString() + " / 180";
        DailyFillAmountText[1].text = secondsElapsed.ToString() + " / 180";

        DailyFillAmountImageBar[1].fillAmount = ItemGachaCount / 30f;
        DailyFillAmountText[2].text = ItemGachaCount.ToString() + " / 30";
        DailyFillAmountText[3].text = ItemGachaCount.ToString() + " / 30";

        DailyFillAmountImageBar[2].fillAmount = FusionCount / 30f;
        DailyFillAmountText[4].text = FusionCount.ToString() + " / 30";
        DailyFillAmountText[5].text = FusionCount.ToString() + " / 30";

        DailyFillAmountImageBar[3].fillAmount = MonsterKilledCount / 180f;
        DailyFillAmountText[6].text = MonsterKilledCount.ToString() + " / 180";
        DailyFillAmountText[7].text = MonsterKilledCount.ToString() + " / 180";

        DailyFillAmountImageBar[4].fillAmount = AchievementCount / 4f;
        DailyFillAmountText[8].text = AchievementCount.ToString() + " / 4";
        DailyFillAmountText[9].text = AchievementCount.ToString() + " / 4";
    }

    void ButtonActiveFalse()
    {
        for(int i = 0; i < DailyButton.Length; i++)
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
                Debug.Log("Entering the condition for index: " + i);
                AchievementCount += 1;
                DailyButton[i].enabled = true;
                isQuestCompleted[i] = true;
            }
        }
    }
    public void AchiveButtonClick(int index)
    {
        for(int i= 0; i< DailyButton.Length; i++)
        {
            DailyButton[i].enabled = false;
            isQuestCompleted[i] = false;
            isGainDieAmond[i] = true;
        }
    }
    void originColor()
    {
        // 이미지의 원래 색상을 저장할 배열을 초기화합니다.
        originalMainImageColors = new Color[colorChange.Length];

        // 모든 colorChange 이미지를 순회합니다.
        for (int i = 0; i < colorChange.Length; i++)
        {
            // 각 메인 이미지의 원래 색상을 저장합니다.
            originalMainImageColors[i] = colorChange[i].color;

            // 현재 메인 이미지의 모든 자식 이미지를 가져옵니다.
            Image[] childImages = colorChange[i].GetComponentsInChildren<Image>();

            // 각 자식 이미지의 색상을 저장할 배열을 초기화합니다.
            Color[] childColors = new Color[childImages.Length];

            // 모든 자식 이미지를 순회합니다.
            for (int j = 0; j < childImages.Length; j++)
            {
                // 각 자식 이미지의 원래 색상을 저장합니다.
                childColors[j] = childImages[j].color;
            }

            // 자식 이미지의 색상 배열을 리스트에 추가합니다.
            originalChildImageColors.Add(childColors);
        }
    }

    void ChangeColor()
    {
        // 모든 colorChange 이미지를 순회합니다.
        for (int i = 0; i < colorChange.Length; i++)
        {
            // 현재 메인 이미지와 자식 이미지를 가져옵니다.
            Image mainImage = colorChange[i];
            Image[] childImages = mainImage.GetComponentsInChildren<Image>();

            // 퀘스트가 완료되지 않았다면
            if (!isQuestCompleted[i])
            {
                // 메인 이미지와 자식 이미지의 색상을 변경합니다.
                mainImage.color = new Color(0.4f, 0.4f, 0.4f);
                foreach (Image childImage in childImages)
                {
                    childImage.color = new Color(0.4f, 0.4f, 0.4f);
                }
            }
            else // 퀘스트가 완료되었다면
            {
                // 메인 이미지와 자식 이미지의 색상을 원래 색상으로 되돌립니다.
                mainImage.color = originalMainImageColors[i];
                for (int j = 0; j < childImages.Length; j++)
                {
                    childImages[j].color = originalChildImageColors[i][j];
                }
            }
        }
    }


    // fillamount가 1이라면 활성화 색변경시키고 시키고 클릭 가능 클릭하면 원래색으로
    public void AchiveMentUIOpen()
    {
        AchivementUIGroup.localPosition = new Vector3(0.0f, 130f, 0f);
    }
    public void AchiveMentUIClose()
    {
        AchivementUIGroup.localPosition = new Vector3(825f, 130f, 0f);
    }
}
