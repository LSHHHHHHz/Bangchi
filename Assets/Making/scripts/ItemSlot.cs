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
    public int count;
    public int upgradeLevel;

    public void SetData(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
        //itemInstance.upgradeLevel = itemInfo.Number;//★강화하려고 사용함
        icon.sprite = Resources.Load<Sprite>(itemInfo.iconPath);
        if (countText != null)
        {

            countText.gameObject.SetActive(false);
        }
    }

    public void SetData(ItemInstance itemInstance)
    {
        //this.itemInstance = itemInstance; //★강화하려고 사용함
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
        itemInfo = null; // 기존에 설정된 스킬 데이터를 날린다.
    }
}
