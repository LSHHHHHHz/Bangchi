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
        D,
        C,
        B,
        A,
        S
    }

    public enum PetType
    {
        Water,
        Soil,
        Wind,
        Fire
    }

    public enum ItemType
    {
        Sword, Shield, Colleague, pet
    }

    /// <summary>
    /// ���ӿ� �����ϴ� ������ ���� �ϳ��ϳ��� ��Ÿ���� ������.
    /// </summary>
    [CreateAssetMenu(menuName = "My Assets/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public ItemGrade grade;
        public ItemType type;
        public PetType petType;
        public string name;
        public string iconPath;
        public int Number; // �κ��丮���� ���° ������������ ��Ÿ��
    }
}
