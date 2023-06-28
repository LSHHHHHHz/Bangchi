using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    public PetType type;

    public Text[] PetPriceText;
    public int[] PetPrice;

    public void Awake()
    {
        
    }
    public Pet()
    {
        PetPrice = new int[3]; // 길이가 3인 배열을 생성

    }

    public void Update()
    {
        PetPriceText[0].text = PetPrice[0].ToString();
        PetPriceText[1].text = PetPrice[1].ToString();
        PetPriceText[2].text = PetPrice[2].ToString();
    }
    public void Buy(int index)
    {
        int price = PetPrice[index];
        if (price > Player.instance.PetCoin)
        {
            return;
        }
        if(index ==0 && type == PetType.Water)
        {
            Player.instance.PetCoin -= price;
            PetPrice[index] += PetPrice[index];
        }


    }
}

