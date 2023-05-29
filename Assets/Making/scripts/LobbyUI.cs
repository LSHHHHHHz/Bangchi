
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
    
    GachaPopup gachaPopup;
    GachaResult gachaResult;

    private void Awake()
    {
        InventoryManager.instance.Load();
    }

    
    public void RunGacha(int count)
    {
        if (gachaPopup == null)
        {
            //GachaPopup1 GameObject를 불러와서 prefab 변수에 넣는다.
            var prefab = Resources.Load<GameObject>("GachaPopup1");

            // Instantiate(prefab) : prefab을 Instantiate 한다.
            // Instantiate 함수 원형 : GameObject Instantiate(GameObject original);
            // prefab을 Instantiate한 후 해당 GameObect를 반환하고 이 GameObject에서 GachaPopup GetComponent를 가져와서 gachaPopup에 넣는다.
            gachaPopup = Instantiate(prefab).GetComponent<GachaPopup>();
        }

        GachaResult gachaResult = GachaCalculator.Calculate(itemDb, count);

        // 가챠를 통해 얻은 아이템을 인벤토리에 하나씩 추가
        foreach (var item in gachaResult.items)
        {
            InventoryManager.instance.AddItem(item);
        }

        // 인벤토리에 다 추가했으면 저장
        InventoryManager.instance.Save();

        // 가챠팝업에서 뽑은 아이템들을 보여줘야 하므로 gachaResult를 넘김.
        gachaPopup.Initialize(gachaResult, this.RunGacha);
    }

    class Person
    {
        private string name;

        public Person()
        {
            this.name = "Unknown";
        }

        public Person(string name)
        {
            this.name = name;
        }
    }
}

