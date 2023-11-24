using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Inventory", menuName = "Inventory/new Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    public List<InventoryStruct> inventoryItems;

    //�κ��丮 ũ��
    [field: SerializeField]
    public int bagSize { get; set; } = 50;

    //�κ��丮 �ʱ�ȭ, inventoryBlank ����Ʈ�� bagSize�� �°� ����
    public void Initialize()
    {
        inventoryItems = new List<InventoryStruct>();

        for (int i = 0; i < bagSize; i++)
        {
            inventoryItems.Add(InventoryStruct.GetEmptyItem());
        }
    }

    //����ִ� ������ ���Կ� �ش� �������� �߰�
    public void AddItem(basicMaterialItemSO item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = new InventoryStruct
                {
                    baseItem = item
                };
                break;
            }
        }
    }

    //�κ��丮 ������ ������ ����
    public void RemoveItem(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < bagSize)
        {
            inventoryItems[itemIndex] = InventoryStruct.GetEmptyItem();

            for (int i = itemIndex; i < bagSize; i++)
            {
                if (!inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i - 1] = inventoryItems[i];
                    inventoryItems[i] = InventoryStruct.GetEmptyItem();
                }
            }
        }
    }

    //�κ��丮 ���� ���� ǥ��
    public Dictionary<int, InventoryStruct> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryStruct> returnValue = new Dictionary<int, InventoryStruct>();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                continue;
            }

            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    //������ �ε��� ��ȣ ��ȯ
    public InventoryStruct GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }
}

[Serializable]

public struct InventoryStruct
{
    public basicMaterialItemSO baseItem;

    public bool IsEmpty => baseItem == null;

    //GetEmptyItem �޼��带 ȣ���ϸ� InventoryStruct �ν��Ͻ��� ��ȯ
    //���ο� �������� ������ ������ ��� �ִ� ������ �������� ���� �� ����
    public static InventoryStruct GetEmptyItem() => new InventoryStruct
    {
        baseItem = null
    };
}
