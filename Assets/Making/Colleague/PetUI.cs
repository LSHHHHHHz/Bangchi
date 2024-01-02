using Assets.Battle;
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

    public static PetUI instance;
    public RectTransform petUI;
    public GameObject GridUI;

    public GameObject BuyPetSlot;
    public RectTransform petInventorySizeUpButton;
    public Text petInventorySizeStatus;
    public RectTransform diamondLack;
    public RectTransform diamondLackText;

    public PetType type;

    public Text[] PetPriceText;
    public int[] PetPrice;
    PetPopup petPopup;
    public PetDB petDB;
    public Sprite lockedSprite;

    public RectTransform isBuy;

    public Grid gird;
    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        gridSizeUP();
    }
    public void Update()
    {
        petInventorySizeStatus.text = PetInventoryManager.Instance.myPets.Count + " / " + PetInventoryManager.Instance.maxaccumulatePetsCount + "";
    }

    //펫이 늘어날 때마다 그리드 사이즈 크게하려고 만듬
    //gridSizeUP 때문에 PetInventoryManager에서 Load를 awake로 바꿈 문제없는지
    public void gridSizeUP()
    {
        //펫인벤토리의 개수가 초과했을 때 160을 증가시키고 4개 증가 때마다 160 증가
        //기본 디폴트값 800
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
        //☆예상 값보다 작을때만 실행하게 바꿈
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
                //PetInventoryManager.Instance.AddPet(pet); <--펫 인벤토리에서는 딱히 필요없음 count가 필요 없기 때문
                PetInventoryManager.Instance.AddPet(pet);

            }
            PetInventoryManager.Instance.Save();

            petPopup.Initialize(petGachaResult, this.RunPet);
            gridSizeUP();
        }
        else
        {
            return;
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
        if (Player.instance.Diamond >= 3000)
        {
            Player.instance.Diamond -= 3000;
            PetInventoryManager.Instance.maxaccumulatePetsCount += 5;
            PetInventoryManager.Instance.petCount+= 5;
            PetInventoryManager.Instance.Save();
        }
        else
        {
            StartCoroutine(FadeOutDiamondLackRectTransform(1, diamondLack));
            StartCoroutine(FadeOutDiamondLackText(1, diamondLackText));
        }
    }
    IEnumerator FadeOutDiamondLackRectTransform(float duration, RectTransform objects)
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

    public void petUIopen()
    {
        //★이거 있으니 에러 발생, gridSizeUp을 instance위에 놓으면 에러 아래에 놓으면 에러X
       // UIManager.instance.OnBottomButtonClicked();
        petUI.localPosition = new Vector3(3605, 0, 0);
    }
    public void petUIClose()
    {
        petUI.localPosition = new Vector3(-3600, 0, 0);
    }
}

