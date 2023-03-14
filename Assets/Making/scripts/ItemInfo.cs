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
    /// ���ӿ� �����ϴ� ������ ���� �ϳ��ϳ��� ��Ÿ���� ������.
    /// </summary>
    [CreateAssetMenu(menuName = "My Assets/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public ItemGrade grade;
        public string name;
        public string iconPath;
    }
}
