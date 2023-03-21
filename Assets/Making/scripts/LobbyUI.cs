using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    public ItemDB itemDb;
    public InventoryManager inventoryManager;

    GachaPopup gachaPopup;

    private void Awake()
    {
        inventoryManager.Load();
    }

    public void RunGacha()
    {
        if (gachaPopup == null)
        {
            var prefab = Resources.Load<GameObject>("GachaPopup");
            gachaPopup = Instantiate(prefab).GetComponent<GachaPopup>();
        }

        GachaResult gachaResult = GachaCalculator.Calculate(itemDb, 10);

        // 가챠를 통해 얻은 아이템을 인벤토리에 하나씩 추가
        foreach (var item in gachaResult.items)
        {
            inventoryManager.AddItem(item);
        }

        // 인벤토리에 다 추가했으면 저장
        inventoryManager.Save();

        // 가챠팝업에서 뽑은 아이템들을 보여줘야 하므로 gachaResult를 넘김.
        gachaPopup.Initialize(gachaResult, RunGacha);
    }

    public void OpenInventory()
    {
        // <타입명> : C#의 Generic - 원하는 타입을 입력하고, 그 입력받은 클래스나 함수에서 그 타입을 활용할 수 있음.
        // List : Generic 클래스
        //List<int> intList = new List<int>();
        //List<GameObject> gameObjectList = new List<GameObject>();

        //Resources.Load<타입> : Generic 메서드

        //var skillImage = Resources.Load<Sprite>("ItemIcon/001_Skill1");

        //----------------------------------------------------


       // var prefab = Resources.Load<GameObject>("SkillInventoryPopup"); //프리펩 불러올 때 Gmaeobject로 불러옴

       // var skillInventoryPopup = Instantiate(prefab).GetComponent<SkillInventoryPopup>();
      //  skillInventoryPopup.Initialize(inventoryManager);

        //SkillInventoryPopup 이 불러와지고 그 안에 스크립트가 어떻게 들어가는지
    }
}

