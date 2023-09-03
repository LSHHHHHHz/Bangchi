using Assets.Battle;
using Assets.Item1;
using Assets.Making.scripts;
using Assets.Making.Stage;
using System;
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

    public PetType type;

    public Text[] PetPriceText;
    public int[] PetPrice;
    PetPopup petPopup;
    public PetDB petDB;
    public Sprite lockedSprite;


    public Grid gird;
    public void Awake()
    {
        instance = this;
    }
    public void RunPet(int count)
    {
        if (PetInventoryManager.Instance.accumulatePets.Count < PetInventoryManager.Instance.maxaccumulatePetsCount)
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
                PetInventoryManager.Instance.accumulatePet(pet);

            }
            PetInventoryManager.Instance.Save();

            petPopup.Initialize(petGachaResult, this.RunPet);
        }
        else
        {
            return;
        }
    }
    public void petUIopen()
    {
        UIManager.instance.OnBottomButtonClicked();
        petUI.localPosition = new Vector3(-900, 0, 0);
    }
    public void petUIClose()
    {
        petUI.localPosition = new Vector3(-3600, 0, 0);
    }
}

