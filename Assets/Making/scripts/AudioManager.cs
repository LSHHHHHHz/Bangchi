using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioPlayerAttack;
    public AudioClip audioDropItem;
    public AudioClip audioMonsterDie;
    AudioSource audioSource;
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource 컴포넌트가 없으면 추가합니다.
            audioSource = gameObject.AddComponent<AudioSource>();
        }

    }
    public void PlaySound(string action)
    {
        switch (action)
        {
            case "PlayerAttack":
                audioSource.clip = audioPlayerAttack;
                break;
            case "DropItem":
                audioSource.clip = audioDropItem;
                break;
            case "MonsterDie":
                audioSource.clip = audioMonsterDie;
                break;
        }
    }
}
