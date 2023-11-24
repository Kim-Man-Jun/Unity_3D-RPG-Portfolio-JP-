using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static basicMaterialItemSO;
using static spendableItemSO;

public class InventorySystem : MonoBehaviour
{
    public List<GameObject> menuBtn;
    public List<GameObject> activeBtn;

    //�κ��丮 ��ĭ ������
    public InvenItem itemPrefab;

    public RectTransform content;

    public InventorySO inventoryData;

    public List<InvenItem> listOfItems;
    public ItemDescription itemDescription;

    public int inventorySize = 10;

    public Image qualityGauge;

    public AlchemyInven alchemyInven;

    public TMP_Dropdown inventoryFilter;
    public basicMaterialItemSO.Element currentFilter = basicMaterialItemSO.Element.none;

    public event Action<int> DescriptionRequested, ItemActionRequested;

    private void Awake()
    {
        GameObject menu = GameObject.Find("Canvas_Menu");
        menuBtn = menu.GetComponent<MainMenuSystem>().menuButton;
        activeBtn = menu.GetComponent<MainMenuSystem>().activeButton;

        InitializeInventory(inventoryData.bagSize);

        this.DescriptionRequested += HandleDescriptionRequest;
    }

    //Main Inventory ������ ������
    public void ActionPopUp_OnItemDeleteRequested(InvenItem item)
    {
        int itemIndex = listOfItems.IndexOf(item);

        if (itemIndex >= 0 && itemIndex < inventoryData.bagSize)
        {
            inventoryData.RemoveItem(itemIndex);

            listOfItems[itemIndex].ResetData();
            listOfItems[itemIndex].Deselect();
        }
    }

