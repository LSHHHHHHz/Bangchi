using Assets.CSharp;
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

    private void Awake()
    {
        InventoryManager.instance.Load();
    }

    public void RunGacha()
    {
        if (gachaPopup == null)
        {
            var prefab = Resources.Load<GameObject>("GachaPopup");
            gachaPopup = Instantiate(prefab).GetComponent<GachaPopup>();
        }

        GachaResult gachaResult = GachaCalculator.Calculate(itemDb, 10);

        // ��í�� ���� ���� �������� �κ��丮�� �ϳ��� �߰�
        foreach (var item in gachaResult.items)
        {
            InventoryManager.instance.AddItem(item);
        }

        // �κ��丮�� �� �߰������� ����
        InventoryManager.instance.Save();

        // ��í�˾����� ���� �����۵��� ������� �ϹǷ� gachaResult�� �ѱ�.
        gachaPopup.Initialize(gachaResult, this.RunGacha, StaticMethodTest.MyStaticMethod);
    }

    public void OpenInventory()
    {
        // <Ÿ�Ը�> : C#�� Generic - ���ϴ� Ÿ���� �Է��ϰ�, �� �Է¹��� Ŭ������ �Լ����� �� Ÿ���� Ȱ���� �� ����.
        // List : Generic Ŭ����
        //List<int> intList = new List<int>();
        //List<GameObject> gameObjectList = new List<GameObject>();

        //Resources.Load<Ÿ��> : Generic �޼���

        //var skillImage = Resources.Load<Sprite>("ItemIcon/001_Skill1");

        //----------------------------------------------------


       // var prefab = Resources.Load<GameObject>("SkillInventoryPopup"); //������ �ҷ��� �� Gmaeobject�� �ҷ���

       // var skillInventoryPopup = Instantiate(prefab).GetComponent<SkillInventoryPopup>();
      //  skillInventoryPopup.Initialize(inventoryManager);

        //SkillInventoryPopup �� �ҷ������� �� �ȿ� ��ũ��Ʈ�� ��� ������
    }
}

