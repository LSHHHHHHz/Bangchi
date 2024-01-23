using Assets.HeroEditor.Common.Scripts.Common;
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemInfo itemInfo;
    public Image icon;
    public Image backgroundImage;
    public Image effectImage;
    public Text countText;
    public int count;
    public int upgradeLevel;
    public void SetData(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
        icon.sprite = Resources.Load<Sprite>(itemInfo.iconPath);
        backgroundImage.sprite = Resources.Load<Sprite>(itemInfo.backgroundiconPath);
        this.backgroundImage.SetActive(true);
        
        if (countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }

    public void SetData(ItemInstance itemInstance)
    {
        this.count = itemInstance.count;

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
        itemInfo = null;
    }
}
