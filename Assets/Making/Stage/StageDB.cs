
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "My Assets/StageDB")]
public class StageDB : ScriptableObject
{
    public List<StageInfo> stages;
}
