using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//배틀 인벤토리 관련
public class BattleInventorySystem : MonoBehaviour
{
    [Header("Scriptable Object Related")]
    public BattleInventorySO attackInventoryData;
    public BattleInventorySO restoreInventoryData;
    public BattleInventorySO equipInventoryData;

    [Header("Battle Inventory List Related")]
    public List<InvenItem> listOfAttackItems;
    public List<InvenItem> listOfRestoreItems;
    public List<InvenItem> listOfEquipItems;

    [Header("Battle Inventory Item Related")]
    public InvenItem battleItemPrefab;

    public RectTransform attackContent;
    public RectTransform restoreContent;
    public RectTransform equipContent;

    [Header("Battle Inventory Size Related")]
    public int battleInventorySize = 10;

    [Header("Battle Inventory Description Related")]
    public TMP_Text battleItemTitle;
    public Image battleItemImage;
    public TMP_Text battleItemQuality;
    public TMP_Text battleItemEffect1;
    public TMP_Text battleItemEffect2;
    public TMP_Text battleItemEffect3;
    public TMP_Text battleItemEffect4;
    public TMP_Text battleItemSpecificity1;
    public TMP_Text battleItemSpecificity2;
    public TMP_Text battleItemSpecificity3;
    public TMP_Text battleItemSpecificity4;

    private int battleAttackItemValue;
    private int battleRestoreItemValue;
    private int battleEquipItemAtkValue;
    private int battleEquipItemDefValue;
    private int battleEquipItemSpdValue;

    [Header("Battle Inventory Quality Gauge Related")]
    public Image qualityGauge;
    public Sprite imageReset;

    [Header("Battle Inventory Equip Item Switch Related")]
    public bool weaponEquip;
    public bool armorEquip;
    public bool ringEquip;

    [Header("Battle Inventory Save & Load Related")]
    public SaveLoadSystem saveLoadSystem;

    [Header("Component Related")]
    public MainMenuSystem mainSys;
    public Player player;

    public event Action<int> AttackItemDescriptionRequested, RestoreItemDescriptionRequested,
        EquipItemDescriptionRequested;

    private void Awake()
    {
        InitializeInventory(battleInventorySize);

        saveLoadSystem.attackItemInventoryLoad();
        saveLoadSystem.restoreItemInventoryLoad();
    }

    //empty 아이템 설치 및 설명창 초기화
    public void InitializeInventory(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            InvenItem Item = Instantiate(battleItemPrefab, Vector3.zero, Quaternion.identity);

            Item.transform.SetParent(attackContent);
            listOfAttackItems.Add(Item);

            Item.OnItemLeftClicked += Item_OnItemLeftClicked;

            AttackItemDescriptionRequested += AttackItemDescriptionOn;
            Item.OnItemRightClicked += Item_OnAttackItemRightClicked;
            Item.OnAttackItemDeleteRequested += Item_OnAttackItemDeleteRequested;
            Item.OnAttackItemCalcelRequested += Item_OnAttackItemCalcelRequested;
        }

        for (int i = 0; i < inventorysize; i++)
        {
            InvenItem Item = Instantiate(battleItemPrefab, Vector3.zero, Quaternion.identity);

            Item.transform.SetParent(restoreContent);
            listOfRestoreItems.Add(Item);

            Item.OnItemLeftClicked += Item_OnItemLeftClicked;

            RestoreItemDescriptionRequested += RestoreItemDescriptionOn;
            Item.OnItemRightClicked += Item_OnRestoreItemRightClicked;
            Item.OnRestoreItemUseRequested += Item_OnRestoreItemUseRequested;
            Item.OnRestoreItemDeleteRequested += Item_OnRestoreItemDeleteRequested;
        }

        for (int i = 0; i < inventorysize; i++)
        {
            InvenItem Item = Instantiate(battleItemPrefab, Vector3.zero, Quaternion.identity);

            Item.transform.SetParent(equipContent);
            listOfEquipItems.Add(Item);

            Item.OnItemLeftClicked += Item_OnItemLeftClicked;

            EquipItemDescriptionRequested += EquipItemDescriptionOn;
            Item.OnItemRightClicked += Item_OnEquipItemRightClicked;
            Item.OnEquipItemRequested += Item_OnEquipItemRequested;
            Item.OnEquipItemDeleteRequested += Item_OnEquipItemDeleteRequested;
        }

