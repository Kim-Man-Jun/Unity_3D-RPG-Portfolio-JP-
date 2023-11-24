using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class skillNum2Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject skillNum2;

    public GameObject skillDescription;

    private Color txtColor;

    private TextMeshProUGUI description;

    private skillclass skill2 = new skillclass();

    public actionSystem actionSystem;

    public Player player;

    private void Start()
    {
        txtColor = skillNum2.GetComponent<TextMeshProUGUI>().color;
        description = skillDescription.GetComponent<TextMeshProUGUI>();

        skill2.skillName = "오러 차지";
        skill2.skillCost = 12;
        skill2.skillEffect = "자신의 공격력을 증가 시킨다";
        skill2.skillDescription = "몸 안의 오러를 모아 힘을 증가시킨다.";
        skill2.skillmagnification = 1.5f;
        skill2.skillSpecify = 2;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        txtColor = new Color(255, 0, 0);
        skillNum2.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = $"스킬명 : {skill2.skillName}" +
            $"\n소비 마나 : {skill2.skillCost}\n효과 : {skill2.skillEffect}" +
            $"\n스킬 설명 : {skill2.skillDescription}";
        skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txtColor = new Color(0, 0, 0);
        skillNum2.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = "";
        skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player.NowMp >= skill2.skillCost)
        {
            player.NowMp -= skill2.skillCost;

            actionSystem.skillNum2 = true;

            txtColor = new Color(0, 0, 0);
            skillNum2.GetComponent<TextMeshProUGUI>().color = txtColor;
            description.text = "";
            skillDescription.GetComponent<TextMeshProUGUI>().text = description.text;

            EffectSoundManager.instance.MainmenuOn();
        }

        else if (player.NowMp < skill2.skillCost)
        {
            return;
        }
    }
}
