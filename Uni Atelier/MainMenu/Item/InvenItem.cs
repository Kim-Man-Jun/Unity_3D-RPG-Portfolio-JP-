using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image hightLight;

    public GameObject mainPopUpDisplay;
    public GameObject alchemyPopUpDisplay;
    public GameObject attackPopUpDisplay;
    public GameObject restorePopUpDisplay;
    public GameObject equipPopUpDisplay;

    public event Action<InvenItem> OnItemLeftClicked, OnItemRightClicked,
        OnItemEquipRequested, OnItemDeleteRequested,
        OnItemInRequested, OnItemOutRequested,
        OnAttackItemDeleteRequested, OnAttackItemCalcelRequested,
        OnRestoreItemUseRequested, OnRestoreItemDeleteRequested,
        OnEquipItemRequested, OnEquipItemDeleteRequested;

    private bool empty = true;

    public bool selectOn;

    private void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);

        if (mainPopUpDisplay != null)
        {
            mainPopUpDisplay.gameObject.SetActive(false);
        }

        if (alchemyPopUpDisplay != null)
        {
            alchemyPopUpDisplay.gameObject.SetActive(false);
        }

        if (attackPopUpDisplay != null)
        {
            attackPopUpDisplay.gameObject.SetActive(false);
        }

        if (restorePopUpDisplay != null)
        {
            restorePopUpDisplay.gameObject.SetActive(false);
        }

        if (equipPopUpDisplay != null)
        {
            equipPopUpDisplay.gameObject.SetActive(false);
        }

        empty = true;
    }

    public void Deselect()
    {
        hightLight.gameObject.SetActive(false);
        selectOn = false;
    }

    public void SetData(Sprite sprite)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        empty = false;
    }

    public void Select()
    {
        hightLight.gameObject.SetActive(true);
        selectOn = true;
    }

    //inventorySystem으로 전달
    //inBattleInventory으로 전달
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnItemLeftClicked?.Invoke(this);
        }
        else
        {
            OnItemRightClicked?.Invoke(this);
        }
    }

    //Main Inven 버튼 액션 모음
    public void MainInvenEquip()
    {
        OnItemEquipRequested?.Invoke(this);
    }

    public void MainInvenDelete()
    {
        OnItemDeleteRequested?.Invoke(this);
    }

    //Alchemy Inven 버튼 액션 모음
    public void AlchemyItemIn()
    {
        OnItemInRequested?.Invoke(this);
    }

    public void AlchemyItemOut()
    {
        OnItemOutRequested?.Invoke(this);
    }

    //BattleInventory 버튼 액션 모음
    public void AttackItemDelete()
    {
        OnAttackItemDeleteRequested?.Invoke(this);
    }

    public void AttackItemCancel()
    {
        OnAttackItemCalcelRequested?.Invoke(this);
    }

    public void RestoreItemUse()
    {
        OnRestoreItemUseRequested?.Invoke(this);
    }

    public void RestoreItemDelete()
    {
        OnRestoreItemDeleteRequested?.Invoke(this);
    }

    public void EquipItem()
    {
        OnEquipItemRequested?.Invoke(this);
    }

    public void EquipItemDelete()
    {
        OnEquipItemDeleteRequested?.Invoke(this);
    }
}
