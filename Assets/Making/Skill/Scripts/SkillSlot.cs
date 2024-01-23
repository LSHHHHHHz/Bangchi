using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//아래 using을 쓰니 backGroundImage SetActive 사용 가능해짐
using Assets.HeroEditor.Common.Scripts.Common;
using Assets.Battle;

public class SkillSlot : MonoBehaviour
{
    public SkillInfo skillInfo;
    public Image icon;
    public Image backGroundImage;
    public Image effectImage;
    public Text countText;

    public Image hideIcon;
    private Coroutine cooldownCoroutine;
    public int skillCollDownCount =0;
    public bool isMaxSkillcount;

    public bool IsCooldown { get; private set; }

    private void Start()
    {
        BattleManager.instance.OnStageRestart += EndSkillCooldown;
        
    }
    private void Update()
    {
        isMaxSkillCount();
    }
    public void isMaxSkillCount()
    {
        if (skillInfo != null && skillInfo.colldownCount == skillCollDownCount && skillInfo.type == SkillType.Passive)
        {
            isMaxSkillcount = true;
        }

    }
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
        cooldownCoroutine = StartCoroutine(CooldownCoroutine(duration, skillInfo.colldownCount));
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
        skillCollDownCount = 0;
        isMaxSkillcount = false;
        hideIcon.fillAmount = 0f; 
    }
    private IEnumerator CooldownCoroutine(float duration, int colldownCount)
    {
        for (int i = 0; i < colldownCount; i++)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                hideIcon.fillAmount = 1f - (elapsed / duration);
                yield return null;
            }
            hideIcon.fillAmount = 0f;
            skillCollDownCount++; //쿨다운 돌았을 때
        }
        IsCooldown = false;
    }
}
