using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Inventory", menuName = "Inventory/new BattleInventory")]
public class BattleInventorySO : ScriptableObject
{
    public List<BattleInventoryStruct> battleInventoryItems;

    [field: SerializeField]
    public int bagSize { get; set; } = 30;

    public void Initialized()
    {
        battleInventoryItems = new List<BattleInventoryStruct>();

        for (int i = 0; i < bagSize; i++)
        {
            battleInventoryItems.Add(BattleInventoryStruct.GetEmptyItem());
        }
    }

    public void AddItem(spendableItemSO item)
    {
        for (int i = 0; i < battleInventoryItems.Count; i++)
        {
            if (battleInventoryItems[i].IsEmpty)
            {
                battleInventoryItems[i] = new BattleInventoryStruct
                {
                    battleItem = item
                };
                break;
            }
        }
    }

    public void RemoveItem(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < bagSize)
        {
            battleInventoryItems[itemIndex] = BattleInventoryStruct.GetEmptyItem();

            for (int i = itemIndex; i < bagSize; i++)
            {
                if (!battleInventoryItems[i].IsEmpty)
                {
                    battleInventoryItems[i - 1] = battleInventoryItems[i];
                    battleInventoryItems[i] = BattleInventoryStruct.GetEmptyItem();
                }
            }
        }
    }

    public Dictionary<int, BattleInventoryStruct> GetCurrentInventoryState()
    {
        Dictionary<int, BattleInventoryStruct> returnValue = new Dictionary<int, BattleInventoryStruct>();

        for (int i = 0; i < battleInventoryItems.Count; i++)
        {
            if (battleInventoryItems[i].IsEmpty)
            {
                continue;
            }

            returnValue[i] = battleInventoryItems[i];
        }
        return returnValue;
    }

    public BattleInventoryStruct GetItemAt(int itemIndex)
    {
        return battleInventoryItems[itemIndex];
    }
}

[Serializable]

public struct BattleInventoryStruct
{
    public spendableItemSO battleItem;

    public bool IsEmpty => battleItem == null;

    public static BattleInventoryStruct GetEmptyItem() => new BattleInventoryStruct
    {
        battleItem = null
    };
}
