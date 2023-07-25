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

    public void SetData(SkillInfo skillInfo) //SkillSlot에 정보를 설정하는 함수
    {
        this.skillInfo = skillInfo;
        // Resources.Load()를 쓸때엔 Resources 폴더 하위에 있어야 하고, Resources 경로 하위의 경로만 입력한다. 확장자는 입력하지 않는다.
        // Assets/Making/Resources/SkillIcon/asdasdadw.png --> 경로 입력 : "SkillIcon/asdasdadw"
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
        skillInfo = null; // 기존에 설정된 스킬 데이터를 날린다.
    }
}
