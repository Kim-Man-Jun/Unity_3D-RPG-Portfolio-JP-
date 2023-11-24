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

    //리스트들이 들어가는 오브젝트
    public RectTransform content;

    //조합 리스트 SO 가져오기
    public CombineListSO listSO;

    public List<GameObject> recipeList = new List<GameObject>();

    public AlchemyInven alchemyInven;

    //마우스 클릭 액션
    public event Action<CombineList> OnItemLeftClicked;

    private void Awake()
    {
        Deselect();
    }

    public void Deselect()
    {
        //색 조정
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }

    private void Start()
    {
        itemTitle.text = listSO.title;
        itemImage.sprite = listSO.itemImage;
    }

    //조합창 노드 이미지 등 변경
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(255, 0, 0);

        switch (itemTitle.text)
        {
            case "프람":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(0);

                AlchemySystem.alchemyNodeNumber = 3;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "프람베":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(1);

                AlchemySystem.alchemyNodeNumber = 4;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "체력 물약":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(2);

                AlchemySystem.alchemyNodeNumber = 3;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "마나 물약":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(3);

                AlchemySystem.alchemyNodeNumber = 3;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "종합 물약":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(4);

                AlchemySystem.alchemyNodeNumber = 4;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "루비 박힌 완드":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(5);

                AlchemySystem.alchemyNodeNumber = 4;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "가벼운 갑옷":
                ThumnailChange();

                MaterialChange(listSO.material1, listSO.material2,
                    listSO.material3, listSO.material4);

                RecipeChange(6);

                AlchemySystem.alchemyNodeNumber = 3;

                alchemyInven.combineImage.sprite = thumnailImage.sprite;
                alchemyInven.combineTitle.text = thumnailName.text;

                return;

            case "그나데링":
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

    //CombineMaterial에 있는 enum을 기반으로 enum에서 string으로 변경
    public void MaterialChange(CombineMaterial combineMaterial1, CombineMaterial combineMaterial2
        , CombineMaterial combineMaterial3, CombineMaterial combineMaterial4)
    {
        combineMaterialName1.text = GetStringForMaterial(combineMaterial1);
        combineMaterialName2.text = GetStringForMaterial(combineMaterial2);
        combineMaterialName3.text = GetStringForMaterial(combineMaterial3);
        combineMaterialName4.text = GetStringForMaterial(combineMaterial4);
    }

    //각 아이템 리스트 별 레시피 그림 변경
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
                return "물 소재";
            case CombineMaterial.plant:
                return "식물 소재";
            case CombineMaterial.stone:
                return "돌 소재";
            case CombineMaterial.ore:
                return "광물 소재";
            case CombineMaterial.fuel:
                return "연료";
            case CombineMaterial.monster:
                return "몬스터 소재";
            case CombineMaterial.mystery:
                return "신비한 힘";
            default:
                return "";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }

    //alchemySystem의 itemSelection으로 전달
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
