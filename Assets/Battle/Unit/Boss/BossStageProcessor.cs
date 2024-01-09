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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
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

    public void RunBossStage(SunBossInfo sunBossInfo, int level)
    {
        lastStage = BattleManager.instance.currentStageInfo;
        RunFadeOutIn(() =>
        {
            BattleManager.instance.StartSunbossStage(sunBossInfo, level);
        });
    }

    private void OnBossStageDone()
    {
        // 보스스테이지가 끝났으니 다시 원래 하던 스테이지(lastStage)로 돌아간다.
        RunFadeOutIn(() =>
        {
            if (lastStage != null)
                BattleManager.instance.StartStage(lastStage);
        });
    }

    private void RunFadeOutIn(TweenCallback fadeOutDoneCallback)
    {
        var sequence = DOTween.Sequence();
        Time.timeScale = 0f;
        fadeImage.enabled = true;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);

        // 화면 1초동안 까매지기
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 1f), 1f));

        // 캐릭터 옮기기, 보스 소환하기
        sequence.AppendCallback(fadeOutDoneCallback);

        // 2초 대기
        sequence.AppendInterval(2f);

        // 화면 1초동안 원상 복구
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 0f), 1f));
        sequence.onComplete += () =>
        {
            fadeImage.enabled = false;
            Time.timeScale = 1f;
        };

        // 원래 DoTween이 timeScale의 영향을 받음. 아래처럼 Update 규칙을 true로 설정해주면 timeScale의 영향을 안받고 독립적인 Update를 수행함.
        sequence.SetUpdate(isIndependentUpdate: true);
        sequence.Play();
    }
}