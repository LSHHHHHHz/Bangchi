using System;
using Assets.Battle;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class FadeInOutStageProcessor : MonoBehaviour
{
    public bool bossstageDone = false;
    public static FadeInOutStageProcessor instance;
    public event Action stageClickForSkillDestory;
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
        RunFadeOutInBoss(() => BattleManager.instance.StartSunbossStage(sunBossInfo, level), 4f);
    }

    private void OnBossStageDone()
    {
        if (lastStage != null)
            BattleManager.instance.StartStage(lastStage);

    }

    private bool test = false;
    public void RunFadeOutStage()
    {
        if (test == true)
        {
            int wefwaef = 0;
            return;
        }

        test = true;
        var sequence = DOTween.Sequence();

        fadeImage.enabled = true;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 1), 2f));
        sequence.AppendInterval(1);
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 0f), 1f));
        sequence.onComplete += () => { fadeImage.enabled = false; test = false; };
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

    //스테이지 선택할 때 사용
    public void RunFadeOutInStage(TweenCallback callback, float fadeOutTime)
    {
        var sequence = DOTween.Sequence();
        //이미지 활성화
        fadeImage.enabled = true;
        //이미지를 검은색으로 시작
        fadeImage.color = new Color(0, 0, 0, 1f);
        //남아있는스킬 파괴
        stageClickForSkillDestory?.Invoke();
        sequence.AppendCallback(callback);
        //화면 n초동안 복구
        sequence.Append(fadeImage.DOColor(new Color(0, 0, 0, 0), fadeOutTime));
        //완료가 되면 이미지를 비활성화시킴
        sequence.onComplete += () =>
        {
            fadeImage.enabled = false;
        };
        sequence.Play();

    }
    public void RunFadeOutInBoss(TweenCallback fadeOutDoneCallback, float fadeOutTime)
    {
        var sequence = DOTween.Sequence();
        //이미지 활성화
        fadeImage.enabled = true;
        //이미지를 검은색으로 시작
        fadeImage.color = new Color(0, 0, 0, 1f);
        //캐릭터 및 보스 소환
        sequence.AppendCallback(fadeOutDoneCallback);
        //보스 배경 활성화
        sequence.AppendCallback(onbossStage);
        //화면 4초동안 복구
        sequence.Append(fadeImage.DOColor(new Color(0, 0, 0, 0), fadeOutTime));
        //완료가 되면 이미지를 비활성화시킴
        sequence.onComplete += () =>
        {
            fadeImage.enabled = false;
        };
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