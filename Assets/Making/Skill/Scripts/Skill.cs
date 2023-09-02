using Assets.Making.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public RectTransform uiGroup;
    public RectTransform skillSlectuiGroup;
    public RectTransform talkText;
    public Player enterPlayer;

    public void EnterSkill()
    {
        UIManager.instance.OnBottomButtonClicked();
        uiGroup.anchoredPosition = new Vector3(0, 0, 0);
    }

    public void ExitSkill()
    {
        uiGroup.anchoredPosition = new Vector3(-3250, -1338, 0);
    }
}
