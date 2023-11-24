using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenItemActionPopUp : MonoBehaviour, IPointerExitHandler
{
    public InvenItem invenItem;

    public void OnPointerExit(PointerEventData eventData)
    {
        if (invenItem.mainPopUpDisplay != null)
        {
            invenItem.mainPopUpDisplay.SetActive(false);
        }

        if (invenItem.alchemyPopUpDisplay != null)
        {
            invenItem.alchemyPopUpDisplay.SetActive(false);
        }

        if (invenItem.attackPopUpDisplay != null)
        {
            invenItem.attackPopUpDisplay.SetActive(false);
        }

        if (invenItem.restorePopUpDisplay != null)
        {
            invenItem.restorePopUpDisplay.SetActive(false);
        }

        if (invenItem.equipPopUpDisplay != null)
        {
            invenItem.equipPopUpDisplay.SetActive(false);
        }
    }

    ////���� ��ư �׼��� inventory System���� ó���� ����
    ////���ݼ� ��ư �׼��� AlchemySystem���� ó���� ����
    //public event Action<InvenItemActionPopUp> OnItemEquipRequested, OnItemDeleteRequested,
    //    OnItemInRequested, OnItemOutRequested;

    ////Main Inventory ��ư �׼� ����
    //public void MainInvenEquip()
    //{ 
    //    OnItemEquipRequested?.Invoke(this);
    //}

    //public void MainInvenDelete()
    //{
    //    OnItemDeleteRequested?.Invoke(this);
    //}

    ////Alchemy Inven ��ư �׼� ����
    //public void AlchemyItemIn()
    //{
    //    OnItemInRequested?.Invoke(this);
    //}

    //public void AlchemyItemOut()
    //{
    //    OnItemOutRequested?.Invoke(this);
    //}
}
