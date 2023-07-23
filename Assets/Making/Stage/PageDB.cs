
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "My Assets/PageDB")]
public class PageDB : ScriptableObject
{
    public List<StageInfo> stagePage;
}