    public void InitializeInventory(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            InvenItem Item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

            Item.transform.SetParent(content);
            listOfItems.Add(Item);

            Item.OnItemLeftClicked += itemSelection;
            Item.OnItemRightClicked += itemShowAction;
            Item.OnItemDeleteRequested += ActionPopUp_OnItemDeleteRequested;
        }
    }

    //������ ���� ǥ��
    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryStruct inventoryStruct = inventoryData.GetItemAt(itemIndex);

        if (inventoryStruct.IsEmpty)
        {
            ResetSelection();
            return;
        }
        basicMaterialItemSO basicItem = inventoryStruct.baseItem;

        UpdateDescription(itemIndex, basicItem.title, basicItem.itemImage, basicItem.quality,
            basicItem.effect1, basicItem.effect2, basicItem.effect3, basicItem.effect4,
            basicItem.specificity1, basicItem.specificity2, basicItem.specificity3, basicItem.specificity4,
            basicItem.element1, basicItem.element2, basicItem.element3, basicItem.element4);
    }

    void Start()
    {
        ResetSelection();

        //���� ���
        inventoryFilter.onValueChanged.AddListener(FilterElement);
    }

    private void FilterElement(int elementIndex)
    {
        currentFilter = (basicMaterialItemSO.Element)elementIndex;
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    public void DeselectAllItems()
    {
        foreach (InvenItem item in listOfItems)
        {
            item.Deselect();
        }
    }

    //���콺 ���� Ŭ���� ��
    public void itemSelection(InvenItem item)
    {
        int index = listOfItems.IndexOf(item);

        if (index == -1)
        {
            return;
        }

        item.selectOn = true;

        DescriptionRequested?.Invoke(index);
    }

    //���콺 ������ Ŭ���� ��
    public void itemShowAction(InvenItem item)
    {
        if (item.selectOn == true)
        {
            item.mainPopUpDisplay.SetActive(true);
        }
    }

    void Update()
    {
        inventoryItemDisplay();

        if (inventoryData.inventoryItems[0].baseItem == null)
        {
            listOfItems[0].ResetData();
        }
    }

    //�κ��丮 �����ֱ�
    public void inventoryItemDisplay()
    {
        foreach (var item in inventoryData.GetCurrentInventoryState())
        {
            bool shouldShowItem = currentFilter == basicMaterialItemSO.Element.none ||
                (item.Value.baseItem.element1 == currentFilter ||
                item.Value.baseItem.element2 == currentFilter ||
                item.Value.baseItem.element3 == currentFilter ||
                item.Value.baseItem.element4 == currentFilter);
            {
                UpdataData(item.Key, item.Value.baseItem.itemImage);
            }
            listOfItems[item.Key].gameObject.SetActive(shouldShowItem);
        }
    }

    public void UpdataData(int itemIndex, Sprite itemImage)
    {
        if (listOfItems.Count > itemIndex)
        {
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
            {
                listOfItems[itemIndex].ResetData();
            }
            else
            {
                listOfItems[itemIndex].SetData(itemImage);

                for (int i = itemIndex; i < listOfItems.Count; i++)
                {
                    if (inventoryData.GetItemAt(i).IsEmpty)
                    {
                        listOfItems[i].ResetData();
                    }
                }
            }
        }
    }

    public void RemoveItemFromList(InvenItem item)
    {
        int itemIndex = listOfItems.IndexOf(item);

        if (itemIndex >= 0 && itemIndex < inventoryData.bagSize)
        {
            inventoryData.RemoveItem(itemIndex);

            listOfItems[itemIndex].ResetData();
            listOfItems[itemIndex].Deselect();
        }
    }

    //������ �� ������ ��� ����
    public void itemQualityGauge(float quality)
    {
        qualityGauge.fillAmount = quality / 100;
    }

    //�κ��丮 ����� ��ư
    public void InventoryHide()
    {
        if (menuBtn != null && activeBtn != null)
        {
            foreach (GameObject go in menuBtn)
            {
                go.SetActive(true);
            }

            EffectSoundManager.instance.MainmenuButtonBack();
            gameObject.SetActive(false);
            DeselectAllItems();
        }
    }

    //������ description ������Ʈ
    public void UpdateDescription(int itemIndex, string title, Sprite itemImage, float quality,
        basicMaterialItemSO.Effect effect1, basicMaterialItemSO.Effect effect2, basicMaterialItemSO.Effect effect3,
        basicMaterialItemSO.Effect effect4, basicMaterialItemSO.Speccificity specificity1,
        basicMaterialItemSO.Speccificity specificity2, basicMaterialItemSO.Speccificity specificity3,
        basicMaterialItemSO.Speccificity specificity4, basicMaterialItemSO.Element element1,
        basicMaterialItemSO.Element element2, basicMaterialItemSO.Element element3,
        basicMaterialItemSO.Element element4)
    {
        //string�� �������� ����
        string effect1String, effect2String, effect3String, effect4String,
            specificity1String, specificity2String, specificity3String, specificity4String,
            element1String, element2String, element3String, element4String;

        //string���� ��ȯ
        EnumToString(effect1, effect2, effect3, effect4,
            specificity1, specificity2, specificity3, specificity4,
            element1, element2, element3, element4,
            out effect1String, out effect2String, out effect3String, out effect4String,
            out specificity1String, out specificity2String, out specificity3String, out specificity4String,
            out element1String, out element2String, out element3String, out element4String);

        if (AlchemySystem.alchemyCombineOnOff == false)
        {
            itemDescription.SetDescription(title, itemImage, quality, effect1String, effect2String, effect3String, effect4String,
                specificity1String, specificity2String, specificity3String, specificity4String);

            itemQualityGauge(quality);

            DeselectAllItems();

            listOfItems[itemIndex].Select();
        }

        else if (AlchemySystem.alchemyCombineOnOff == true)
        {
            alchemyInven.invenSysToAlchemyInven(itemIndex, itemImage, title, quality, specificity1String, specificity2String,
                specificity3String, specificity4String,
                element1String, element2String, element3String, element4String);

            DeselectAllItems();
        }
    }

    public static void EnumToString(Effect effect1, Effect effect2, Effect effect3, Effect effect4,
        Speccificity specificity1, Speccificity specificity2, Speccificity specificity3, Speccificity specificity4,
        Element element1, Element element2, Element element3, Element element4,
        out string effect1String, out string effect2String, out string effect3String, out string effect4String,
        out string specificity1String, out string specificity2String, out string specificity3String, out string specificity4String,
        out string element1String, out string element2String, out string element3String, out string element4String)
    {
        effect1String = GetStringForEffect(effect1);
        effect2String = GetStringForEffect(effect2);
        effect3String = GetStringForEffect(effect3);
        effect4String = GetStringForEffect(effect4);

        specificity1String = GetStringForSpecificity(specificity1);
        specificity2String = GetStringForSpecificity(specificity2);
        specificity3String = GetStringForSpecificity(specificity3);
        specificity4String = GetStringForSpecificity(specificity4);

        element1String = GetstringForElement(element1);
        element2String = GetstringForElement(element2);
        element3String = GetstringForElement(element3);
        element4String = GetstringForElement(element4);
    }

    public static string GetStringForEffect(Effect effect)
    {
        switch (effect)
        {
            case Effect.none:
                return "-";
            case Effect.damageSmall:
                return "������ ��";
            case Effect.damageMedium:
                return "������ ��";
            case Effect.damageLarge:
                return "������ ��";
            case Effect.damageExtraLarge:
                return "������ Ư��";

            case Effect.restoreSmall:
                return "ȸ���� ��";
            case Effect.restoreMedium:
                return "ȸ���� ��";
            case Effect.restoreLarge:
                return "ȸ���� ��";
            case Effect.resotreExtraLarge:
                return "ȸ���� Ư��";

            case Effect.atkSmall:
                return "���ݷ� ���� ��";
            case Effect.atkMedium:
                return "���ݷ� ���� ��";
            case Effect.atkLarge:
                return "���ݷ� ���� ��";
            case Effect.atkExtraLarge:
                return "���ݷ� ���� Ư��";

            case Effect.defSmall:
                return "���� ���� ��";
            case Effect.defMedium:
                return "���� ���� ��";
            case Effect.defLarge:
                return "���� ���� ��";
            case Effect.defExtraLarge:
                return "���� ���� Ư��";

            case Effect.spdSmall:
                return "�ӵ� ���� ��";
            case Effect.spdMedium:
                return "�ӵ� ���� ��";
            case Effect.spdLarge:
                return "�ӵ� ���� ��";
            case Effect.spdExtraLarge:
                return "�ӵ� ���� Ư��";

            default:
                return "";
        }
    }

    public static string GetStringForSpecificity(Speccificity speccificity)
    {
        switch (speccificity)
        {
            case Speccificity.none:
                return "-";
            case Speccificity.damagePowerUp:
                return "������ ��ġ ����";
            case Speccificity.restorePowerUp:
                return "ȸ�� ��ġ ����";
            case Speccificity.atkUp:
                return "���ݷ� ��ġ ����";
            case Speccificity.defUp:
                return "���� ��ġ ����";
            case Speccificity.spdUp:
                return "�ӵ� ��ġ ����";
            case Speccificity.atkdefUp:
                return "���� ��ġ ����";
            case Speccificity.atkspdUp:
                return "���� ��ġ ����";
            case Speccificity.defspdUp:
                return "��� ��ġ ����";
            case Speccificity.allUp:
                return "���ɷ� ��ġ ����";
            default:
                return "";
        }
    }

    public static string GetstringForElement(Element element)
    {
        switch (element)
        {
            case Element.none:
                return "-";
            case Element.water:
                return "�� ����";
            case Element.plant:
                return "�Ĺ� ����";
            case Element.stone:
                return "�� ����";
            case Element.ore:
                return "���� ����";
            case Element.fuel:
                return "����";
            case Element.monster:
                return "���� ����";
            case Element.mystery:
                return "�ź��� ��";
            default:
                return "";
        }
    }

    public static string GetstromgForTarget(Target target)
    {
        switch (target)
        {
            case Target.none:
                return "-";
            case Target.enemy:
                return "�� ����";
            case Target.enemyAll:
                return "�� ��ü";
            case Target.ally:
                return "�Ʊ�";
            case Target.equipable:
                return "��������";
            default:
                return "";
        }
    }
}
