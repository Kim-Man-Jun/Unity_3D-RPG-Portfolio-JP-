using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//��Ʋ �κ��丮 ����
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

    //empty ������ ��ġ �� ����â �ʱ�ȭ
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

    //��ü �ʱ�ȭ
    public void ResetSelection()
    {
        ResetDescription();
        DeselectAllItems();
    }

    //����â �ʱ�ȭ
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

    //���̶���Ʈ �� �ʱ�ȭ
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

    //�� �κ��丮�� �̹��� ǥ��
    void Update()
    {
        BattleInventoryItemDisplay();
    }

    //�� �κ��丮 ����
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

    //������ ������ empty ����
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

    //ȸ���� ������ empty ����
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

    //����� ������ empty ����
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

    //�˾�â ��ư �׼�
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

    //������ ����â
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

    //������ ���� �� ������ ��ư Ŭ��
    private void Item_OnAttackItemRightClicked(InvenItem attackItem)
    {
        if (attackItem.selectOn == true)
        {
            attackItem.attackPopUpDisplay.SetActive(true);
        }
    }

    //������ ������ �׼�
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

    //ȸ���� ������ ��� �׼�
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

    //����� �������� Ŭ��
    private void Item_OnEquipItemRightClicked(InvenItem equipItem)
    {
        if (equipItem.selectOn == true)
        {
            equipItem.equipPopUpDisplay.SetActive(true);

            //�̹� �������� ���� ���¿��ٸ�
            //�ߺ� ������ ���� ���� �����ϱ⸦ �����ֱ�
            if (battleItemTitle.text == "��� ���� �ϵ�" && weaponEquip == true)
            {
                equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "�����ϱ�";
                equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
            }

            else if (battleItemTitle.text == "������ ����" && armorEquip == true)
            {
                equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "�����ϱ�";
                equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
            }

            else if (battleItemTitle.text == "�׳�����" && ringEquip == true)
            {
                equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "�����ϱ�";
                equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    //��� �������� ó�� ������ �� '�����ϱ�', '������'
    //������ �Ŀ��� '������'�� setactive(false)�� �Ͽ� '�����ϱ�'�� 
    private void Item_OnEquipItemRequested(InvenItem equipItem)
    {
        //������ ������ ��
        if (battleItemTitle.text == "��� ���� �ϵ�" && weaponEquip == false)
        {
            EffectSoundManager.instance.ItemEquip();
            weaponEquip = true;
            player.Equip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "�����ϱ�";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (battleItemTitle.text == "������ ����" && armorEquip == false)
        {
            EffectSoundManager.instance.ItemEquip();
            armorEquip = true;
            player.Equip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "�����ϱ�";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (battleItemTitle.text == "�׳�����" && ringEquip == false)
        {
            EffectSoundManager.instance.ItemEquip();
            ringEquip = true;
            player.Equip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "�����ϱ�";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
        }

        //������ ������ ��
        else if (battleItemTitle.text == "��� ���� �ϵ�" && weaponEquip == true)
        {
            EffectSoundManager.instance.ItemUnequip();
            weaponEquip = false;
            player.Unequip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "�����ϱ�";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (battleItemTitle.text == "������ ����" && armorEquip == true)
        {
            EffectSoundManager.instance.ItemUnequip();
            armorEquip = false;
            player.Unequip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "�����ϱ�";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (battleItemTitle.text == "�׳�����" && ringEquip == true)
        {
            EffectSoundManager.instance.ItemUnequip();
            ringEquip = false;
            player.Unequip(battleEquipItemAtkValue, battleEquipItemDefValue, battleEquipItemSpdValue);
            equipItem.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = "�����ϱ�";
            equipItem.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
        }

        saveLoadSystem.PlayerStatSave();
        saveLoadSystem.weaponEquipSwitchSave();
    }

    //����� ������ ������ �׼�
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

    //�κ��丮 ����� ��ư
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

    //������ ���� �ʱ�ȭ
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

    //������ �� ������ ��� ����
    public void itemQualityGauge(float quality)
    {
        qualityGauge.fillAmount = quality / 200;
    }
}