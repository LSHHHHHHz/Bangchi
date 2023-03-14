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
    }
}
