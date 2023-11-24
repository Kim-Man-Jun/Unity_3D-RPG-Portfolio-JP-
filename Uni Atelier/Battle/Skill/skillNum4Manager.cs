using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class skillNum4Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject skillNum4;

    public GameObject skillDescription;

    private Color txtColor;

    private TextMeshProUGUI description;

    private skillclass skill4 = new skillclass();

    public actionSystem actionSystem;

    public Player player;

    private void Start()
    {
        txtColor = skillNum4.GetComponent<TextMeshProUGUI>().color;
        description = skillDescription.GetComponent<TextMeshProUGUI>();

        skill4.skillName = "이지스 필드";
        skill4.skillCost = 20;
        skill4.skillEffect = "자신의 방어력과 스피드 증가";
        skill4.skillDescription = "몸에 오러를 둘러 능력을 향상시킨다.";
        skill4.skillmagnification = 1.4f;
        skill4.skillSpecify = 2;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        txtColor = new Color(255, 0, 0);
        skillNum4.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = $"스킬명 : {skill4.skillName}" +
            $"\n소비 마나 : {skill4.skillCost}\n효과 : {skill4.skillEffect}" +
            $"\n스킬 설명 : {skill4.skillDescription}";
        skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txtColor = new Color(0, 0, 0);
        skillNum4.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = "";
        skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player.NowMp >= skill4.skillCost)
        {
            player.NowMp -= skill4.skillCost;

            actionSystem.skillNum4 = true;

            txtColor = new Color(0, 0, 0);
            skillNum4.GetComponent<TextMeshProUGUI>().color = txtColor;
            description.text = "";
            skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;

            EffectSoundManager.instance.MainmenuOn();
        }

        else if (player.NowMp < skill4.skillCost)
        {
            return;
        }
    }
}
