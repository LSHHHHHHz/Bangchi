
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
            //GachaPopup1 GameObject�� �ҷ��ͼ� prefab ������ �ִ´�.
            var prefab = Resources.Load<GameObject>("GachaPopup1");

            // Instantiate(prefab) : prefab�� Instantiate �Ѵ�.
            // Instantiate �Լ� ���� : GameObject Instantiate(GameObject original);
            // prefab�� Instantiate�� �� �ش� GameObect�� ��ȯ�ϰ� �� GameObject���� GachaPopup GetComponent�� �����ͼ� gachaPopup�� �ִ´�.
            gachaPopup = Instantiate(prefab).GetComponent<GachaPopup>();
        }

        GachaResult gachaResult = GachaCalculator.Calculate(itemDb, count);

        // ��í�� ���� ���� �������� �κ��丮�� �ϳ��� �߰�
        foreach (var item in gachaResult.items)
        {
            InventoryManager.instance.AddItem(item);
        }

        // �κ��丮�� �� �߰������� ����
        InventoryManager.instance.Save();

        // ��í�˾����� ���� �����۵��� ������� �ϹǷ� gachaResult�� �ѱ�.
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

