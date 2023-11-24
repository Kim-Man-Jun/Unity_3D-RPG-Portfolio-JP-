using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static CombineListSO;

public class CombineList : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("recipe List")]
    public Image itemImage;
    public TMP_Text itemTitle;

    [Header("Item Thumbnail")]
    public Image thumnailImage;
    public TMP_Text thumnailName;
    public TMP_Text thumnailDescription;

    [Header("Item Material")]
    public TMP_Text combineMaterialName1;
    public TMP_Text combineMaterialName2;
    public TMP_Text combineMaterialName3;
    public TMP_Text combineMaterialName4;

    //����Ʈ���� ���� ������Ʈ
    public RectTransform content;

    //���� ����Ʈ SO ��������
    public CombineListSO listSO;

    public List<GameObject> recipeList = new List<GameObject>();

    public AlchemyInven alchemyInven;

    //���콺 Ŭ�� �׼�
    public event Action<CombineList> OnItemLeftClicked;

    private void Awake()
    {
        Deselect();
    }

    public void Deselect()
    {
        //�� ����
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }

    private void Start()
    {
        itemTitle.text = listSO.title;
        itemImage.sprite = listSO.itemImage;
    }

    //����â ��� �̹��� �� ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(255, 0, 0);

        switch (itemTitle.text)
        {
            case "����":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(0);

                AlchemySystem.alchemyNodeNumber = 3;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "������":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(1);

                AlchemySystem.alchemyNodeNumber = 4;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "ü�� ����":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(2);

                AlchemySystem.alchemyNodeNumber = 3;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "���� ����":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(3);

                AlchemySystem.alchemyNodeNumber = 3;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "���� ����":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(4);

                AlchemySystem.alchemyNodeNumber = 4;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "��� ���� �ϵ�":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(5);

                AlchemySystem.alchemyNodeNumber = 4;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "������ ����":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(6);

                AlchemySystem.alchemyNodeNumber = 3;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "�׳�����":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(7);

                AlchemySystem.alchemyNodeNumber = 4;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;
        }
    }

    private void ThumnailChange()
    {
        thumnailImage.sprite = listSO.itemImage;
        thumnailName.text = listSO.title;
        thumnailDescription.text = listSO.description;
    }

    //CombineMaterial�� �ִ� enum�� ������� enum���� string���� ����
    public void MaterialChange(CombineMaterial combineMaterial1, CombineMaterial combineMaterial2
        , CombineMaterial combineMaterial3, CombineMaterial combineMaterial4)
    {
        combineMaterialName1.text = GetStringForMaterial(combineMaterial1);
        combineMaterialName2.text = GetStringForMaterial(combineMaterial2);
        combineMaterialName3.text = GetStringForMaterial(combineMaterial3);
        combineMaterialName4.text = GetStringForMaterial(combineMaterial4);
    }

    //�� ������ ����Ʈ �� ������ �׸� ����
    public void RecipeChange(int recipeNum)
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            recipeList[i].SetActive(false);
        }

        switch (recipeNum)
        {
            case 0:
                recipeList[0].SetActive(true);
                return;
            case 1:
                recipeList[1].SetActive(true);
                return;
            case 2:
                recipeList[2].SetActive(true);
                return;
            case 3:
                recipeList[3].SetActive(true);
                return;
            case 4:
                recipeList[4].SetActive(true);
                return;
            case 5:
                recipeList[5].SetActive(true);
                return;
            case 6:
                recipeList[6].SetActive(true);
                return;
            case 7:
                recipeList[7].SetActive(true);
                return;
        }
    }

    public static string GetStringForMaterial(CombineMaterial combineMaterial)
    {
        switch (combineMaterial)
        {
            case CombineMaterial.none:
                return "-";
            case CombineMaterial.water:
                return "�� ����";
            case CombineMaterial.plant:
                return "�Ĺ� ����";
            case CombineMaterial.stone:
                return "�� ����";
            case CombineMaterial.ore:
                return "���� ����";
            case CombineMaterial.fuel:
                return "����";
            case CombineMaterial.monster:
                return "���� ����";
            case CombineMaterial.mystery:
                return "�ź��� ��";
            default:
                return "";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }

    //alchemySystem�� itemSelection���� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            EffectSoundManager.instance.MainmenuOn();
            OnItemLeftClicked?.Invoke(this);
            this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }
}
