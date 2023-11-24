using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//연금술 화면 재료 인벤토리 관련
public class AlchemyInven : MonoBehaviour
{
    public InventorySO invenSO;
    public InventorySystem invenSys;
    public InvenItem invenItemPrefab;
    public RectTransform content;

    public TMP_Dropdown elementFilter;

    public event Action<int> AlchemyItemDescriptionRequested,
        OnItemInRequested, OnItemOutRequested;

    [Header("Material Description Related")]
    public Sprite nullImage;
    public Image materialImage;
    public TMP_Text materialTitle;
    public TMP_Text materialQuality;
    public TMP_Text materialSpecificity1;
    public TMP_Text materialSpecificity2;
    public TMP_Text materialSpecificity3;
    public TMP_Text materialSpecificity4;
    public TMP_Text materialElement1;
    public TMP_Text materialElement2;
    public TMP_Text materialElement3;
    public TMP_Text materialElement4;

    [Header("Combine Description Related")]
    public Image combineImage;
    public TMP_Text combineTitle;
    public TMP_Text combineQuality;
    public TMP_Text combineEffect1;
    public TMP_Text combineEffect2;
    public TMP_Text combineEffect3;
    public TMP_Text combineEffect4;
    public TMP_Text combineSpecificity1;
    public TMP_Text combineSpecificity2;
    public TMP_Text combineSpecificity3;
    public TMP_Text combineSpecificity4;

    public List<InvenItem> AlchemylistOfItems;

    public basicMaterialItemSO.Element currentFilter = basicMaterialItemSO.Element.none;

    [Header("Alchemy Combine Materials Select")]
    public AlchemySystem alchemySystem;
    public List<InvenItem> selectedItem;
    public GameObject specificityExpand;
    public float qualitySum;

    void Start()
    {
        InitializeAlchemy();

        AlchemyItemDescriptionRequested += AlchemyDescriptionOn;

        //필터기능, elementFilter가 변경될 때마다 FilterElement 실행
        elementFilter.onValueChanged.AddListener(FilterElement);

        specificityExpand.SetActive(false);
    }

    public void InitializeAlchemy()
    {
        for (int i = 0; i < invenSys.inventorySize; i++)
        {
            InvenItem Item = Instantiate(invenItemPrefab, Vector3.zero, Quaternion.identity);

            Item.transform.SetParent(content);
            AlchemylistOfItems.Add(Item);

            Item.OnItemLeftClicked += AlchemyItemLeftClicked;
            Item.OnItemRightClicked += AlchemyItemRightClicked;
            Item.OnItemInRequested += Item_OnItemInRequested;
            Item.OnItemOutRequested += Item_OnItemOutRequested;
        }
    }

    //아이템 투입
    private void Item_OnItemInRequested(InvenItem listItem)
    {
        if (alchemySystem.node1Start == true || alchemySystem.node2Start == true
            || alchemySystem.node3Start == true || alchemySystem.node4Start == true)
        {
            if ((materialElement1.text == alchemySystem.combineConditionElement.text) ||
                (materialElement2.text == alchemySystem.combineConditionElement.text) ||
                (materialElement3.text == alchemySystem.combineConditionElement.text) ||
                (materialElement4.text == alchemySystem.combineConditionElement.text))
            {
                if (selectedItem.Contains(listItem))
                {
                    return;
                }
                else
                {
                    if (alchemySystem.combineConditionValue <= 0)
                    {
                        return;
                    }
                    else
                    {
                        EffectSoundManager.instance.MaterialIn();

                        alchemySystem.combineConditionValue--;
                        selectedItem.Add(listItem);
                        invenItemPrefab.alchemyPopUpDisplay.SetActive(false);

                        //TMP_Text - float로 변환
                        if (float.TryParse(materialQuality.text, out float floatQuality))
                        {
                            qualitySum += floatQuality;

                            combineQuality.text = qualitySum.ToString();
                        }
                    }
                }
            }
        }
    }

    //아이템 빼기
    private void Item_OnItemOutRequested(InvenItem listItem)
    {
        if (alchemySystem.node1Start == true || alchemySystem.node2Start == true
         || alchemySystem.node3Start == true || alchemySystem.node4Start == true)
        {
            if ((materialElement1.text == alchemySystem.combineConditionElement.text) ||
                (materialElement2.text == alchemySystem.combineConditionElement.text) ||
                (materialElement3.text == alchemySystem.combineConditionElement.text) ||
                (materialElement4.text == alchemySystem.combineConditionElement.text))
            {
                if ((alchemySystem.node1Start != alchemySystem.node1Complete) ||
                    (alchemySystem.node2Start != alchemySystem.node2Complete) ||
                    (alchemySystem.node3Start != alchemySystem.node3Complete) ||
                    (alchemySystem.node4Start != alchemySystem.node4Complete))
                {
                    if (selectedItem.Contains(listItem))
                    {
                        EffectSoundManager.instance.MaterialOut();

                        alchemySystem.combineConditionValue++;
                        selectedItem.Remove(listItem);
                        invenItemPrefab.alchemyPopUpDisplay.SetActive(false);

                        if (float.TryParse(materialQuality.text, out float floatQuality))
                        {
                            qualitySum -= floatQuality;

                            combineQuality.text = qualitySum.ToString();
                        }
                    }
                }
            }
        }
    }

