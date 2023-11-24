using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class skillNum5Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject skillNum5;
    public GameObject skillDescription;

    private Color txtColor;
    private TextMeshProUGUI description;

    private skillclass skill5 = new skillclass();

    public actionSystem actionSystem;

    public Player player;

    private void Start()
    {
        txtColor = skillNum5.GetComponent<TextMeshProUGUI>().color;
        description = skillDescription.GetComponent<TextMeshProUGUI>();

        skill5.skillName = "체인 크리티컬";
        skill5.skillCost = 24;
        skill5.skillEffect = "적 단일 대상에게 큰 데미지";
        skill5.skillDescription = "몸 전체를 사용한 무자비한 연격.";
        skill5.skillmagnification = 5;
        skill5.skillSpecify = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        txtColor = new Color(255, 0, 0);
        skillNum5.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = $"스킬명 : {skill5.skillName}" +
            $"\n소비 마나 : {skill5.skillCost}\n효과 : {skill5.skillEffect}" +
            $"\n스킬 설명 : {skill5.skillDescription}";

        skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txtColor = new Color(0, 0, 0);
        skillNum5.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = "";
        skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player.NowMp >= skill5.skillCost)
        {
            player.NowMp -= skill5.skillCost;

            actionSystem.skillNum5 = true;

            txtColor = new Color(0, 0, 0);
            skillNum5.GetComponent<TextMeshProUGUI>().color = txtColor;
            description.text = "";
            skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;

            EffectSoundManager.instance.MainmenuOn();
        }

        else if (player.NowMp < skill5.skillCost)
        {
            return;
        }
    }
}
