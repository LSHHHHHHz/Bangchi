using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenesMove : MonoBehaviour
{

    public void ExpBossSceeneFadeOutMove()
    {
        FadeInOutStageProcessor.instance.RunFadeOutIn(() => ExpBossSceneMove(), 1);
    }
    public void ExpBossSceneMove()
    {
        SceneManager.LoadScene("ExpBoss");
        gameObject.SetActive(false);
    }

    public void BasicSceneMove()
    {
        SceneManager.LoadScene("240101");
    }
 
}
