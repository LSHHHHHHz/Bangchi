using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemInfo itemInfo;
    public Image icon;
    public Text countText;
    public ItemInstance itemInstance;

    public void SetData(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
        icon.sprite = Resources.Load<Sprite>(itemInfo.iconPath);
        if (countText != null)
        {
            
            countText.gameObject.SetActive(false);
        }
    }

    public void SetData(ItemInstance itemInstance)
    {
        SetData(itemInstance.itemInfo);
        if (countText != null)
        {
            if (itemInstance.count == 0)
            {
                countText.gameObject.SetActive(false);
            }
            else
            {
                countText.gameObject.SetActive(true);
                countText.text = itemInstance.count.ToString();
            }
        }
    }
    public void SetEmpty(Sprite emptySprite)
    {
        icon.sprite = emptySprite;
        itemInfo = null; // ������ ������ ��ų �����͸� ������.
    }
}
