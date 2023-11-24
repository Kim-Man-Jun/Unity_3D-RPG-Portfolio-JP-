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

        skill5.skillName = "ü�� ũ��Ƽ��";
        skill5.skillCost = 24;
        skill5.skillEffect = "�� ���� ��󿡰� ū ������";
        skill5.skillDescription = "�� ��ü�� ����� ���ں��� ����.";
        skill5.skillmagnification = 5;
        skill5.skillSpecify = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        txtColor = new Color(255, 0, 0);
        skillNum5.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = $"��ų�� : {skill5.skillName}" +
            $"\n�Һ� ���� : {skill5.skillCost}\nȿ�� : {skill5.skillEffect}" +
            $"\n��ų ���� : {skill5.skillDescription}";

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
