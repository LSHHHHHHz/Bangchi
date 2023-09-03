using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Making.scripts
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        private void Awake()
        {
            instance = this;
        }
        public void OnBottomButtonClicked()
        {
            if (BossUI.instance.isShowing)
                BossUI.instance.PanelFadeOut();

            PetUI.instance.petUIClose();
        }
    }
}
