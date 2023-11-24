using UnityEngine;
using UnityEngine.EventSystems;

public class skillButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject skillButton;

    public GameObject highlightPointer;

    private GameObject go;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skillButton != null)
        {
            go = Instantiate(highlightPointer, skillButton.transform.position, Quaternion.identity);
            go.transform.parent = skillButton.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (skillButton != null)
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
