using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Item1
{
    /// <summary>
    /// 게임에 존재하는 아이템들을 관리하기 위한 데이터베이스
    /// </summary>
    [CreateAssetMenu(menuName = "My Assets/ItemDB")]
    public class ItemDB : ScriptableObject
    {
        public static ItemDB instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ItemDB>("GameData/Item/Item_DB");
                }

                return _instance;
            }
        }
        private static ItemDB _instance;

        public List<ItemInfo> items;
        

        public List<ItemInfo> GetItemGradeAndType(ItemGrade itemGrade, ItemType type)
        {
            List<ItemInfo> result = new List<ItemInfo>();

            foreach (ItemInfo item in items)
            {
                if(item.grade == itemGrade && item.type == type)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public ItemInfo GetItemInfoByNumber(int number)
        {
            foreach (ItemInfo item in items)
            {
                if (item.Number == number)
                {
                    return item;
                }
            }
            return null;
        }
    }
}