using UnityEngine;
using UnityEngine.EventSystems;

public class attackButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject attackButton;

    public GameObject highlightPointer;

    private GameObject go;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (attackButton != null)
        {
            go = Instantiate(highlightPointer, attackButton.transform.position, Quaternion.identity);
            go.transform.parent = attackButton.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (attackButton != null)
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
