using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//아래 using을 쓰니 backGroundImage SetActive 사용 가능해짐
using Assets.HeroEditor.Common.Scripts.Common;


public class SkillSlot : MonoBehaviour
{
    public SkillInfo skillInfo;
    public Image icon;
    public Image backGroundImage;
    public Image effectImage;
    public Text countText;

    public Image hideIcon;
    private Coroutine cooldownCoroutine;

    public bool IsCooldown { get; private set; }

    public void SetData(SkillInfo skillInfo)
    {
        this.skillInfo = skillInfo;
        if (skillInfo == null)
            return;

        icon.sprite = Resources.Load<Sprite>(skillInfo.iconPath);
        backGroundImage.sprite = Resources.Load<Sprite>(skillInfo.backGroundIconPath);
        this.backGroundImage.SetActive(true);
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
        backGroundImage.sprite = emptySprite;
        icon.sprite = emptySprite;
        skillInfo = null;
    } 
    
    // 스킬 쿨다운 시작
    public void StartSkillCooldown(float duration)
    {
        IsCooldown = true;
        hideIcon.gameObject.SetActive(true);
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
        cooldownCoroutine = StartCoroutine(CooldownCoroutine(duration));
    }

    // 스킬 쿨다운 종료
    public void EndSkillCooldown()
    {
        IsCooldown = false;
        hideIcon.gameObject.SetActive(false);
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
        hideIcon.fillAmount = 0f; 
    }

    private IEnumerator CooldownCoroutine(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            hideIcon.fillAmount = 1f - (elapsed / duration);
            yield return null;
        }
        hideIcon.fillAmount = 0f;
        IsCooldown = false;
    }
}
