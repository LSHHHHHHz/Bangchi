using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Item1
{
    /// <summary>
    /// ���ӿ� �����ϴ� �����۵��� �����ϱ� ���� �����ͺ��̽�
    /// </summary>
    [CreateAssetMenu(menuName = "My Assets/ItemDB")]
    public class ItemDB : ScriptableObject
    {
        public List<ItemInfo> items; //<> �ȿ� �ִ� Ÿ�� �ν��Ͻ��� ������ ���´�

        //������DB�� ����ִ� �������� Ư�� ������ ������ ������ �Լ�
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