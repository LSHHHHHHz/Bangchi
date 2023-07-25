using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillInfo skillInfo;
    public Image icon;
    public Text countText;

    public void SetData(SkillInfo skillInfo) //SkillSlot�� ������ �����ϴ� �Լ�
    {
        this.skillInfo = skillInfo;
        // Resources.Load()�� ������ Resources ���� ������ �־�� �ϰ�, Resources ��� ������ ��θ� �Է��Ѵ�. Ȯ���ڴ� �Է����� �ʴ´�.
        // Assets/Making/Resources/SkillIcon/asdasdadw.png --> ��� �Է� : "SkillIcon/asdasdadw"
        icon.sprite = Resources.Load<Sprite>(skillInfo.iconPath);
        if (countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }

    public void SetData(SkillInstance skillInstance)
    {
        SetData(skillInstance.skillInfo);
        if (countText != null)
        {
            countText.gameObject.SetActive(true);
            countText.text = skillInstance.count.ToString();
        }
    }

    public void SetEmpty(Sprite emptySprite)
    {
        icon.sprite = emptySprite;
        skillInfo = null; // ������ ������ ��ų �����͸� ������.
    }
}
