using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Inventory", menuName = "Inventory/new Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    public List<InventoryStruct> inventoryItems;

    //인벤토리 크기
    [field: SerializeField]
    public int bagSize { get; set; } = 50;

    //인벤토리 초기화, inventoryBlank 리스트를 bagSize에 맞게 생성
    public void Initialize()
    {
        inventoryItems = new List<InventoryStruct>();

        for (int i = 0; i < bagSize; i++)
        {
            inventoryItems.Add(InventoryStruct.GetEmptyItem());
        }
    }

    //비어있는 아이템 슬롯에 해당 아이템을 추가
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

    //인벤토리 내에서 아이템 삭제
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

    //인벤토리 현재 상태 표시
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

    //아이템 인덱스 번호 반환
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

    //GetEmptyItem 메서드를 호출하면 InventoryStruct 인스턴스를 반환
    //새로운 아이켐을 생성할 때마다 비어 있는 상태의 아이템을 얻을 수 있음
    public static InventoryStruct GetEmptyItem() => new InventoryStruct
    {
        baseItem = null
    };
}
