using UnityEngine;
using UnityEngine.EventSystems;

public class itemButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject itemButton;

    public GameObject highlightPointer;

    private GameObject go;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemButton != null)
        {
            go = Instantiate(highlightPointer, itemButton.transform.position, Quaternion.identity);
            go.transform.parent = itemButton.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemButton != null)
        {
            Destroy(go);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(go);
        EffectSoundManager.instance.SelectButton();
    }
}
