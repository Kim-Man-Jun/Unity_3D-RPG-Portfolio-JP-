using UnityEngine;
using UnityEngine.EventSystems;

public class runButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject runButton;

    public GameObject highlightPointer;

    private GameObject go;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (runButton != null)
        {
            go = Instantiate(highlightPointer, runButton.transform.position, Quaternion.identity);
            go.transform.parent = runButton.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (runButton != null)
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
