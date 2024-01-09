using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    private void Awake()
    {

        instance = this;
        DontDestroyOnLoad(gameObject);

    }
}
