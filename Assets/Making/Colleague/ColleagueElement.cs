using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Making.Colleague
{
    public class ColleagueElement : MonoBehaviour
    {
        public int[] ColleagueStatsPrice;
        public Text[] ColleagueStatsPriceText;
        public Text[] ColleagueStatsLVText;
        public ColleagueType colleagueType;

        public int First_stat;
        public int First_stat_LV;
        public int Second_stat;
        public int Second_stat_LV;
        public int Third_stat;
        public int Third_stat_LV;



        public void Update()
        {
            ColleagueStatsPriceText[0].text = ColleagueStatsPrice[0].ToString();
            ColleagueStatsPriceText[1].text = ColleagueStatsPrice[1].ToString();
            ColleagueStatsPriceText[2].text = ColleagueStatsPrice[2].ToString();


            ColleagueStatsLVText[0].text = First_stat_LV.ToString();
            ColleagueStatsLVText[1].text = Second_stat_LV.ToString();
            ColleagueStatsLVText[2].text = Third_stat_LV.ToString();

        }
        public void ColleagueStatusBuy(int statButtonIndex)
        {
            int typeIndex = (int)colleagueType;
            int price = ColleagueStatsPrice[statButtonIndex];

            if (Player.instance.ColleageCoinWater > price)
            {
                switch (statButtonIndex)
                {
                    case 0:
                        First_stat_LV += 1;
                        First_stat += First_stat_LV;
                        break;
                    case 1:
                        Second_stat_LV += 1;
                        Second_stat += Second_stat_LV;
                        break;
                    case 2:
                        Third_stat_LV += 1;
                        Third_stat += Third_stat_LV;
                        break;
                }
                ColleagueStatsPrice[statButtonIndex] += 100 * (statButtonIndex + 1);
                ColleagueStatsPriceText[statButtonIndex].text = ColleagueStatsPrice[statButtonIndex].ToString();
            }

            else
            {
                return;
            }
        }

    }
}
