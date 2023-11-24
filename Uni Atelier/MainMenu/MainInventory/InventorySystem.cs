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

    //인벤토리 빈칸 프리팹
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

    //Main Inventory 아이템 버리기
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

    //아이템 설명 표시
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

        //필터 기능
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

    //마우스 왼쪽 클릭할 때
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

    //마우스 오른쪽 클릭할 때
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

    //인벤토리 보여주기
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

    //아이템 별 게이지 모습 변경
    public void itemQualityGauge(float quality)
    {
        qualityGauge.fillAmount = quality / 100;
    }

    //인벤토리 숨기기 버튼
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

    //아이템 description 업데이트
    public void UpdateDescription(int itemIndex, string title, Sprite itemImage, float quality,
        basicMaterialItemSO.Effect effect1, basicMaterialItemSO.Effect effect2, basicMaterialItemSO.Effect effect3,
        basicMaterialItemSO.Effect effect4, basicMaterialItemSO.Speccificity specificity1,
        basicMaterialItemSO.Speccificity specificity2, basicMaterialItemSO.Speccificity specificity3,
        basicMaterialItemSO.Speccificity specificity4, basicMaterialItemSO.Element element1,
        basicMaterialItemSO.Element element2, basicMaterialItemSO.Element element3,
        basicMaterialItemSO.Element element4)
    {
        //string용 지역변수 선언
        string effect1String, effect2String, effect3String, effect4String,
            specificity1String, specificity2String, specificity3String, specificity4String,
            element1String, element2String, element3String, element4String;

        //string으로 변환
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
                return "데미지 소";
            case Effect.damageMedium:
                return "데미지 중";
            case Effect.damageLarge:
                return "데미지 대";
            case Effect.damageExtraLarge:
                return "데미지 특대";

            case Effect.restoreSmall:
                return "회복력 소";
            case Effect.restoreMedium:
                return "회복력 중";
            case Effect.restoreLarge:
                return "회복력 대";
            case Effect.resotreExtraLarge:
                return "회복력 특대";

            case Effect.atkSmall:
                return "공격력 증가 소";
            case Effect.atkMedium:
                return "공격력 증가 중";
            case Effect.atkLarge:
                return "공격력 증가 대";
            case Effect.atkExtraLarge:
                return "공격력 증가 특대";

            case Effect.defSmall:
                return "방어력 증가 소";
            case Effect.defMedium:
                return "방어력 증가 중";
            case Effect.defLarge:
                return "방어력 증가 대";
            case Effect.defExtraLarge:
                return "방어력 증가 특대";

            case Effect.spdSmall:
                return "속도 증가 소";
            case Effect.spdMedium:
                return "속도 증가 중";
            case Effect.spdLarge:
                return "속도 증가 대";
            case Effect.spdExtraLarge:
                return "속도 증가 특대";

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
                return "데미지 수치 증가";
            case Speccificity.restorePowerUp:
                return "회복 수치 증가";
            case Speccificity.atkUp:
                return "공격력 수치 증가";
            case Speccificity.defUp:
                return "방어력 수치 증가";
            case Speccificity.spdUp:
                return "속도 수치 증가";
            case Speccificity.atkdefUp:
                return "공방 수치 증가";
            case Speccificity.atkspdUp:
                return "공속 수치 증가";
            case Speccificity.defspdUp:
                return "방속 수치 증가";
            case Speccificity.allUp:
                return "전능력 수치 증가";
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
                return "물 소재";
            case Element.plant:
                return "식물 소재";
            case Element.stone:
                return "돌 소재";
            case Element.ore:
                return "광물 소재";
            case Element.fuel:
                return "연료";
            case Element.monster:
                return "몬스터 소재";
            case Element.mystery:
                return "신비한 힘";
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
                return "적 단일";
            case Target.enemyAll:
                return "적 전체";
            case Target.ally:
                return "아군";
            case Target.equipable:
                return "장비아이템";
            default:
                return "";
        }
    }
}
