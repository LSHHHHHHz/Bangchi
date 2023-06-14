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
    public enum ItemType
    {
        Sword, Shield,   }

    /// <summary>
    /// ���ӿ� �����ϴ� ������ ���� �ϳ��ϳ��� ��Ÿ���� ������.
    /// </summary>
    [CreateAssetMenu(menuName = "My Assets/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public ItemGrade grade;
        public ItemType type;
        public string name;
        public string iconPath;
        public int Number; // �κ��丮���� ���° ������������ ��Ÿ��. ���� ��� �κ��丮�� �»�� �������� Number == 1�̴�.
    }
}