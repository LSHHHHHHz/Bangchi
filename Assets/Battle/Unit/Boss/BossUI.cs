using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Item1;
using Unity.VisualScripting;
using System;
using Assets.Making.scripts;

public class BossUI : MonoBehaviour
{
    public static BossUI instance;

    public EXP_Cristal exp;
    public float fadeTime = 1;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public GameObject backGround;
    public GameObject Bar;
    public bool isTimerActive = false;
    public bool isShowing;

    private void Awake()
    {
        instance = this;
    }

    public void PanelFadeIn()
    {
        UIManager.instance.OnBottomButtonClicked();
        isShowing = true;
        canvasGroup.alpha = 0;
        rectTransform.transform.localPosition = new Vector3(0f, -1500f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);
    }
    public void PanelFadeOut()
    {
        isShowing = false;
        canvasGroup.alpha = 1;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -1500f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup.DOFade(0, fadeTime);
    }

    public void Run_Exp_Boss()
    {
        isTimerActive = true;

        backGround.SetActive(true);
        Bar.SetActive(true);
        rectTransform.transform.localPosition = new Vector3(0f, -1500f, 0f);
        canvasGroup.DOFade(0, fadeTime);
    }
    public void Exit_Exp_Boss()
    {
        isTimerActive = false;

        backGround.SetActive(false);
        Bar.SetActive(false);
    }

}
