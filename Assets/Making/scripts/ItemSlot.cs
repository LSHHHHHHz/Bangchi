using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    public Text countText;

    public void SetData(ItemInfo itemInfo)
    {
        icon.sprite = Resources.Load<Sprite>(itemInfo.iconPath);
        countText.gameObject.SetActive(false);
    }

    public void SetData(ItemInstance itemInstance)
    {
        SetData(itemInstance.itemInfo);
        countText.gameObject.SetActive(true);
        countText.text = itemInstance.count.ToString();
    }
}
