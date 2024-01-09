using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenesMove : MonoBehaviour
{
    public void ExpBossSceneMove()
    {
        SceneManager.LoadScene("ExpBoss");
    }

    public void BasicSceneMove()
    {
        SceneManager.LoadScene("240101");
    }
 
}
