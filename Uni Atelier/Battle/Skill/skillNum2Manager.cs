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

        skill2.skillName = "���� ����";
        skill2.skillCost = 12;
        skill2.skillEffect = "�ڽ��� ���ݷ��� ���� ��Ų��";
        skill2.skillDescription = "�� ���� ������ ��� ���� ������Ų��.";
        skill2.skillmagnification = 1.5f;
        skill2.skillSpecify = 2;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        txtColor = new Color(255, 0, 0);
        skillNum2.GetComponent<TextMeshProUGUI>().color = txtColor;
        description.text = $"��ų�� : {skill2.skillName}" +
            $"\n�Һ� ���� : {skill2.skillCost}\nȿ�� : {skill2.skillEffect}" +
            $"\n��ų ���� : {skill2.skillDescription}";
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
