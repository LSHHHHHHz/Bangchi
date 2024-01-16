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
            OnBossStageDone();
            bossstageDone = true;
        }
    }
    //문제점 : 보스가 끝날 때 화면이 꺼졋다 커지는게 두번 반복됨
               //보스가 끝났을 때 꺼짐과동시에 스테이지가 시작되면서 다시 페이드오프가 시작되는것처럼보임
               //수정 후 문제 지금 스테이지가 끝나면 자동으로 실행되는 무언가가 있는것 같음 그것을 찾으면 됨


    //문제점 : 스테이지가 끝나면 3초동안 멈추지 말고 페이드오프를 3초동안 천천히 진행해야함
     
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
    public void RunFadeOutBoss()
    {
        var sequence = DOTween.Sequence();

        fadeImage.enabled = true;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 1), 3f));
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