    public void RemoveItemFromSelectedLIst(InvenItem item)
    {
        int itemIndex = AlchemylistOfItems.IndexOf(item);

        if (itemIndex >= 0 && itemIndex < invenSO.bagSize)
        {
            invenSO.RemoveItem(itemIndex);

            AlchemylistOfItems[itemIndex].ResetData();
            AlchemylistOfItems[itemIndex].Deselect();
        }
    }

    void Update()
    {
        AlchemyinventoryItemDisplay();
    }

    private void FilterElement(int elementIndex)
    {
        currentFilter = (basicMaterialItemSO.Element)elementIndex;
    }

    //인벤토리 content내에 아이템 이미지 표시
    public void AlchemyUpdataData(int itemIndex, Sprite itemImage)
    {
        if (AlchemylistOfItems.Count > itemIndex)
        {
            if (invenSO.GetItemAt(itemIndex).IsEmpty)
            {
                // 빈 슬롯인 경우 이미지 숨기거나 빈 이미지로 설정
                AlchemylistOfItems[itemIndex].ResetData();
            }
            else
            {
                AlchemylistOfItems[itemIndex].SetData(itemImage);

                for (int i = itemIndex; i < AlchemylistOfItems.Count; i++)
                {
                    if (invenSO.GetItemAt(i).IsEmpty)
                    {
                        AlchemylistOfItems[i].ResetData();
                    }
                }
            }
        }
    }

    //필터 기능 포함된 인벤토리 내용물 표시
    public void AlchemyinventoryItemDisplay()
    {
        foreach (var item in invenSO.GetCurrentInventoryState())
        {
            bool shouldShowItem = currentFilter == basicMaterialItemSO.Element.none ||
                (item.Value.baseItem.element1 == currentFilter ||
                item.Value.baseItem.element2 == currentFilter ||
                item.Value.baseItem.element3 == currentFilter ||
                item.Value.baseItem.element4 == currentFilter);
            {
                AlchemyUpdataData(item.Key, item.Value.baseItem.itemImage);
            }

            AlchemylistOfItems[item.Key].gameObject.SetActive(shouldShowItem);
        }
    }

    //아이템 왼쪽 클릭 시
    public void AlchemyItemLeftClicked(InvenItem item)
    {
        int index = AlchemylistOfItems.IndexOf(item);

        if (index == -1)
        {
            return;
        }

        AlchemyItemDescriptionRequested?.Invoke(index);
    }

    //아이템 오른쪽 클릭 시 팝업창 등장
    private void AlchemyItemRightClicked(InvenItem item)
    {
        if (item.selectOn == true)
        {
            item.alchemyPopUpDisplay.SetActive(true);
        }
    }

    public void AlchemyDescriptionOn(int itemIndex)
    {
        InventoryStruct inventoryStruct = invenSO.GetItemAt(itemIndex);

        if (inventoryStruct.IsEmpty)
        {
            return;
        }

        basicMaterialItemSO basicItem = inventoryStruct.baseItem;

        invenSys.UpdateDescription(itemIndex, basicItem.title, basicItem.itemImage, basicItem.quality,
            basicItem.effect1, basicItem.effect2, basicItem.effect3, basicItem.effect4,
            basicItem.specificity1, basicItem.specificity2, basicItem.specificity3, basicItem.specificity4,
            basicItem.element1, basicItem.element2, basicItem.element3, basicItem.element4);
    }

    public void invenSysToAlchemyInven(int itemIndex, Sprite itemImage, string title, float quality, string specificity1, string specificity2,
        string specificity3, string specificity4,
        string element1, string element2, string element3, string element4)
    {
        materialImage.sprite = itemImage;
        materialTitle.text = title;
        materialQuality.text = quality + "";
        materialSpecificity1.text = specificity1;
        materialSpecificity2.text = specificity2;
        materialSpecificity3.text = specificity3;
        materialSpecificity4.text = specificity4;
        materialElement1.text = element1;
        materialElement2.text = element2;
        materialElement3.text = element3;
        materialElement4.text = element4;

        DeselectAllItems();

        AlchemylistOfItems[itemIndex].Select();
    }

    public void DeselectAllItems()
    {
        foreach (InvenItem item in AlchemylistOfItems)
        {
            if (!selectedItem.Contains(item))
                item.Deselect();
        }
    }

    public void ResetDescription()
    {
        materialImage.sprite = nullImage;
        materialTitle.text = "-";
        materialQuality.text = "-";
        materialSpecificity1.text = "-";
        materialSpecificity2.text = "-";
        materialSpecificity3.text = "-";
        materialSpecificity4.text = "-";
        materialElement1.text = "-";
        materialElement2.text = "-";
        materialElement3.text = "-";
        materialElement4.text = "-";
    }
}
