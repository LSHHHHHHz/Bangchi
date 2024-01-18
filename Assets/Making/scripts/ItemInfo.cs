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
    /// 게임에 등장하는 아이템 각각 하나하나를 나타내는 데이터.
    /// </summary>
    [CreateAssetMenu(menuName = "My Assets/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public ItemGrade grade;
        public ItemType type;
        public PetType petType;
        public string name;
        public string iconPath;
        public string backgroundiconPath;
        public int Number; // 인벤토리에서 몇번째 아이템인지를 나타냄
        public int Attack;
        public int HP;
        public int HP_recovery;
        public string iteminfoText;
    }
}
