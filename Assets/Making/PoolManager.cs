//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.Pool;

//namespace Assets.Making
//{
//    public class PoolManager : MonoBehaviour
//    {
//        public static PoolManager instance;


//        private void Awake()
//        {
//            instance = this;
//        }

//        public GameObject[] prefabs;

//        private Dictionary<GameObject, List<GameObject>> pools = new();

//        public GameObject Get(GameObject prefab)
//        {
//            if (pools.TryGetValue(prefab, out List<GameObject> list) == false)
//            {
//                list = new List<GameObject>();
//                pools[prefab] = list;
//            }

//            if (list.Count == 0)
//            {
//                list.Add(Instantiate(prefab));
//            }

//            var result = list[list.Count - 1];
//            list.RemoveAt(list.Count - 1);
//            return result;
//        }

//        public void Return(GameObject instance)
//        {
//            //if (pools.TryGetValue(prefab, out List<GameObject> list) == false)
//            //{
//            //    Debug.LogError("No pool list! " + prefab);
//            //    return;
//            //}

//            list.Add(instance);
//        }
//    }
//}