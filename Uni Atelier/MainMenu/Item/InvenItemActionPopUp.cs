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

    ////메인 버튼 액션은 inventory System에서 처리할 예정
    ////연금술 버튼 액션은 AlchemySystem에서 처리할 예정
    //public event Action<InvenItemActionPopUp> OnItemEquipRequested, OnItemDeleteRequested,
    //    OnItemInRequested, OnItemOutRequested;

    ////Main Inventory 버튼 액션 모음
    //public void MainInvenEquip()
    //{ 
    //    OnItemEquipRequested?.Invoke(this);
    //}

    //public void MainInvenDelete()
    //{
    //    OnItemDeleteRequested?.Invoke(this);
    //}

    ////Alchemy Inven 버튼 액션 모음
    //public void AlchemyItemIn()
    //{
    //    OnItemInRequested?.Invoke(this);
    //}

    //public void AlchemyItemOut()
    //{
    //    OnItemOutRequested?.Invoke(this);
    //}
}
