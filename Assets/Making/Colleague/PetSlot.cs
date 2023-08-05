using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PetSlot : MonoBehaviour
{
    public PetInfo petInfo;
    public Image icon;
    public Text countText;

    public void SetData(PetInfo petInfo)
    {
        this.petInfo = petInfo;
        icon.sprite = Resources.Load<Sprite>(petInfo.iconPath);
        if(countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }

    public void SetData(PetInstance petInstance)
    {
        SetData(petInstance.petInfo);
        if(countText != null)
        {
            countText.gameObject.SetActive(true);
            countText.text = petInstance.count.ToString();
        }
    }
}