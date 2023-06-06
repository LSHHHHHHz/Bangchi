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
    }
    
}