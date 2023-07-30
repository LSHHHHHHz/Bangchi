using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/PageInfo")]
public class PageInfo : ScriptableObject
{
    public int PageNumber;
    public string pageIconPath;

    // 여기에 스테이지들을 연결합니다.
    public List<StageInfo> stages = new();
}
