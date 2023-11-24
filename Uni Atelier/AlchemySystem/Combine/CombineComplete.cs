using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CombineComplete : MonoBehaviour
{
    [Header("CombineComplete List")]
    public Image combineCompleteImage;
    public TMP_Text combineCompleteTitle;
    public TMP_Text combineCompleteQuality;
    public TMP_Text combineCompleteEffect1;
    public TMP_Text combineCompleteEffect2;
    public TMP_Text combineCompleteEffect3;
    public TMP_Text combineCompleteEffect4;
    public TMP_Text combineCompleteSpecificity1;
    public TMP_Text combineCompleteSpecificity2;
    public TMP_Text combineCompleteSpecificity3;
    public TMP_Text combineCompleteSpecificity4;

    [Header("Inventory Item Clear")]
    public InventorySystem inventorySystem;
    public AlchemyInven alchemyInven;
    public AlchemySystem alcheySystem;
    public InvenItem invenitemPrefab;

    [Header("Battle Item Inventory")]
    public BattleInventorySO attackItemInventory;
    public BattleInventorySO restoreItemInventory;
    public BattleInventorySO equipItemInventory;

    public AlchemyController alchemyController;
    public GameObject combinePanel;
    public GameObject specificityExpand;

    public SaveLoadSystem saveLoad;

    //���� ���� ��ư�� ������ ���
    public void combineSpendableItemStart()
    {
        float qualityTofloat;

        float.TryParse(combineCompleteQuality.text, out qualityTofloat);

        basicMaterialItemSO.Effect effect1 =
            spendableItemSO.GetEffectEnumFromText(combineCompleteEffect1);
        basicMaterialItemSO.Effect effect2 =
            spendableItemSO.GetEffectEnumFromText(combineCompleteEffect2);
        basicMaterialItemSO.Effect effect3 =
            spendableItemSO.GetEffectEnumFromText(combineCompleteEffect3);
        basicMaterialItemSO.Effect effect4 =
            spendableItemSO.GetEffectEnumFromText(combineCompleteEffect4);

        basicMaterialItemSO.Speccificity specificity1 = spendableItemSO.
            GetSpeccificityEnumFromText(combineCompleteSpecificity1);
        basicMaterialItemSO.Speccificity specificity2 = spendableItemSO.
            GetSpeccificityEnumFromText(combineCompleteSpecificity2);
        basicMaterialItemSO.Speccificity specificity3 = spendableItemSO.
            GetSpeccificityEnumFromText(combineCompleteSpecificity3);
        basicMaterialItemSO.Speccificity specificity4 = spendableItemSO.
            GetSpeccificityEnumFromText(combineCompleteSpecificity4);

        spendableItemSO newItem = spendableItemSO.CreateItemAssetInstance(combineCompleteTitle.text,
            combineCompleteImage.sprite, qualityTofloat, effect1, effect2, effect3, effect4,
            specificity1, specificity2, specificity3, specificity4);

        for (int i = 0; i < alchemyInven.selectedItem.Count; i++)
        {
            InvenItem item = alchemyInven.selectedItem[i];

            inventorySystem.RemoveItemFromList(item);
            alchemyInven.RemoveItemFromSelectedLIst(item);
        }

        alchemyInven.selectedItem.Clear();

        if (attackItemInventory != null && restoreItemInventory != null
            && equipItemInventory != null)
        {
            switch (combineCompleteTitle.text)
            {
                case "����":
                case "������":
                    attackItemInventory.AddItem(newItem);
                    saveLoad.attackItemInventorySave();
                    break;

                case "ü�� ����":
                case "���� ����":
                case "���� ����":
                    restoreItemInventory.AddItem(newItem);
                    saveLoad.restoreItemInventorySave();
                    break;

                case "��� ���� �ϵ�":
                case "������ ����":
                case "�׳�����":
                    equipItemInventory.AddItem(newItem);
                    saveLoad.equipItemInventorySave();
                    break;
            }
            AssetDatabase.Refresh();
        }

        specificityExpand.SetActive(false);
        alchemyInven.qualitySum = 0;
        alcheySystem.alchemyNodeOff();
        alcheySystem.clickNumber = 0;
        alcheySystem.specificityExpandListDelete();

        AlchemySystem.alchemySystemOnOff = false;
        AlchemySystem.alchemyCombineComplete = false;

        alcheySystem.alchemyPlayingStop();

        alchemyController.backwardButton();

        combinePanel.SetActive(false);

        EffectSoundManager.instance.CombineComplete();

        AssetDatabase.Refresh();
    }
}
