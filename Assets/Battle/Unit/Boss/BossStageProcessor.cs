using System;
using Assets.Battle;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossStageProcessor : MonoBehaviour
{
    public static BossStageProcessor instance;

    [SerializeField]
    private Image fadeImage;

    private StageInfo lastStage;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        BattleManager.instance.OnStageDone += OnStageDone;
    }

    private void OnStageDone(StageInfo stageInfo)
    {
        if (stageInfo.Type == StageType.Boss)
        {
            OnBossStageDone();
        }
    }

    public void RunBossStage(StageInfo stageInfo)
    {
        lastStage = BattleManager.instance.currentStageInfo;
        RunFadeOutIn(() =>
        {
            BattleManager.instance.StartStage(stageInfo);
        });
    }

    private void RunFadeOutIn(TweenCallback fadeOutDoneCallback)
    {
        var sequence = DOTween.Sequence();
        Time.timeScale = 0f;
        fadeImage.enabled = true;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 1f), 1f).SetUpdate(isIndependentUpdate: true));

        // 캐릭터 옮기기, 보스 소환하기
        sequence.AppendCallback(fadeOutDoneCallback);

        sequence.AppendInterval(2f);

        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 0f), 1f).SetUpdate(isIndependentUpdate: true));
        sequence.onComplete += () =>
        {
            fadeImage.enabled = false;
            Time.timeScale = 1f;
        };
        sequence.SetUpdate(isIndependentUpdate: true);
        sequence.Play();
    }

    private void OnBossStageDone()
    {
        RunFadeOutIn(() =>
        {
            BattleManager.instance.StartStage(lastStage);
        });
    }
}