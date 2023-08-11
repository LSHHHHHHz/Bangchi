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
        public List<ItemInfo> items; //<> 안에 있는 타입 인스턴스를 여러개 갖는다

        //아이템DB에 들어있는 아이템중 특정 아이템 종류만 얻어오는 함수
        public List<ItemInfo> GetItemsByType(ItemType type)
        {
            var result = new List<ItemInfo>();
            foreach (ItemInfo item in items)
            {
                if (item.type == type)
                {
                    result.Add(item);
                }           
            }

            return result;
        }

        public List<ItemInfo> GetItemGrade(ItemGrade itemGrade, ItemType type)
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

    }

    
}