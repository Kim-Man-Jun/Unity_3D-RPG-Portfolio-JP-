using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class inBattleInventory : MonoBehaviour
{
    [Header("BattleInventory SO Related")]
    public BattleInventorySO attackItemInventorySO;
    public BattleInventorySO restoreItemInventorySO;

    [Header("BattleInventory Blank Prefab Related")]
    public InvenItem inBattleItem;

    [Header("BattleInventory List Related")]
    public List<InvenItem> attackItemList;
    public List<InvenItem> restoreItemList;

    [Header("BattleInventory Setting Transform Related")]
    public RectTransform attackContent;
    public RectTransform restoreContent;

    [Header("BattleInventory Size")]
    public int inBattleInventorySize = 10;

    [Header("BattleInventory Description Related")]
    public TMP_Text inBattleItemTitle;
    public TMP_Text inBattleItemTarget;
    public TMP_Text inBattleItemDmgResTextChange;
    public TMP_Text inBattleItemDmgResValue;

    [Header("GameObject Related")]
    public GameObject ItemActionButton;

    [Header("Atk, ResItem Switch Related")]
    public bool atkSingleItemUse = false;
    public bool atkGroupItemUse = false;
    public bool resItemUse = false;

    public event Action<int> inAttackItemDescription, inRestoreItemDescription;
    public event Action<inBattleInventory> inBattleItemActionStart;

    public int attackItemIndex;
    public int restoreItemIndex;

    private void Awake()
    {
        for (int i = 0; i < inBattleInventorySize; i++)
        {
            InvenItem Item = Instantiate(inBattleItem, Vector3.zero, Quaternion.identity);

            Item.transform.SetParent(attackContent);
            attackItemList.Add(Item);

            Item.OnItemLeftClicked += Item_OninBattleItemLeftClick;
            inAttackItemDescription += InBattleAttackItemDescription;
        }

        for (int i = 0; i < inBattleInventorySize; i++)
        {
            InvenItem Item = Instantiate(inBattleItem, Vector3.zero, Quaternion.identity);

            Item.transform.SetParent(restoreContent);
            restoreItemList.Add(Item);

            Item.OnItemLeftClicked += Item_OninBattleItemLeftClick;
            inRestoreItemDescription += InBattleRestoreItemDescription;
        }

        ItemActionButton.SetActive(false);
    }

    public void ResetSelection()
    {
        ResetDescription();
        DeselectAllItems();
    }

    //설명창 초기화
    public void ResetDescription()
    {
        inBattleItemTitle.text = "";
        inBattleItemTarget.text = "";
        inBattleItemDmgResTextChange.text = "피해량 : ";
        inBattleItemDmgResValue.text = "";
    }

    //하이라이트 선 초기화
    public void DeselectAllItems()
    {
        foreach (InvenItem item in attackItemList)
        {
            item.Deselect();
        }

        foreach (InvenItem item in restoreItemList)
        {
            item.Deselect();
        }
    }

    void Update()
    {
        BattleInventoryItemDisplay();

        if (attackItemInventorySO.battleInventoryItems[0].battleItem == null)
        {
            attackItemList[0].ResetData();
        }

        if (restoreItemInventorySO.battleInventoryItems[0].battleItem == null)
        {
            restoreItemList[0].ResetData();
        }
    }

    public void BattleInventoryItemDisplay()
    {
        //공격형 아이템 인벤토리
        foreach (var item in attackItemInventorySO.GetCurrentInventoryState())
        {
            UpdataAttackBattleItemData(item.Key, item.Value.battleItem.itemImage);
        }

        //회복형 아이템 인벤토리
        foreach (var item in restoreItemInventorySO.GetCurrentInventoryState())
        {
            UpdataRestoreBattleItemData(item.Key, item.Value.battleItem.itemImage);
        }
    }

    private void UpdataAttackBattleItemData(int inBattleItemIndex, Sprite itemImage)
    {
        if (attackItemList.Count > inBattleItemIndex)
        {
            if (attackItemInventorySO.GetItemAt(inBattleItemIndex).IsEmpty)
            {
                attackItemList[inBattleItemIndex].ResetData();
            }
            else
            {
                attackItemList[inBattleItemIndex].SetData(itemImage);

                for (int i = inBattleItemIndex; i < attackItemList.Count; i++)
                {
                    if (attackItemInventorySO.GetItemAt(i).IsEmpty)
                    {
                        attackItemList[i].ResetData();
                    }
                }
            }
        }
    }

    private void UpdataRestoreBattleItemData(int inBattleItemIndex, Sprite itemImage)
    {
        if (restoreItemList.Count > inBattleItemIndex)
        {
            if (restoreItemInventorySO.GetItemAt(inBattleItemIndex).IsEmpty)
            {
                restoreItemList[inBattleItemIndex].ResetData();
            }
            else
            {
                restoreItemList[inBattleItemIndex].SetData(itemImage);

                for (int i = inBattleItemIndex; i < restoreItemList.Count; i++)
                {
                    if (restoreItemInventorySO.GetItemAt(i).IsEmpty)
                    {
                        restoreItemList[i].ResetData();
                    }
                }
            }
        }
    }

    public void Item_OninBattleItemLeftClick(InvenItem inBattleItem)
    {
        if (attackItemList.Contains(inBattleItem))
        {
            attackItemIndex = attackItemList.IndexOf(inBattleItem);

            if (attackItemIndex == -1)
            {
                return;
            }

            DeselectAllItems();
            inBattleItem.Select();

            inAttackItemDescription?.Invoke(attackItemIndex);

            EffectSoundManager.instance.SelectButton();
        }

        if (restoreItemList.Contains(inBattleItem))
        {
            restoreItemIndex = restoreItemList.IndexOf(inBattleItem);

            if (restoreItemIndex == -1)
            {
                return;
            }

            DeselectAllItems();
            inBattleItem.Select();

            inRestoreItemDescription?.Invoke(restoreItemIndex);

            EffectSoundManager.instance.SelectButton();
        }
    }

    public void InBattleAttackItemDescription(int ItemIndex)
    {
        BattleInventoryStruct inBattleAttackInventory = attackItemInventorySO.GetItemAt(ItemIndex);

        if (inBattleAttackInventory.IsEmpty)
        {
            ResetSelection();
            return;
        }

        ItemActionButton.SetActive(true);

        spendableItemSO attackItem = inBattleAttackInventory.battleItem;
        inBattleItemDescription(attackItem);
    }

    public void InBattleRestoreItemDescription(int ItemIndex)
    {
        BattleInventoryStruct inBattleRestoreInventory = restoreItemInventorySO.GetItemAt(ItemIndex);

        if (inBattleRestoreInventory.IsEmpty)
        {
            ResetSelection();
            return;
        }

        ItemActionButton.SetActive(true);

        spendableItemSO restoreItem = inBattleRestoreInventory.battleItem;
        inBattleItemDescription(restoreItem);
    }

    private void inBattleItemDescription(spendableItemSO item)
    {
        inBattleItemTitle.text = item.title;
        inBattleItemTarget.text = InventorySystem.GetstromgForTarget((item.target));

        switch (inBattleItemTarget.text)
        {
            case "적 단일":
            case "적 전체":
                inBattleItemDmgResTextChange.text = "피해량 : ";
                inBattleItemDmgResValue.text = item.DmgValue.ToString();
                return;

            case "아군":
                inBattleItemDmgResTextChange.text = "회복량 : ";
                inBattleItemDmgResValue.text = item.ResValue.ToString();
                return;
        }
    }

    //actionSystem으로 전달
    public void ItemActionStart()
    {
        if (inBattleItemTarget.text == "적 단일")
        {
            atkSingleItemUse = true;
            atkGroupItemUse = false;
            resItemUse = false;

            attackItemInventorySO.RemoveItem(attackItemIndex);
        }
        else if (inBattleItemTarget.text == "적 전체")
        {
            atkSingleItemUse = false;
            atkGroupItemUse = true;
            resItemUse = false;

            attackItemInventorySO.RemoveItem(attackItemIndex);
        }
        else if (inBattleItemTarget.text == "아군")
        {
            atkSingleItemUse = false;
            atkGroupItemUse = false;
            resItemUse = true;

            restoreItemInventorySO.RemoveItem(restoreItemIndex);
        }

        EffectSoundManager.instance.ItemEquip();

        inBattleItemActionStart?.Invoke(this);

        DeselectAllItems();
        ResetDescription();
        ItemActionButton.SetActive(false);
        gameObject.SetActive(false);

        attackItemIndex = -1;
        restoreItemIndex = -1;
    }

    public void ItemActionQuit()
    {
        EffectSoundManager.instance.ItemUnequip();

        DeselectAllItems();
        ResetDescription();
        ItemActionButton.SetActive(false);
    }

    public void ItemPanelQuitButton()
    {
        DeselectAllItems();
        ResetDescription();
        ItemActionButton.SetActive(false);
        gameObject.SetActive(false);
    }
}
