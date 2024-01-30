using Assets.Battle;
using Assets.HeroEditor.Common.Scripts.Common;
using Assets.Item1;
using Assets.Making.scripts;
using Assets.Making.Stage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PetUI : MonoBehaviour
{
    petinfoPopup petinfopopup;

    public static PetUI instance;
    public RectTransform petUI;
    public RectTransform PetAndColleagueUI;

    public GameObject GridUI;

    public RectTransform PetSlotParent;
    public PetSlot[] petSlots;
    List<PetSlot> petSlotsList = new();

    public GameObject BuyPetSlot;
    public RectTransform petInventorySizeUpButton;
    public Text petInventorySizeStatus;
    public RectTransform diamondLackBackGround;
    public RectTransform diamondLackText;

    public PetType type;

    public Text[] PetPriceText;
    public int[] PetPrice;
    PetPopup petPopup;
    public PetDB petDB;
    public Sprite lockedSprite;

    public RectTransform isBuy;

    public GridLayoutGroup grid;
    public void Awake()
    {
        instance = this;
        List<PetSlot> childList = new();
        for (int i = 0; i < PetSlotParent.childCount; i++)
        {
            PetSlot child = PetSlotParent.GetChild(i).GetComponent<PetSlot>();
            childList.Add(child);
        }
        petSlots = childList.ToArray();
    }
    public void Start()
    {
        gridSizeChange();
    }
    public void Update()
    {
        petInventorySizeStatus.text = PetInventoryManager.Instance.myPets.Count + " / " + PetInventoryManager.Instance.maxaccumulatePetsCount + "";
    }
    public void gridSizeChange()
    {
        RectTransform rectTransform = GridUI.GetComponent<RectTransform>();
        Vector2 size = rectTransform.sizeDelta;
         int count = PetInventoryManager.Instance.myPets.Count / 4;
        if (count >= 4)
        {
            size.y = 800 + 160 * (count - 4);
            rectTransform.sizeDelta = size;
            
        }
    }
    public void RunPet(int count)
    {
        if (PetInventoryManager.Instance.myPets.Count + count<= PetInventoryManager.Instance.maxaccumulatePetsCount)
        {
            if (petPopup == null)
            {
                GameObject prefab = Resources.Load<GameObject>("PetPopup");

                petPopup = Instantiate(prefab).GetComponent<PetPopup>();
            }

            PetGachaResult petGachaResult = PetGachaCalculator.Calculate(count, petDB);


            foreach (var pet in petGachaResult.pets)
            {
                PetInventoryManager.Instance.AddPet(pet);

            }
            PetInventoryManager.Instance.Save();

            petPopup.Initialize(petGachaResult, this.RunPet);
            gridSizeChange();
        }
        else
        {
            return;
        }
    }

    public void SortSlots()
    {
        bool isActive = Array.Exists(petSlots, x => x.petInfo != null);
        petSlotsList.Clear();
        foreach (PetSlot slot in petSlots)
        {
            if (slot.petInfo != null)
            {
                petSlotsList.Add(slot);
            }
        }

        if (isActive)
        {
            petSlotsList.Sort((x, y) =>
            {
                int petNumberCompare = y.petInfo.Number.CompareTo(x.petInfo.Number);
                if (petNumberCompare != 0) return petNumberCompare;

                return x.petInfo.petType.CompareTo(y.petInfo.petType);
            });


            for (int i = 0; i < petSlotsList.Count; ++i)
            {
                PetSlot slot = petSlotsList[i];
                slot.transform.SetSiblingIndex(i);
            }
        }
    }

    public void SetData()
    {
        int slotCount = petSlots.Length;

        for (int i = 0; i < PetInventoryManager.Instance.myPets.Count; i++)
        {
            if (i >= slotCount)
            {
                Debug.LogWarning("확장필요");
                break;
            }

            PetSlot slot = petSlots[i];
            slot.SetData(PetInventoryManager.Instance.myPets[i]);
            slot.SetActive(true);
        }
    }
    public void PetInventorySizeOpen()
    {
        petInventorySizeUpButton.localPosition = new Vector3(0, 0, 0);
    }
    public void PetInventorySizeClose()
    {
        petInventorySizeUpButton.localPosition = new Vector3(-600, 0, 0);
    }
    public void PetInventorySizeBuing()
    {
        if (grid.transform.childCount <= PetInventoryManager.Instance.maxaccumulatePetsCount)
        { 
            Debug.Log("최대치");
            return;
        }
        if (Player.instance.Diamond >= 3000)
        {
            Player.instance.Diamond -= 3000;
            PetInventoryManager.Instance.maxaccumulatePetsCount += 5;
            PetInventoryManager.Instance.petCount+= 5;
            PetInventoryManager.Instance.Save();
        }
        else if(Player.instance.Diamond<3000)
        {
            StartCoroutine(FadeOutDiamondLackRectBackGround(1, diamondLackBackGround));
            StartCoroutine(FadeOutDiamondLackText(1, diamondLackText));
        }
        
    }
    IEnumerator FadeOutDiamondLackRectBackGround(float duration, RectTransform objects)
    {
        objects.localPosition = new Vector3(0, 0, 0);
        Image image = objects.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        if (image != null)
        {
            float currentTime = 0;
            float startAlpha = image.color.a;

            while (currentTime < duration)
            {
                float alpha = Mathf.Lerp(startAlpha, 0, currentTime / duration);
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                currentTime += Time.deltaTime;
                yield return null;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
        objects.localPosition = new Vector3(0, -1000, 0);

    }
    IEnumerator FadeOutDiamondLackText(float duration, RectTransform objects)
    {
        Text image = objects.GetComponent<Text>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        if (image != null)
        {
            float currentTime = 0;
            float startAlpha = image.color.a;

            while (currentTime < duration)
            {
                float alpha = Mathf.Lerp(startAlpha, 0, currentTime / duration);
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                currentTime += Time.deltaTime;
                yield return null;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }
   
    public void PetAndColleagueOpen()
    {
        PetAndColleagueUI.localPosition = new Vector3(3727, 250, 0);
        PetAndColleagueUI.SetActive(true);
        
    }
    public void petUIopen()
    {
        petUI.localPosition = new Vector3(3727, 0, 0);
        PetAndColleagueUI.SetActive(false);
    }
    public void petUIClose()
    {
        petUI.localPosition = new Vector3(-3600, 0, 0);
    }
    public void collegurClose()
    {
        PetAndColleagueUI.localPosition = new Vector3(-3600, 0, 0);
    }
}

