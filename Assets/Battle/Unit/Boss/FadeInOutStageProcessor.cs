using System;
using Assets.Battle;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutProcessor
{
    
}

public class FadeInOutStageProcessor : MonoBehaviour
{
    public static FadeInOutStageProcessor instance;
    public event Action FadeOutAndResetSkillsOnStageChange;
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
            BattleManager.instance.currentStageInfo = lastStage;
        }
    }
    public void RunBossStage(SunBossInfo sunBossInfo, int level)
    {
        lastStage = BattleManager.instance.currentStageInfo;
        RunFadeOutInBoss(() => BattleManager.instance.StartSunbossStage(sunBossInfo, level), 4f);
    }
    public void RunFadeOutStage()
    {
        var sequence = DOTween.Sequence();

        fadeImage.enabled = true;
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 1), 2f));
        sequence.AppendInterval(1);
        sequence.Append(fadeImage.DOColor(new Color(0f, 0f, 0f, 0f), 1f));
        sequence.onComplete += () => { fadeImage.enabled = false;};
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
        FadeOutAndResetSkillsOnStageChange?.Invoke();
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
}