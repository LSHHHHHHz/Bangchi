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
    }
    
}