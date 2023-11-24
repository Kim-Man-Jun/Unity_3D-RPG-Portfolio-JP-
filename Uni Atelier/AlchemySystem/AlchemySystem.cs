using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlchemySystem : MonoBehaviour
{
    //���ݼ� Panel Object On/Off
    public static bool alchemySystemOnOff;
    //���ݼ� Combine Panel On/Off
    public static bool alchemyCombineOnOff;
    //���չ� ��� ���� 3���� 4����
    public static int alchemyNodeNumber;
    //���� ����
    public static bool alchemyCombineComplete;

    public TMP_Text recipeChange;
    public List<CombineList> combineList;

    //�ӽ� ���� string 
    string recipeChoice = "������ ����";
    string materialInput = "�������";

    [Header("selectUI Collection")]
    public GameObject listPanel;
    public GameObject CombineText;
    public GameObject ItemInfo;
    public GameObject MaterialList;

    [Header("combineUI Collection")]
    public GameObject CombineInvenDisplay;

    public List<NodeController> nodeList;

    //�� ��� ���� ��
    [Header("nodeStart Complete")]
    public bool node1Start;
    public bool node2Start;
    public bool node3Start;
    public bool node4Start;

    //��忡 ��ᰡ ä������ ��
    public bool node1Complete;
    public bool node2Complete;
    public bool node3Complete;
    public bool node4Complete;

    //��� �ܰ躰 int��
    public int nodeCount;
    //3�� ��� �����ϴ��� 4�� ��� �����ϴ���
    public int nodeDivide = 0;

    [Header("combineCondition")]
    //��忡 ��� ����ִ� ������ ���� ��ġ �� �÷� �����ϱ�
    public float combineConditionValue;
    public TMP_Text combineConditionEffect;
    public TMP_Text combineConditionElement;
    public string elementName;

    [Header("Alchemy Inven")]
    //���ݼ� �κ��丮 ��������;
    public AlchemyInven alchemyInven;

    [Header("Alchemy Specificity Expand")]
    //Ư�� Ȯ��â Panel
    public GameObject specificityExpandPanel;
    //��� ������ Ư�� ��ü ����Ʈȭ
    public List<string> alchemySpecificityExpand = new List<string>();
    //Ư�� Ȯ��â ���ÿ� ������
    public specificityExpandList specPrefab;
    //Ư�� Ȯ��â ���ÿ� ������ ���� ��ġ
    public RectTransform content;
    //Ư�� ���� ����
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
        //��庰 ���ʿ� �ִ� ��� �����ؾ� �ϴ� ����
        //�װ��� ������ ���� ���� ��
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

                //combineList�� �ִ� static ���չ��� ���� ��� ����
                //�ʿ��� ��� ���ڰ� 3���� ���, Ư�� ���� �� ���� ����
                if (alchemyNodeNumber == 3)
                {
                    //�ߺ��� ���� �� Ư�� Ȯ��â���� �̵�
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

                        //specificityExpandList���� �̾����
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
                    //Ư�� �ߺ��� ����
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

    //Ư�� Ȯ��â�� �ʱ�ȭ
    public void specificityExpandListDelete()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

    //�߰��� ������� �� ���� �ʱ�ȭ
    public void alchemyPlayingStop()
    {
        if (node1Start == true)
        {
            //���� ������ false
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

            //��� ������ �� ������ outline ����
            for (int i = 0; i < nodeList.Count; i++)
            {
                nodeList[i].GetComponent<Outline>().enabled = false;
            }

            for (int i = 0; i < alchemyInven.selectedItem.Count; i++)
            {
                alchemyInven.selectedItem[i].Deselect();
            }

            //����â �ʱ�ȭ
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

            //������ ���� ���̶���Ʈ �ʱ�ȭ
            alchemyInven.invenItemPrefab.Deselect();

            //��忡 ���Եƴ� �������� ����Ʈ Ŭ����
            alchemyInven.selectedItem.Clear();
            //��� �ϼ��ϸ鼭 ���ԵǾ��� Ư�� ����Ʈ Ŭ����
            alchemySpecificityExpand.Clear();

            //Ư�� ����â���� ���� �� ������� ��� �ʱ�ȭ
            specificityExpandListDelete();
            specificityExpandPanel.SetActive(false);
        }
    }

    //������ �����ϸ鼭 �ߺ��ƴ� Ư������ ����� Ư�� 4���� ����
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
            //���� ���� �ؽ�Ʈ�� ó�� ������ �ؽ�Ʈ�� ���� ��� return ó��
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

                //Ư�� 4������ ���� �� ���հ��� �Ϸ�ó��
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

    //combineList�� �ִ� ����Ʈ �� �ϳ��� ������ ���
    public void itemSelection(CombineList list)
    {
        //���ݼ� ȭ�鿡 ���� ����
        alchemyNodeOn();
    }

    public void alchemyNodeOn()
    {
        if (alchemyCombineOnOff == false)
        {
            //UI ���ȭ������ ��ü
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
            //UI ùȭ������ ��ü
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

    //������ Ŭ������ ��
    private void AlchemySystem_alchemyCombineClick(NodeController nodeList)
    {
        //��� �ܰ迡 ���� �ٸ� ������ Ŭ������ ��� return ó��
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
            Debug.Log("���1 ���� ������ ����");
        }
        else if (node2Start == true && nodeList.nodeName == "AlchemyNode2")
        {
            Debug.Log("���2 ���� ������ ����");
        }
        else if (node3Start == true && nodeList.nodeName == "AlchemyNode3")
        {
            Debug.Log("���3 ���� ������ ����");
        }
        else if (node4Start == true && nodeList.nodeName == "AlchemyNode4")
        {
            Debug.Log("���4 ���� ������ ����");
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
        //��� 1, 2, 3, 4 ������
        elementName = nodeList.nodeElementName.text;
        combineConditionValue = nodeList.nodeInsertQuantity;
        combineConditionEffect = nodeList.nodeEffectAdding;
        combineConditionElement = nodeList.nodeElementlAdding;
    }
}
