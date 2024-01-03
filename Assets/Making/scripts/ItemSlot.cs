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


    //캐릭터 상태창에 넣는데이터를 따로 넣어야 하는지
    //가차 실행 후 흰색 네모가 fadeOff 하면서 뒷 배경이 보이도록 하고 싶은데
    //이러면 캐릭터 상태창에도 동일하게 적용될 것 같아서 안함
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

    public void SetEmpty(Sprite emptySprite) // 기존에 설정된 스킬 데이터를 날린다.
    {
        icon.sprite = emptySprite;
        itemInfo = null;
    }
}
