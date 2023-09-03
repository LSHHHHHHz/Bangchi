using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Item1;

[Serializable] //실제로 내가 가지고 있는 아이템을 표현. 내 아이템의 업그레이드 레벨, 아이템 개수등이 주요 데이터.
public class ItemInstance
{
    public ItemInfo itemInfo;
    public int count;
    public int upgradeLevel;
}
