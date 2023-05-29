using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Item1
{
    public enum ItemGrade
    {
        FFF,
        EEE,
        DDD,
        CCC,
        BBB,
        AAA,
        SSS
    }

    /// <summary>
    /// 게임에 등장하는 아이템 각각 하나하나를 나타내는 데이터.
    /// </summary>
    [CreateAssetMenu(menuName = "My Assets/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public ItemGrade grade;
        public string name;
        public string iconPath;
        public int Number; // 인벤토리에서 몇번째 아이템인지를 나타냄. 예를 들어 인벤토리의 좌상단 아이템은 Number == 1이다.
    }
}
