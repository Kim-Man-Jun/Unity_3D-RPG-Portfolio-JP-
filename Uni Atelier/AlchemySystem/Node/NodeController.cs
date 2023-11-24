using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//각 노드별로 부착
public class NodeController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public string nodeName;

    public List<Sprite> nodeImages;

    public TMP_Text nodeElementName;
    public Image nodeElementImage;

    //각 재료별 필요한 수량
    public float nodeInsertNumber;

    //각 재료별 필요한 수량을 표시
    public float nodeInsertQuantity;

    public TMP_Text nodeInsertNumberText;
    public TMP_Text nodeEffectAdding;
    public TMP_Text nodeElementlAdding;

    public Outline nodeOutline;

    public CombineListSO combineListSO;

    public CombineList combineList;

    public AlchemySystem alchemySystem;

    public event Action<NodeController> alchemyCombineClick, alchemyCombineNodeOut;

    void Start()
    {
        nodeName = gameObject.name;
    }

    void Update()
    {
        switch (nodeName)
        {
            case "AlchemyNode1":
                nodeElementName.text = combineList.combineMaterialName1.text;
                break;

            case "AlchemyNode2":
                nodeElementName.text = combineList.combineMaterialName2.text;
                break;

            case "AlchemyNode3":
                nodeElementName.text = combineList.combineMaterialName3.text;
                break;

            case "AlchemyNode4":
                nodeElementName.text = combineList.combineMaterialName4.text;
                break;
        }

        if (AlchemySystem.alchemyCombineOnOff == false)
        {
            switch (nodeElementName.text)
            {
                case "-":
                    nodeElementImage.sprite = null;
                    break;

                case "물 소재":
                    nodeInsertNumber = 2;
                    nodeInsertNumberText.text = nodeInsertNumber + "";
                    nodeElementImage.sprite = nodeImages[0];
                    nodeInsertQuantity = nodeInsertNumber;
                    break;

                case "식물 소재":
                    nodeInsertNumber = 3;
                    nodeInsertNumberText.text = nodeInsertNumber + "";
                    nodeElementImage.sprite = nodeImages[1];
                    nodeInsertQuantity = nodeInsertNumber;
                    break;

                case "돌 소재":
                    nodeInsertNumber = 3;
                    nodeInsertNumberText.text = nodeInsertNumber + "";
                    nodeElementImage.sprite = nodeImages[2];
                    nodeInsertQuantity = nodeInsertNumber;
                    break;

                case "광물 소재":
                    nodeInsertNumber = 4;
                    nodeInsertNumberText.text = nodeInsertNumber + "";
                    nodeElementImage.sprite = nodeImages[3];
                    nodeInsertQuantity = nodeInsertNumber;
                    break;

                case "연료":
                    nodeInsertNumber = 2;
                    nodeInsertNumberText.text = nodeInsertNumber + "";
                    nodeElementImage.sprite = nodeImages[4];
                    nodeInsertQuantity = nodeInsertNumber;
                    break;

                case "몬스터 소재":
                    nodeInsertNumber = 3;
                    nodeInsertNumberText.text = nodeInsertNumber + "";
                    nodeElementImage.sprite = nodeImages[5];
                    nodeInsertQuantity = nodeInsertNumber;
                    break;

                case "신비한 힘":
                    nodeInsertNumber = 1;
                    nodeInsertNumberText.text = nodeInsertNumber + "";
                    nodeElementImage.sprite = nodeImages[6];
                    nodeInsertQuantity = nodeInsertNumber;
                    break;
            }
        }

        else
        {
            switch (nodeElementName.text)
            {
                case "-":
                    nodeElementImage.sprite = null;
                    break;

                case "물 소재":
                    nodeInsertNumberText.text = nodeInsertQuantity + "";
                    nodeElementImage.sprite = nodeImages[0];
                    break;

                case "식물 소재":
                    nodeInsertNumberText.text = nodeInsertQuantity + "";
                    nodeElementImage.sprite = nodeImages[1];
                    break;

                case "돌 소재":
                    nodeInsertNumberText.text = nodeInsertQuantity + "";
                    nodeElementImage.sprite = nodeImages[2];
                    break;

                case "광물 소재":
                    nodeInsertNumberText.text = nodeInsertQuantity + "";
                    nodeElementImage.sprite = nodeImages[3];
                    break;
                    
                case "연료":
                    nodeInsertNumberText.text = nodeInsertQuantity + "";
                    nodeElementImage.sprite = nodeImages[4];
                    break;

                case "몬스터 소재":
                    nodeInsertNumberText.text = nodeInsertQuantity + "";
                    nodeElementImage.sprite = nodeImages[5];
                    break;

                case "신비한 힘":
                    nodeInsertNumberText.text = nodeInsertQuantity + "";
                    nodeElementImage.sprite = nodeImages[6];
                    break;
            }
        }

        switch (combineList.name)
        {
            case "Fram":
                nodeEffectNameChange(nodeName);
                break;

            case "Frambe":
                nodeEffectNameChange(nodeName);
                break;

            case "HpRestore":
                nodeEffectNameChange(nodeName);
                break;

            case "MpRestore":
                nodeEffectNameChange(nodeName);
                break;

            case "HpMpRestore":
                nodeEffectNameChange(nodeName);
                break;

            case "RubyWand":
                nodeEffectNameChange(nodeName);
                break;

            case "LightArmor":
                nodeEffectNameChange(nodeName);
                break;

            case "Gnadering":
                nodeEffectNameChange(nodeName);
                break;
        }
    }

    private void nodeEffectNameChange(string nodeName)
    {
        if (nodeName == "AlchemyNode1")
        {
            nodeEffectAdding.text =
                InventorySystem.GetStringForEffect(combineListSO.effect1);
        }
        else if (nodeName == "AlchemyNode2")
        {
            nodeEffectAdding.text =
                InventorySystem.GetStringForEffect(combineListSO.effect2);
        }
        else if (nodeName == "AlchemyNode3")
        {
            nodeEffectAdding.text =
                InventorySystem.GetStringForEffect(combineListSO.effect3);
        }
        else if (nodeName == "AlchemyNode4")
        {
            nodeEffectAdding.text =
                InventorySystem.GetStringForEffect(combineListSO.effect4);
        }
    }

    //AlchemySystem으로 전달
    public void OnPointerClick(PointerEventData eventData)
    {
        if (AlchemySystem.alchemyCombineOnOff == true)
        {
            EffectSoundManager.instance.SelectButton();
            alchemyCombineClick?.Invoke(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (AlchemySystem.alchemyCombineOnOff == true)
        {
            nodeOutline.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (AlchemySystem.alchemyCombineOnOff == true)
        {
            alchemyCombineNodeOut?.Invoke(this);
        }
    }
}
