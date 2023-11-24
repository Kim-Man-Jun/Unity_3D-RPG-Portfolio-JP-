using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class skillNum1Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject skillNum1;

    public GameObject skillDescription;

    private Color txtColor;

    private TMP_Text description;

    private skillclass skill1 = new skillclass();

    public actionSystem actionSystem;

    public Player player;

    private void Start()
    {
        txtColor = skillNum1.GetComponent<TMP_Text>().color;
        description = skillDescription.GetComponent<TMP_Text>();

        skill1.skillName = "오러 레이저";
        skill1.skillCost = 8;
        skill1.skillEffect = "적 단일 대상에게 데미지";
        skill1.skillDescription = "지팡이 끝에서 레이저를 발사한다.";
        skill1.skillmagnification = 1.5f;
        skill1.skillSpecify = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        txtColor = new Color(255, 0, 0);
        skillNum1.GetComponent<TMP_Text>().color = txtColor;
        description.text = $"스킬명 : {skill1.skillName}" +
            $"\n소비 마나 : {skill1.skillCost}\n효과 : {skill1.skillEffect}" +
            $"\n스킬 설명 : {skill1.skillDescription}";
        skillDescription.GetComponent<TMP_Text>().text = description.text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txtColor = new Color(0, 0, 0);
        skillNum1.GetComponent<TMP_Text>().color = txtColor;
        description.text = "";
        skillDescription.GetComponent<TMP_Text>().text = description.text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player.NowMp >= skill1.skillCost)
        {
            player.NowMp -= skill1.skillCost;

            actionSystem.skillNum1 = true;

            txtColor = new Color(0, 0, 0);
            skillNum1.GetComponent<TMP_Text>().color = txtColor;
            description.text = "";
            skillDescription.GetComponent<TMP_Text>().text = description.text;

            EffectSoundManager.instance.MainmenuOn();
        }

        else if (player.NowMp < skill1.skillCost)
        {
            return;
        }
    }
}
