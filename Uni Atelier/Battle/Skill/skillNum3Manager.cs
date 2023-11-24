using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class skillNum3Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject skillNum3;

    public GameObject skillDescription;

    private Color txtColor;

    private TextMeshProUGUI description;

    private skillclass skill3 = new skillclass();

    public actionSystem actionSystem;

    public Player player;
    private void Start()
    {
        txtColor = skillNum3.GetComponent<TextMeshProUGUI>().color;
        description = skillDescription.GetComponent<TextMeshProUGUI>();

        skill3.skillName = "오러 브레이크";
        skill3.skillCost = 12;
        skill3.skillEffect = "적 전체에게 데미지";
        skill3.skillDescription = "지팡이에 오러를 가득 담아 후려친다.";
        skill3.skillmagnification = 1.2f;
        skill3.skillSpecify = 1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        txtColor = new Color(255, 0, 0);
        skillNum3.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = $"스킬명 : {skill3.skillName}" +
            $"\n소비 마나 : {skill3.skillCost}\n효과 : {skill3.skillEffect}" +
            $"\n스킬 설명 : {skill3.skillDescription}";
        skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txtColor = new Color(0, 0, 0);
        skillNum3.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = "";
        skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player.NowMp >= skill3.skillCost)
        {
            player.NowMp -= skill3.skillCost;

            actionSystem.skillNum3 = true;

            txtColor = new Color(0, 0, 0);
            skillNum3.GetComponent<TextMeshProUGUI>().color = txtColor;
            description.text = "";
            skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;

            EffectSoundManager.instance.MainmenuOn();
        }

        else if (player.NowMp < skill3.skillCost)
        {
            return;
        }
    }
}
