using System;
using Assets.Battle;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class FadeInOutStageProcessor : MonoBehaviour
{
    public bool bossstageDone = false;
    public static FadeInOutStageProcessor instance;

    public int bossTime = 2;
    public int petInfoTime = 1;


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
            //재실행 안되게끔 우선 지우고 스테이지 다시 저장
            //OnBossStageDone();
            BattleManager.instance.currentStageInfo = lastStage;
            bossstageDone = true;
        }
    }
     
    public void RunBossStage(SunBossInfo sunBossInfo, int level)
    {
        lastStage = BattleManager.instance.currentStageInfo;
        RunFadeOutIn(() =>
        {
            BattleManager.instance.StartSunbossStage(sunBossInfo, level);
        },2);
    }

    private void OnBossStageDone()
    {
        if (lastStage != null)
            BattleManager.instance.StartStage(lastStage);

    }
    public void RunFadeOutStage()
    {
        var sequence = DOTween.Sequence();

        fadeImage.enabled = true;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 1), 2f));
        sequence.AppendInterval(1);
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 0f), 1f));
        sequence.onComplete += () => { fadeImage.enabled = false; };
        sequence.Play();
    }

    public void RunFadeOutIn(TweenCallback fadeOutDoneCallback,float fadeOutTime)
    {
        var sequence = DOTween.Sequence();

        Time.timeScale = 0f;
        fadeImage.enabled = true;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);

        // 화면 fadeOutTime초동안 까매지기
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 1f), fadeOutTime));

        // 캐릭터 옮기기, 보스 소환하기
        sequence.AppendCallback(fadeOutDoneCallback);

        // 2초 대기
        sequence.AppendInterval(2);
        sequence.AppendCallback(onbossStage);
        // 화면 1초동안 원상 복구
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 0f), 3f));
        sequence.onComplete += () =>
        {
            fadeImage.enabled = false;
            Time.timeScale = 1f;
        };

        // 원래 DoTween이 timeScale의 영향을 받음. 아래처럼 Update 규칙을 true로 설정해주면 timeScale의 영향을 안받고 독립적인 Update를 수행함.
        sequence.SetUpdate(isIndependentUpdate: true);
        sequence.Play();
    }
    private void onbossStage()
    {
        GameObject backGroundImage = GameObject.Find("BackGroundImage");
        if (backGroundImage != null)
        {
            foreach (Transform child in backGroundImage.transform)
            {
                PageBackGroundScript pageScript = child.GetComponent<PageBackGroundScript>();
                if (pageScript != null)
                {
                    if (pageScript.stageType == StageType.Boss)
                    {
                        child.gameObject.SetActive(true);
                    }
                    else
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public void RunFadeOutIn(float fadeOutTime)
    {
        var sequence = DOTween.Sequence();
        Time.timeScale = 0f;
        fadeImage.enabled = true;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);

        // 화면 fadeOutTime초동안 까매지기
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 1f), fadeOutTime));


        // 0.5초 대기
        sequence.AppendInterval(0.5f);

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