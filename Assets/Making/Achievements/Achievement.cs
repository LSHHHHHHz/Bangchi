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


    // fillamount�� 1�̶�� Ȱ��ȭ �������Ű�� ��Ű�� Ŭ�� ���� Ŭ���ϸ� ����������
    public void AchiveMentUIOpen()
    {
        AchivementUIGroup.localPosition = new Vector3(0.0f, 130f, 0f);
    }
    public void AchiveMentUIClose()
    {
        AchivementUIGroup.localPosition = new Vector3(825f, 130f, 0f);
    }
}
