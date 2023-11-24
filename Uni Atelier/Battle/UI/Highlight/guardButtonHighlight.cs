using UnityEngine;
using UnityEngine.EventSystems;

public class guardButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject guardButton;

    public GameObject highlightPointer;

    private GameObject go;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (guardButton != null)
        {
            go = Instantiate(highlightPointer, guardButton.transform.position, Quaternion.identity);
            go.transform.parent = guardButton.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (guardButton != null)
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
