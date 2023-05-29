using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ������ �����ִ� UI.
public class Equips : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform weaponselectuiGroup;
    public RectTransform talkText;
    public Player enterPlayer;

    public RectTransform weaponSlotParent;

    private ItemSlot[] weaponSlots;


    //�θ� ���ӿ�����Ʈ ����� �Ʒ� 16�� �ڽ� ������Ʈ ����
    //
    private void Awake()
    {
        // ���� ������ 16���� �ִµ�, �װ��� �θ� GameObject�� weaponSlotParent�̴�.
        // �ݴ�� ���ϸ� weaponSlotParent�� �ڽĵ��� ������ ���� �װ͵��� ���� �����̴�.

        List<ItemSlot> childList = new();
        for (int i = 0; i < weaponSlotParent.childCount; ++i) // weaponSlotParent�� �ڽ� ������ ��������, �� ������ŭ for�� �ݺ�
        {
            ItemSlot child = weaponSlotParent.GetChild(i).GetComponent<ItemSlot>();
            childList.Add(child); // �ڽ��� childList�� �ӽ÷� �־�д�.
        }

        weaponSlots = childList.ToArray(); //�ڽĵ��� ����ִ� childList�� �迭 ��ȯ�� ��ȯ�Ѵ�.

    }

    private void Start() //�� Start�� �ִ°���?
    {
        // 4-16
        // �κ��丮�� �ٲ���� �� UI�� ������ �� �ֵ��� �̺�Ʈ �ݹ�(Ư�� ������ ȣ��Ǵ� �Լ�) ���.
        InventoryManager.instance.OnInventoryChanged += OnInventoryChangedCallback;
        // 4-16


        // �κ��丮�� �ε�� ���� �����͸� �����ؾ� �ؼ� Awake�� �ƴ� Start���ٰ� �Լ� �߰�
        SetData();
    }

    // 4-16
    private void OnInventoryChangedCallback()
    {
        SetData();
    }

    public void EnterEquip()
    {
        uiGroup.anchoredPosition = new Vector3(0, 1670, 0);
        Debug.Log("Ŭ��");
    }

    public void ExitEquip()
    {
        uiGroup.anchoredPosition = new Vector3(2049, -1607, 0);
    }

    public void EnterWeaponSelect()
    {
        weaponselectuiGroup.anchoredPosition = new Vector3(0, 625, 0);
    }

    public void ExitWeaponSelect()
    {
        weaponselectuiGroup.anchoredPosition = new Vector3(-1687, 625, 0);
    }

    // �κ��丮�� �ִ� �����۵��� UI�� �����ϴ� ���.
    public void SetData()
    {
        // ���� ���� �ִ� �����۵��� foreach������ ��ȸ�Ѵ�.
        // ���� ���� �ִ� �������� InventoryManager.instance.myItems�� ����ִ�.
        foreach (ItemInstance item in InventoryManager.instance.myItems)
        {
            // number : 2 /  weaponSlots �迭�� 0~15���� ����ִ�. number2�� �ش��ϴ� weaponSlots ���� 1�̴�.(0���� �����ϴϱ� -1�� ����).
            // number��  : 1���� ����, �迭�� 0���� �����ϱ� ������ -1�� ����� ���� ����.
            int number = item.itemInfo.Number;
            ItemSlot slot = weaponSlots[number - 1];
            slot.SetData(item);
        }
    }
}
