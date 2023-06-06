using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Item1
{
    [CreateAssetMenu(menuName = "My Assets/ItemDB_SH")]
    public class ItemDB_SH : ScriptableObject
    {
        public List<ItemInfo> items;
    }
}