        ResetSelection();
    }

    //전체 초기화
    public void ResetSelection()
    {
        ResetDescription();
        DeselectAllItems();
    }

    //설명창 초기화
    public void ResetDescription()
    {
        battleItemTitle.text = "";
        battleItemImage.sprite = imageReset;
        battleItemQuality.text = "";
        battleItemEffect1.text = "";
        battleItemEffect2.text = "";
        battleItemEffect3.text = "";
        battleItemEffect4.text = "";
        battleItemSpecificity1.text = "";
        battleItemSpecificity2.text = "";
        battleItemSpecificity3.text = "";
        battleItemSpecificity4.text = "";
    }

    //하이라이트 선 초기화
    public void DeselectAllItems()
    {
        foreach (InvenItem item in listOfAttackItems)
        {
            item.Deselect();
        }

        foreach (InvenItem item in listOfRestoreItems)
        {
            item.Deselect();
        }

        foreach (InvenItem item in listOfEquipItems)
        {
            item.Deselect();
        }
    }

    //각 인벤토리에 이미지 표시
    void Update()
    {
        BattleInventoryItemDisplay();
    }

    //각 인벤토리 구분
    public void BattleInventoryItemDisplay()
    {
        foreach (var item in attackInventoryData.GetCurrentInventoryState())
        {
            UpdataAttackBattleItemData(item.Key, item.Value.battleItem.itemImage);
        }

        foreach (var item in restoreInventoryData.GetCurrentInventoryState())
        {
            UpdataRestoreBattleItemData(item.Key, item.Value.battleItem.itemImage);
        }

        foreach (var item in equipInventoryData.GetCurrentInventoryState())
        {
            UpdataEquipBattleItemData(item.Key, item.Value.battleItem.itemImage);
        }
    }

    //공격형 아이템 empty 구분
    private void UpdataAttackBattleItemData(int battleItemIndex, Sprite itemImage)
    {
        if (listOfAttackItems.Count > battleItemIndex)
        {
            if (attackInventoryData.GetItemAt(battleItemIndex).IsEmpty)
            {
                listOfAttackItems[battleItemIndex].ResetData();
            }
            else
            {
                listOfAttackItems[battleItemIndex].SetData(itemImage);

                for (int i = battleItemIndex; i < listOfAttackItems.Count; i++)
                {
                    if (attackInventoryData.GetItemAt(i).IsEmpty)
                    {
                        listOfAttackItems[i].ResetData();
                    }
                }
            }
        }
    }

    //회복형 아이템 empty 구분
    private void UpdataRestoreBattleItemData(int battleItemIndex, Sprite itemImage)
    {
        if (listOfRestoreItems.Count > battleItemIndex)
        {
            if (restoreInventoryData.GetItemAt(battleItemIndex).IsEmpty)
            {
                listOfRestoreItems[battleItemIndex].ResetData();
            }
            else
            {
                listOfRestoreItems[battleItemIndex].SetData(itemImage);

                for (int i = battleItemIndex; i < listOfRestoreItems.Count; i++)
                {
                    if (restoreInventoryData.GetItemAt(i).IsEmpty)
                    {
                        listOfRestoreItems[i].ResetData();
                    }
                }
            }
        }
    }

    //장비형 아이템 empty 구분
    private void UpdataEquipBattleItemData(int battleItemIndex, Sprite itemImage)
    {
        if (listOfEquipItems.Count > battleItemIndex)
        {
            if (equipInventoryData.GetItemAt(battleItemIndex).IsEmpty)
            {
                listOfEquipItems[battleItemIndex].ResetData();
            }
            else
            {
                listOfEquipItems[battleItemIndex].SetData(itemImage);

                for (int i = battleItemIndex; i < listOfEquipItems.Count; i++)
                {
                    if (equipInventoryData.GetItemAt(i).IsEmpty)
                    {
                        listOfEquipItems[i].ResetData();
                    }
                }
            }
        }
    }

    //팝업창 버튼 액션
    private void Item_OnItemLeftClicked(InvenItem commonItem)
    {
        if (listOfAttackItems.Contains(commonItem))
        {
            if (commonItem != null)
            {
                int index = listOfAttackItems.IndexOf(commonItem);

                if (index == -1)
                {
                    return;
                }

                DeselectAllItems();
                commonItem.Select();

                AttackItemDescriptionRequested?.Invoke(index);
            }
        }

        if (listOfRestoreItems.Contains(commonItem))
        {
            int index = listOfRestoreItems.IndexOf(commonItem);

            if (index == -1)
            {
                return;
            }

            DeselectAllItems();
            commonItem.Select();

            RestoreItemDescriptionRequested?.Invoke(index);
        }

        if (listOfEquipItems.Contains(commonItem))
        {
            int index = listOfEquipItems.IndexOf(commonItem);

            if (index == -1)
            {
                return;
            }

            DeselectAllItems();
            commonItem.Select();

            EquipItemDescriptionRequested?.Invoke(index);
        }
    }

    //아이템 설명창
    public void AttackItemDescriptionOn(int ItemIndex)
    {
        BattleInventoryStruct attackInventoryStruct = attackInventoryData.GetItemAt(ItemIndex);

        if (attackInventoryStruct.IsEmpty)
        {
            ResetSelection();
            return;
        }

        spendableItemSO attackItem = attackInventoryStruct.battleItem;
        ItemToDecription(attackItem);
    }

    public void RestoreItemDescriptionOn(int ItemIndex)
    {
        BattleInventoryStruct restoreInventoryStruct = restoreInventoryData.GetItemAt(ItemIndex);

        if (restoreInventoryStruct.IsEmpty)
        {
            ResetSelection();
            return;
        }

        spendableItemSO restoreItem = restoreInventoryStruct.battleItem;
        ItemToDecription(restoreItem);
    }

    public void EquipItemDescriptionOn(int ItemIndex)
    {
        BattleInventoryStruct equipInventoryStruct = equipInventoryData.GetItemAt(ItemIndex);

        if (equipInventoryStruct.IsEmpty)
        {
            ResetSelection();
            return;
        }

        spendableItemSO equipItem = equipInventoryStruct.battleItem;
        ItemToDecription(equipItem);
    }

    private void ItemToDecription(spendableItemSO Item)
    {
        battleItemTitle.text = Item.title;
        battleItemImage.sprite = Item.itemImage;
        battleItemQuality.text = Item.quality.ToString();
        battleItemEffect1.text = InventorySystem.GetStringForEffect(Item.effect1);
        battleItemEffect2.text = InventorySystem.GetStringForEffect(Item.effect2);
        battleItemEffect3.text = InventorySystem.GetStringForEffect(Item.effect3);
        battleItemEffect4.text = InventorySystem.GetStringForEffect(Item.effect4);
        battleItemSpecificity1.text = InventorySystem.GetStringForSpecificity(Item.specificity1);
        battleItemSpecificity2.text = InventorySystem.GetStringForSpecificity(Item.specificity2);
        battleItemSpecificity3.text = InventorySystem.GetStringForSpecificity(Item.specificity3);
        battleItemSpecificity4.text = InventorySystem.GetStringForSpecificity(Item.specificity4);

        battleAttackItemValue = (int)Item.DmgValue;
        battleRestoreItemValue = (int)Item.ResValue;
        battleEquipItemAtkValue = (int)Item.atkValue;
        battleEquipItemDefValue = (int)Item.defValue;
        battleEquipItemSpdValue = (int)Item.spdValue;

        itemQualityGauge(Item.quality);
    }

    //아이템 선택 후 오른쪽 버튼 클릭
    private void Item_OnAttackItemRightClicked(InvenItem attackItem)
    {
        if (attackItem.selectOn == true)
        {
            attackItem.attackPopUpDisplay.SetActive(true);
        }
    }

    //아이템 버리기 액션
    private void Item_OnAttackItemDeleteRequested(InvenItem attackItem)
    {
        int itemIndex = listOfAttackItems.IndexOf(attackItem);

        if (itemIndex >= 0 && itemIndex < attackInventoryData.bagSize)
        {
            attackInventoryData.RemoveItem(itemIndex);

            listOfAttackItems[itemIndex].ResetData();
            listOfAttackItems[itemIndex].Deselect();
        }
    }

    private void Item_OnAttackItemCalcelRequested(InvenItem attackItem)
    {
        attackItem.attackPopUpDisplay.SetActive(false);
    }


    private void Item_OnRestoreItemRightClicked(InvenItem restoreItem)
    {
        if (restoreItem.selectOn == true)
        {
            restoreItem.restorePopUpDisplay.SetActive(true);
        }
    }

    //회복형 아이템 사용 액션
    private void Item_OnRestoreItemUseRequested(InvenItem restoreItem)
    {
        EffectSoundManager.instance.PotionHealing();

        player.Restore(battleRestoreItemValue);

        int itemIndex = listOfRestoreItems.IndexOf(restoreItem);

        if (itemIndex >= 0 && itemIndex < restoreInventoryData.bagSize)
        {
            restoreInventoryData.RemoveItem(itemIndex);

            listOfRestoreItems[itemIndex].ResetData();
            listOfRestoreItems[itemIndex].Deselect();
        }
    }

    private void Item_OnRestoreItemDeleteRequested(InvenItem restoreItem)
    {
        int itemIndex = listOfRestoreItems.IndexOf(restoreItem);

        if (itemIndex >= 0 && itemIndex < restoreInventoryData.bagSize)
        {
            restoreInventoryData.RemoveItem(itemIndex);

            listOfRestoreItems[itemIndex].ResetData();
            listOfRestoreItems[itemIndex].Deselect();
        }
    }

    //장비형 아이템을 클릭
    private void Item_OnEquipItemRightClicked(InvenItem equipItem)
    {
        if (equipItem.selectOn == true)
        {
            equipItem.equipPopUpDisplay.SetActive(true);

            //이미 아이템이 장비된 상태였다면
            //중복 장착을 막기 위해 해제하기를 보여주기
            if (battleItemTitle.text == "루비 박힌 완드" && weaponEquip == true)
            {
                equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "해제하기";
                equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
            }

            else if (battleItemTitle.text == "가벼운 갑옷" && armorEquip == true)
            {
                equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "해제하기";
                equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
            }

            else if (battleItemTitle.text == "그나데링" && ringEquip == true)
            {
                equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "해제하기";
                equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    //장비 아이템을 처음 눌렀을 땐 '장착하기', '버리기'
    //장착한 후에는 '버리기'를 setactive(false)를 하여 '해제하기'만 
    private void Item_OnEquipItemRequested(InvenItem equipItem)
    {
        //아이템 장착할 때
        if (battleItemTitle.text == "루비 박힌 완드" && weaponEquip == false)
        {
            EffectSoundManager.instance.ItemEquip();
            weaponEquip = true;
            player.Equip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "해제하기";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (battleItemTitle.text == "가벼운 갑옷" && armorEquip == false)
        {
            EffectSoundManager.instance.ItemEquip();
            armorEquip = true;
            player.Equip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "해제하기";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (battleItemTitle.text == "그나데링" && ringEquip == false)
        {
            EffectSoundManager.instance.ItemEquip();
            ringEquip = true;
            player.Equip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "해제하기";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
        }

        //아이템 해제할 때
        else if (battleItemTitle.text == "루비 박힌 완드" && weaponEquip == true)
        {
            EffectSoundManager.instance.ItemUnequip();
            weaponEquip = false;
            player.Unequip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "장착하기";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (battleItemTitle.text == "가벼운 갑옷" && armorEquip == true)
        {
            EffectSoundManager.instance.ItemUnequip();
            armorEquip = false;
            player.Unequip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "장착하기";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (battleItemTitle.text == "그나데링" && ringEquip == true)
        {
            EffectSoundManager.instance.ItemUnequip();
            ringEquip = false;
            player.Unequip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "장착하기";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
        }

        saveLoadSystem.PlayerStatSave();
        saveLoadSystem.weaponEquipSwitchSave();
    }

    //장비형 아이템 버리기 액션
    private void Item_OnEquipItemDeleteRequested(InvenItem equipItem)
    {
        int itemIndex = listOfEquipItems.IndexOf(equipItem);

        if (itemIndex >= 0 && itemIndex < equipInventoryData.bagSize)
        {
            equipInventoryData.RemoveItem(itemIndex);

            listOfEquipItems[itemIndex].ResetData();
            listOfEquipItems[itemIndex].Deselect();
        }
    }

    //인벤토리 숨기기 버튼
    public void InventoryHide()
    {
        if (mainSys.menuButton != null && mainSys.activeButton != null)
        {
            foreach (GameObject go in mainSys.menuButton)
            {
                go.SetActive(true);
            }

            EffectSoundManager.instance.MainmenuButtonBack();
            gameObject.SetActive(false);
            ResetSelection();
        }
    }

    //아이템 선택 초기화
    public void DeselectAll()
    {
        foreach (InvenItem item in listOfAttackItems)
        {
            item.Deselect();
        }

        foreach (InvenItem item in listOfRestoreItems)
        {
            item.Deselect();
        }

        foreach (InvenItem item in listOfEquipItems)
        {
            item.Deselect();
        }
    }

    //아이템 별 게이지 모습 변경
    public void itemQualityGauge(float quality)
    {
        qualityGauge.fillAmount = quality / 200;
    }
}