using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class specificityExpandList : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Outline hightLigth;

    AlchemySystem alchemySystem;

    public TMP_Text specificityList1;
    public TMP_Text specificityList2;
    public TMP_Text specificityList3;
    public TMP_Text specificityList4;

    int clickNumber;

    public event Action<specificityExpandList> specificityExpandClick;

    private void Start()
    {
        hightLigth = gameObject.GetComponent<Outline>();
        hightLigth.enabled = false;
    }

    //AlchemySystem으로 이동함
    public void OnPointerClick(PointerEventData eventData)
    {
        specificityExpandClick?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hightLigth.enabled = true;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hightLigth.enabled = false;
    }
}
