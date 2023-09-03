using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/PageInfo")]
public class PageInfo : ScriptableObject
{
    public int PageNumber;
    public string pageIconPath;

    public List<StageInfo> stages = new();
}
