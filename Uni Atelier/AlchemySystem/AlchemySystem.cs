using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlchemySystem : MonoBehaviour
{
    //연금술 Panel Object On/Off
    public static bool alchemySystemOnOff;
    //연금술 Combine Panel On/Off
    public static bool alchemyCombineOnOff;
    //조합법 노드 숫자 3에서 4까지
    public static int alchemyNodeNumber;
    //조합 실행
    public static bool alchemyCombineComplete;

    public TMP_Text recipeChange;
    public List<CombineList> combineList;

    //임시 내부 string 
    string recipeChoice = "레시피 선택";
    string materialInput = "재료투입";

    [Header("selectUI Collection")]
    public GameObject listPanel;
    public GameObject CombineText;
    public GameObject ItemInfo;
    public GameObject MaterialList;

    [Header("combineUI Collection")]
    public GameObject CombineInvenDisplay;

    public List<NodeController> nodeList;

    //각 노드 선택 시
    [Header("nodeStart Complete")]
    public bool node1Start;
    public bool node2Start;
    public bool node3Start;
    public bool node4Start;

    //노드에 재료가 채워졌을 때
    public bool node1Complete;
    public bool node2Complete;
    public bool node3Complete;
    public bool node4Complete;

    //노드 단계별 int값
    public int nodeCount;
    //3개 섞어서 조합하는지 4개 섞어서 조합하는지
    public int nodeDivide = 0;

    [Header("combineCondition")]
    //노드에 재료 집어넣는 정도에 따라 수치 및 컬러 변경하기
    public float combineConditionValue;
    public TMP_Text combineConditionEffect;
    public TMP_Text combineConditionElement;
    public string elementName;

    [Header("Alchemy Inven")]
    //연금술 인벤토리 가져오기;
    public AlchemyInven alchemyInven;

    [Header("Alchemy Specificity Expand")]
    //특성 확장창 Panel
    public GameObject specificityExpandPanel;
    //재료 아이템 특성 전체 리스트화
    public List<string> alchemySpecificityExpand = new List<string>();
    //특성 확장창 선택용 프리팹
    public specificityExpandList specPrefab;
    //특성 확장창 선택용 프리팹 생성 위치
    public RectTransform content;
    //특성 선택 정도
    public int clickNumber;

    [Header("Alchemy Combine Complete")]
    public GameObject completePanel;

    private void Awake()
    {
        completePanel.SetActive(false);
    }

    void Start()
    {
        for (int i = 0; i < combineList.Count; i++)
        {
            combineList[i].OnItemLeftClicked += itemSelection;
        }

        for (int i = 0; i < nodeList.Count; i++)
        {
            nodeList[i].alchemyCombineClick += AlchemySystem_alchemyCombineClick;
            nodeList[i].alchemyCombineNodeOut += AlchemySystem_alchemyCombineNodeOut;
        }
    }

    void Update()
    {
        //노드별 왼쪽에 있는 재료 투입해야 하는 수량
        //그것이 수량에 맞춰 들어갔을 때
        if (combineConditionValue <= 0)
        {
            if (node1Start == true && nodeDivide == 0)
            {
                node1Complete = true;
                nodeDivide = 1;
                alchemyInven.combineEffect1.text = combineConditionEffect.text;

                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity1.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity2.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity3.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity4.text);

                EffectSoundManager.instance.NodeComplete();
            }

            if (node2Start == true && nodeDivide == 1)
            {
                node2Complete = true;
                nodeDivide = 2;
                alchemyInven.combineEffect2.text = combineConditionEffect.text;

                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity1.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity2.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity3.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity4.text);

                EffectSoundManager.instance.NodeComplete();
            }

            if (node3Start == true && nodeDivide == 2)
            {
                node3Complete = true;
                nodeDivide = 3;
                alchemyInven.combineEffect3.text = combineConditionEffect.text;

                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity1.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity2.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity3.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity4.text);

                EffectSoundManager.instance.NodeComplete();

                //combineList에 있는 static 조합법에 따른 노드 숫자
                //필요한 노드 숫자가 3개일 경우, 특성 고르기 및 조합 실행
                if (alchemyNodeNumber == 3)
                {
                    //중복값 제거 후 특성 확장창으로 이동
                    List<string> uniqueList = alchemySpecificityExpand.Distinct().ToList();

                    for (int i = 0; i < uniqueList.Count; i++)
                    {
                        string alchemySpecificityList = uniqueList[i];

                        specificityExpandList spec = Instantiate(specPrefab, Vector3.zero, Quaternion.identity);

                        spec.transform.SetParent(content);

                        TMP_Text textComponent = spec.transform.GetChild(0).GetComponent<TMP_Text>();

                        if (textComponent != null)
                        {
                            textComponent.text = alchemySpecificityList;
                        }

                        //specificityExpandList에서 이어받음
                        spec.specificityExpandClick += Spec_specificityExpandClick;
                    }

                    alchemyInven.specificityExpand.SetActive(true);
                }
            }

            if (node4Start == true && nodeDivide == 3)
            {
                node4Complete = true;
                nodeDivide = 4;
                alchemyInven.combineEffect4.text = combineConditionEffect.text;

                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity1.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity2.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity3.text);
                alchemySpecificityExpand.Add(alchemyInven.materialSpecificity4.text);

                EffectSoundManager.instance.NodeComplete();

                if (alchemyNodeNumber == 4)
                {
                    //특성 중복값 제거
                    List<string> uniqueList = alchemySpecificityExpand.Distinct().ToList();

                    for (int i = 0; i < uniqueList.Count; i++)
                    {
                        string alchemySpecificityList = uniqueList[i];

                        specificityExpandList spec = Instantiate(specPrefab, Vector3.zero, Quaternion.identity);

                        spec.transform.SetParent(content);

                        TMP_Text textComponent = spec.transform.GetChild(0).GetComponent<TMP_Text>();

                        if (textComponent != null)
                        {
                            textComponent.text = alchemySpecificityList;
                        }

                        spec.specificityExpandClick += Spec_specificityExpandClick;
                    }

                    alchemyInven.specificityExpand.SetActive(true);
                }
            }
        }
    }

    //특성 확장창에 초기화
    public void specificityExpandListDelete()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

    //중간에 취소했을 때 전부 초기화
    public void alchemyPlayingStop()
    {
        if (node1Start == true)
        {
            //각종 변수들 false
            node1Start = false;
            node2Start = false;
            node3Start = false;
            node4Start = false;

            node1Complete = false;
            node2Complete = false;
            node3Complete = false;
            node4Complete = false;

            nodeDivide = 0;
            nodeCount = 0;

            //노드 눌렀을 때 나오는 outline 해제
            for (int i = 0; i < nodeList.Count; i++)
            {
                nodeList[i].GetComponent<Outline>().enabled = false;
            }

            for (int i = 0; i < alchemyInven.selectedItem.Count; i++)
            {
                alchemyInven.selectedItem[i].Deselect();
            }

            //설명창 초기화
            alchemyInven.combineImage.sprite = alchemyInven.nullImage;
            alchemyInven.combineTitle.text = "-";
            alchemyInven.combineQuality.text = "-";
            alchemyInven.combineEffect1.text = "-";
            alchemyInven.combineEffect2.text = "-";
            alchemyInven.combineEffect3.text = "-";
            alchemyInven.combineEffect4.text = "-";
            alchemyInven.combineSpecificity1.text = "-";
            alchemyInven.combineSpecificity2.text = "-";
            alchemyInven.combineSpecificity3.text = "-";
            alchemyInven.combineSpecificity4.text = "-";

            //아이템 선택 하이라이트 초기화
            alchemyInven.invenItemPrefab.Deselect();

            //노드에 투입됐던 재료아이템 리스트 클리어
            alchemyInven.selectedItem.Clear();
            //노드 완성하면서 투입되었던 특성 리스트 클리어
            alchemySpecificityExpand.Clear();

            //특성 선택창까지 갔을 때 취소했을 경우 초기화
            specificityExpandListDelete();
            specificityExpandPanel.SetActive(false);
        }
    }

    //재료들을 선택하면서 중복됐던 특성들을 지우고 특성 4개를 선택
    private void Spec_specificityExpandClick(specificityExpandList specExpand)
    {
        if (clickNumber == 0)
        {
            alchemyInven.combineSpecificity1.text
                = specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text;
            clickNumber++;

            EffectSoundManager.instance.CombineStart();
        }

        if (clickNumber == 1)
        {
            //만약 누른 텍스트가 처음 선택한 텍스트와 같을 경우 return 처리
            if (specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text
                == alchemyInven.combineSpecificity1.text)
            {
                return;
            }

            else
            {
                alchemyInven.combineSpecificity2.text
                    = specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text;
                clickNumber++;

                EffectSoundManager.instance.CombineStart();
            }
        }

        if (clickNumber == 2)
        {
            if ((specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text
                == alchemyInven.combineSpecificity1.text) || (specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text
                == alchemyInven.combineSpecificity2.text))
            {
                return;
            }
            else
            {
                alchemyInven.combineSpecificity3.text
                    = specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text;
                clickNumber++;
                EffectSoundManager.instance.CombineStart();

            }
        }

        if (clickNumber == 3)
        {
            if ((specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text
                == alchemyInven.combineSpecificity1.text) || (specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text
                == alchemyInven.combineSpecificity2.text) || (specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text
                == alchemyInven.combineSpecificity3.text))
            {
                return;
            }
            else
            {
                alchemyInven.combineSpecificity4.text
                    = specExpand.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text;
                clickNumber++;

                //특성 4개까지 선택 시 조합과정 완료처리
                alchemyCombineComplete = true;
                completePanel.SetActive(true);

                EffectSoundManager.instance.CombineStart();
            }
        }

        if (clickNumber == 4)
        {
            return;
        }
    }

    //combineList에 있는 리스트 중 하나를 눌렀을 경우
    public void itemSelection(CombineList list)
    {
        //연금술 화면에 노드들 등장
        alchemyNodeOn();
    }

    public void alchemyNodeOn()
    {
        if (alchemyCombineOnOff == false)
        {
            //UI 노드화면으로 교체
            recipeChange.text = materialInput;

            listPanel.SetActive(false);
            CombineText.SetActive(false);
            ItemInfo.SetActive(false);
            MaterialList.SetActive(false);
            alchemyCombineOnOff = true;
            CombineInvenDisplay.SetActive(true);
        }
    }

    public void itemDeselection()
    {
        alchemyNodeOff();
    }

    public void alchemyNodeOff()
    {
        if (alchemyCombineOnOff == true)
        {
            //UI 첫화면으로 교체
            recipeChange.text = recipeChoice;

            CombineInvenDisplay.SetActive(false);

            listPanel.SetActive(true);
            CombineText.SetActive(true);
            ItemInfo.SetActive(true);
            MaterialList.SetActive(true);

            alchemyCombineOnOff = false;

            alchemyInven.DeselectAllItems();
            alchemyInven.ResetDescription();
        }
    }

    //노드들을 클릭했을 때
    private void AlchemySystem_alchemyCombineClick(NodeController nodeList)
    {
        //노드 단계에 따라 다른 노드들을 클릭했을 경우 return 처리
        if ((nodeCount == 0 && (nodeList.nodeName == "AlchemyNode2" || nodeList.nodeName == "AlchemyNode3" || nodeList.nodeName == "AlchemyNode4"))
    || (nodeCount == 1 && (nodeList.nodeName == "AlchemyNode3" || nodeList.nodeName == "AlchemyNode4"))
    || (nodeCount == 2 && nodeList.nodeName == "AlchemyNode4"))
        {
            return;
        }

        if (nodeCount == 0 && nodeList.nodeName == "AlchemyNode1" &&
            node1Start == false)
        {
            node1Start = true;
            nodeCount++;

            Node_AlchemyChange(nodeList);
        }
        else if (nodeCount == 1 && nodeList.nodeName == "AlchemyNode2"
            && node2Start == false && node1Complete == true)
        {
            node2Start = true;
            nodeCount++;

            Node_AlchemyChange(nodeList);
        }
        else if (nodeCount == 2 && nodeList.nodeName == "AlchemyNode3"
            && node3Start == false && node2Complete == true)
        {
            node3Start = true;
            nodeCount++;

            Node_AlchemyChange(nodeList);
        }
        else if (nodeCount == 3 && nodeList.nodeName == "AlchemyNode4"
            && node4Start == false && node3Complete == true)
        {
            node4Start = true;
            nodeCount++;

            Node_AlchemyChange(nodeList);
        }

        if (node1Start == true && nodeList.nodeName == "AlchemyNode1")
        {
            Debug.Log("재료1 투입 시퀸스 돌입");
        }
        else if (node2Start == true && nodeList.nodeName == "AlchemyNode2")
        {
            Debug.Log("재료2 투입 시퀸스 돌입");
        }
        else if (node3Start == true && nodeList.nodeName == "AlchemyNode3")
        {
            Debug.Log("재료3 투입 시퀸스 돌입");
        }
        else if (node4Start == true && nodeList.nodeName == "AlchemyNode4")
        {
            Debug.Log("재료4 투입 시퀸스 돌입");
        }
    }

    public void AlchemySystem_alchemyCombineNodeOut(NodeController nodeList)
    {
        if ((nodeList.name == "AlchemyNode1" && node1Start == true) ||
            (nodeList.name == "AlchemyNode2" && node2Start == true) ||
            (nodeList.name == "AlchemyNode3" && node3Start == true) ||
            (nodeList.name == "AlchemyNode4" && node4Start == true))
        {
            return;
        }

        nodeList.nodeOutline.enabled = false;
    }

    public void Node_AlchemyChange(NodeController nodeList)
    {
        //노드 1, 2, 3, 4 구별용
        elementName = nodeList.nodeElementName.text;
        combineConditionValue = nodeList.nodeInsertQuantity;
        combineConditionEffect = nodeList.nodeEffectAdding;
        combineConditionElement = nodeList.nodeElementlAdding;
    }
}
