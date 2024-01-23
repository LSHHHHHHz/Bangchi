using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PetSlot : MonoBehaviour
{
    public PetInfo petInfo;
    public Image icon;
    public Image effecticon;
    public Text countText;
    public Button button;

    private void Awake()
    {
        if (button != null)
        {
            button.onClick.AddListener(() => PetInfoPopup());
            button.onClick.AddListener(() => PetInventoryManager.Instance.EquipPet(this.petInfo));
        }
    }
    public void SetData(PetInfo petInfo)
    {
        this.petInfo = petInfo;
        icon.sprite = Resources.Load<Sprite>(petInfo.iconPath);
        if (countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }
    public void SetData(PetInstance petInstance)
    {
        SetData(petInstance.petInfo);
        if (countText != null)
        {
            countText.gameObject.SetActive(true);
            countText.text = petInstance.count.ToString();
        }
    }
    public void ResetData()
    {
        this.petInfo = null;
        icon.sprite = null;
        effecticon.sprite = null;
        this.gameObject.SetActive(false);
    }
    //버튼을 누르면서 PetInfoPopup에 같이 붙어있는 펫 PetSlot에 PetInfo 데이터를 넣어주고 그 데이터를 쓰게만듬
    public void PetInfoPopup()
    {
        GameObject prefab = Resources.Load<GameObject>("PetInfoPopup");

        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab);
            petinfoPopup petinfoPopup = instance.GetComponent<petinfoPopup>();
            PetInventoryManager.Instance.petinfoPopup.Add(petinfoPopup);

            PetSlot petinfopopup = instance.GetComponent<PetSlot>();
            petinfopopup.SetData(petInfo);

        }
        else
        {
            Debug.LogError("프리펩 확인 불가");
        }

    }

}