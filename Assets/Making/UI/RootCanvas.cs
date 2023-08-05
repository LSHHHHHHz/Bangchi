using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Making.UI
{
    public class RootCanvas : MonoBehaviour
    {
        public static RootCanvas Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
