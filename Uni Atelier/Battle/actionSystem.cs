using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class actionSystem : MonoBehaviour
{
    //�÷��̾� ���� ���
    public enum playerStateList
    {
        Idle,
        Attack,
        Skill,
        Item,
        Guard,
        Run
    };

    public string playerCurrent = Enum.GetName(typeof(playerStateList), 0);

    [Header("Camera Related")]
    public GameObject battleMainCamera;
    public GameObject battlePlayerCamera;
    public GameObject battleVictoryCamera;

    [Header("Action Gauge Related")]
    public actionGaugeManager actionGaugeManager;

    [Header("BattleUI Panel Related")]
    //���� ȭ�� ī�޶�
    public GameObject battleMain;
    //�÷��̾� ī�޶�
    public GameObject battlePlayer;
    //���� �¸� ī�޶�
    public GameObject battleVictory;

    [Header("Player State Related")]
    public TMP_Text playerHpBartxt;
    public TMP_Text playerMpBartxt;
    public Image playerHpBarImg;
    public Image playerMpBarImg;

    [Header("Enemy Related")]
    GameObject player = null;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    Transform enemy1_attackPos;
    Transform enemy2_attackPos;
    Transform enemy3_attackPos;

    [Header("Transform Related")]
    public Transform playerPos;
    public Transform enemyPos1;
    public Transform enemyPos2;
    public Transform enemyPos3;

    [Header("Attack Related")]
    //���ݹ�ư ������ ��
    public bool action_AttackStart = false;
    //������ ��ġ�� �������� ��
    public bool action_movingStop;

    [Header("Enemy Select Related")]
    //�� ������ �� 2�� �Ǹ� ���� ����
    public int enemySelect = 0;
    //�� ������ �� �ӽ� ���, �� �� ° ������ ����� ������ ���� ��� ���� ����
    //�ƴ� ��� �ʱ�ȭ
    public GameObject temporaryGO;
    //Ŀ�� ������Ʈ
    public GameObject enemySelectCursor;
    GameObject cursor;

    //��ü ���ݿ�
    GameObject cursor1;
    GameObject cursor2;
    GameObject cursor3;

    [Header("Skill Related")]
    public GameObject skillPanel;
    public Transform skillPos;

    [Header("Skill1 Related")]
    public bool skillNum1 = false;
    public bool action_skill1Start = false;

    [Header("Skill2 Related")]
    public bool skillNum2 = false;
    public bool action_skill2Start = false;

    [Header("Skill3 Related")]
    public bool skillNum3 = false;
    public bool action_skill3Start = false;

    [Header("Skill4 Related")]
    public bool skillNum4 = false;
    public bool action_skill4Start = false;

    [Header("Skill5 Related")]
    public bool skillNum5 = false;
    public bool action_skill5Start = false;
    public GameObject skill5_Dash;

    [Header("Item Related")]
    public GameObject itemPanel;
    public inBattleInventory inBattleInventory;
    public bool attackSingleItem = false;
    public bool attackGroupItem = false;
    public bool restoreItem = false;

    public bool action_ItemStart = false;

    public string itemUseTitle;
    public int itemValue;

    [Header("Guard Related")]
    public bool action_Guard = false;

    [Header("Run Related")]
    public bool action_Run = false;

    [Header("Victory Related")]
    public bool battle_Victory = false;

    [Header("Defeat Related")]
    public bool battle_Defeat = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        enemy1_attackPos = enemyPos1.GetChild(0);
        enemy2_attackPos = enemyPos2.GetChild(0);
        enemy3_attackPos = enemyPos3.GetChild(0);

        inBattleInventory.inBattleItemActionStart += InBattleInventory_inBattleItemActionStart;
    }

    public void InBattleInventory_inBattleItemActionStart(inBattleInventory itemUse)
    {
        //���� ������ ���� �غ�
        if (itemUse.atkSingleItemUse == true)
        {
            attackSingleItem = true;

            itemUseTitle = itemUse.inBattleItemTitle.text;
            int.TryParse(itemUse.inBattleItemDmgResValue.text, out itemValue);
        }

        if (itemUse.atkGroupItemUse == true)
        {
            attackGroupItem = true;

            itemUseTitle = itemUse.inBattleItemTitle.text;
            int.TryParse(itemUse.inBattleItemDmgResValue.text, out itemValue);
        }

        //ȸ�� ������ ���� �غ�
        if (itemUse.resItemUse == true)
        {
            restoreItem = true;

            itemUseTitle = itemUse.inBattleItemTitle.text;
            int.TryParse(itemUse.inBattleItemDmgResValue.text, out itemValue);
        }
    }

    private void Update()
    {
        //�÷��̾� HP, MP
        playerHpMpState();

        //�� ���� ���� ���� raycast
        if (playerCurrent == "Attack" && action_AttackStart == false)
        {
            enemyMouseSelect();
            actionGaugeManager.playerActionSwitch = true;
        }

        //ī�޶� ��ȯ
        Main_BattleCameraManager(actionGaugeManager.playerActionOn);

        if (playerCurrent != "Idle")
        {
            GameObject go = GameObject.Find("HighlightPointer(Clone)");

            Destroy(go);
        }

        //�÷��̾� ���� attack�϶�
        if (playerCurrent == Enum.GetName(typeof(playerStateList), 1))
        {
            //���� ��ư ����
            if (temporaryGO != null)
            {
                action_AttackButton_On(temporaryGO);
            }

            //���� ��ư ������ ����
            action_AttackButton_Off();
        }

        //�� ���ϴ�� ����
        if (skillNum1 == true || skillNum5 == true)
        {
            playerCurrent = Enum.GetName(typeof(playerStateList), 2);

            skillPanel.SetActive(false);

            actionGaugeManager.playerActionOn = false;
            actionGaugeManager.playerActionSwitch = true;

            enemyMouseSelect();
        }

        //�÷��̾� �ڽ� ����
        if (skillNum2 == true || skillNum4 == true)
        {
            playerCurrent = Enum.GetName(typeof(playerStateList), 2);

            skillPanel.SetActive(false);

            actionGaugeManager.playerActionOn = false;
            actionGaugeManager.playerActionSwitch = true;

            enemyMouseSelect();
        }

        //�� ��ü�� �����ϵ��� �����
        if (skillNum3 == true)
        {
            playerCurrent = Enum.GetName(typeof(playerStateList), 2);

            skillPanel.SetActive(false);

            actionGaugeManager.playerActionOn = false;
            actionGaugeManager.playerActionSwitch = true;

            enemyMouseSelect();
        }

        //������ ���
        if (attackSingleItem == true || attackGroupItem == true || restoreItem == true)
        {
            playerCurrent = Enum.GetName(typeof(playerStateList), 3);

            itemPanel.SetActive(false);

            actionGaugeManager.playerActionOn = false;
            actionGaugeManager.playerActionSwitch = true;

            enemyMouseSelect();
        }

        //����� ���� ���ڿ� óġ�� ���� ���ڰ� ������ ���
        if (SaveLoadSystem.enemyCount == SaveLoadSystem.enemyDiecount)
        {
            battle_Victory = true;
            Main_VictoryCameraManager();
        }

        if (battle_Victory == true)
        {
            battlePlayer.SetActive(false);
        }
    }

    #region playerHP, MP
    private void playerHpMpState()
    {
        int nowHpint = Mathf.RoundToInt(player.GetComponent<Player>().NowHp);
        int nowMpint = Mathf.RoundToInt(player.GetComponent<Player>().NowMp);

        playerHpBartxt.text = $"{nowHpint} / {player.GetComponent<Player>().MaxHp}";
        playerMpBartxt.text = $"{nowMpint} / {player.GetComponent<Player>().MaxMp}";

        playerHpBarImg.fillAmount = nowHpint / player.GetComponent<Player>().MaxHp;
        playerMpBarImg.fillAmount = nowMpint / player.GetComponent<Player>().MaxMp;

        if (player.GetComponent<Player>().NowHp > player.GetComponent<Player>().MaxHp)
        {
            player.GetComponent<Player>().NowHp = (int)player.GetComponent<Player>().MaxHp;
        }

        if (player.GetComponent<Player>().NowMp > player.GetComponent<Player>().MaxMp)
        {
            player.GetComponent<Player>().NowMp = (int)player.GetComponent<Player>().MaxMp;
        }
    }
    #endregion

    public void enemyMouseSelect()
    {
        //�÷��̾ �� ���� ����ĳ��Ʈ
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (skillNum2 == true || skillNum4 == true || restoreItem == true)
        {
            //�÷��̾� �ڽ��� ����
            if (Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Player")) && Input.GetMouseButtonDown(0))
            {
                GameObject go = hit.collider.gameObject;

                if (enemySelect == 0)
                {
                    if (go == player)
                    {
                        Vector3 pos = player.transform.position;
                        pos.y = player.transform.position.y + 2f;

                        cursor = Instantiate(enemySelectCursor, pos, Quaternion.identity);

                        temporaryGO = go;
                        enemySelect++;
                    }
                }

                else if (enemySelect == 1)
                {
                    if (skillNum2 == true)
                    {
                        action_skill2Start = true;
                    }

                    else if (skillNum4 == true)
                    {
                        action_skill4Start = true;
                    }

                    else if (restoreItem == true)
                    {
                        action_ItemStart = true;
                    }

                    Destroy(cursor, 0.3f);
                }
            }
        }

        //���� ���� ��ų�̳� �������� ���
        else
        {
            //���̾ enemy�� ���
            if (Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Enemy")) && Input.GetMouseButtonDown(0))
            {
                GameObject go = hit.collider.gameObject;

                //��ų 3�� ��ü ���ݱ� ������ ��
                if (skillNum3 == true || attackGroupItem == true)
                {
                    if (enemySelect == 0)
                    {
                        if (go == enemy1 || enemy2 || enemy3)
                        {
                            if (enemy1 != null)
                            {
                                Vector3 pos1 = enemy1.transform.position;
                                pos1.y = enemy1.transform.position.y + 2f;
                                cursor1 = Instantiate(enemySelectCursor, pos1, Quaternion.identity);
                            }

                            if (enemy2 != null)
                            {
                                Vector3 pos2 = enemy2.transform.position;
                                pos2.y = enemy2.transform.position.y + 2f;
                                cursor2 = Instantiate(enemySelectCursor, pos2, Quaternion.identity);
                            }

                            if (enemy3 != null)
                            {
                                Vector3 pos3 = enemy3.transform.position;
                                pos3.y = enemy3.transform.position.y + 2f;
                                cursor3 = Instantiate(enemySelectCursor, pos3, Quaternion.identity);
                            }
                        }
                        temporaryGO = go;
                        enemySelect++;
                    }

                    else if (enemySelect == 1)
                    {
                        if (skillNum3 == true)
                        {
                            action_skill3Start = true;
                        }

                        if (attackGroupItem == true)
                        {
                            action_ItemStart = true;
                        }

                        Destroy(cursor1, 1f);
                        Destroy(cursor2, 1f);
                        Destroy(cursor3, 1f);
                    }
                }

                //�Ϲ� ���ݰ� ��ų 1, 5��, ���� ���� ������ ������ ��
                if (enemySelect == 0)
                {
                    if (go == enemy1)
                    {
                        enemySelectCursorPos(enemy1);
                    }
                    else if (go == enemy2)
                    {
                        enemySelectCursorPos(enemy2);
                    }
                    else if (go == enemy3)
                    {
                        enemySelectCursorPos(enemy3);
                    }

                    temporaryGO = go;
                    enemySelect++;
                }

                else if (enemySelect == 1)
                {
                    //���콺 Ŭ���� ���� �ӽ� ������ ����� ���� ��� ���� ����
                    if (enemyChoose(go, temporaryGO) == true)
                    {
                        if (playerCurrent == "Attack")
                        {
                            action_AttackStart = true;
                        }
                        else if (playerCurrent == "Skill" && skillNum1 == true)
                        {
                            action_skill1Start = true;
                        }
                        else if (playerCurrent == "Skill" && skillNum5 == true)
                        {
                            action_skill5Start = true;
                            skill5_Start();
                        }
                        else if (playerCurrent == "Item" && attackSingleItem == true)
                        {
                            action_ItemStart = true;
                        }

                        Destroy(cursor, 1f);
                    }

                    //�ٸ� ����� ����� ���
                    //temporaryGO �ʱ�ȭ
                    else
                    {
                        Destroy(cursor);

                        if (go == enemy1)
                        {
                            enemySelectCursorPos(enemy1);
                        }
                        else if (go == enemy2)
                        {
                            enemySelectCursorPos(enemy2);
                        }
                        else if (go == enemy3)
                        {
                            enemySelectCursorPos(enemy3);
                        }

                        temporaryGO = go;
                    }
                }
            }
        }
    }

    //Ŀ�� ��ġ ���� �� Ŀ�� ����
    private void enemySelectCursorPos(GameObject enemyPos)
    {
        Vector3 pos = enemyPos.transform.position;
        pos.y = enemyPos.transform.position.y + 2f;

        cursor = Instantiate(enemySelectCursor, pos, Quaternion.identity);
    }

    //������ ���� ������ ������ ���� ���� ���
    //true ��ȯ �ƴ� ��� false ��ȯ
    public bool enemyChoose(GameObject go, GameObject temporaryGO)
    {
        if (go == temporaryGO)
        {
            return true;
        }
        else { return false; }
    }

    //action Panel ���� ��ư ����
    //�ٸ� �гε��� �������� ��� return ó��
    public void attackButtonOn()
    {
        if (skillPanel.activeSelf == true || itemPanel.activeSelf == true)
        {
            return;
        }

        if (skillPanel.activeSelf == false || itemPanel.activeSelf == false)
        {
            actionGaugeManager.playerActionOn = false;

            playerCurrent = Enum.GetName(typeof(playerStateList), 1);
        }
    }

    private void action_AttackButton_On(GameObject attackedEnemy)
    {
        Vector3 pos = attackedEnemy.transform.position;

        if (attackedEnemy.name != "Enemy_Dragon(Clone)")
        {
            pos.z -= 1;
        }
        else if (attackedEnemy.name == "Enemy_Dragon(Clone)")
        {
            pos.z -= 2f;
        }

        if (action_AttackStart == true && action_movingStop == false)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position,
                pos, 8 * Time.deltaTime);

            player.transform.LookAt(pos);

            if (player.transform.position == pos)
            {
                action_movingStop = true;
            }
        }
    }

    private void action_AttackButton_Off()
    {
        if (action_AttackStart == true && action_movingStop == true && player.GetComponent<Player>().playerBusy == true)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position,
                playerPos.transform.position, 8 * Time.deltaTime);

            player.transform.LookAt(playerPos.transform.position);

            if (player.transform.position == playerPos.transform.position)
            {
                player.transform.LookAt(enemy2_attackPos.transform.position);

                action_movingStop = false;
                action_AttackStart = false;
                player.GetComponent<Player>().playerBusy = false;
                playerCurrent = Enum.GetName(typeof(playerStateList), 0);
                actionGaugeManager.playerActionSwitch = false;

                enemySelect = 0;
            }
        }
    }

    //action Panel ��ų ��ư ����
    //Skill_Fire���� ���� �ʱ�ȭ
    public void skillButtonOn()
    {
        if (skillPanel.activeSelf == true || itemPanel.activeSelf == true)
        {
            return;
        }

        if (skillPanel.activeSelf == false)
        {
            skillPanel.SetActive(true);
        }
    }

    public void SkillPanelQuitButton()
    {
        skillPanel.SetActive(false);
    }

    public void skill5_Start()
    {
        GameObject go = Instantiate(skill5_Dash,
            playerPos.transform.position, temporaryGO.transform.rotation);

        Destroy(go, 0.2f);

        Vector3 pos = temporaryGO.transform.position;
        pos.z -= 1;

        player.transform.position = pos;
    }

    //action Panel ������ ��ư ����
    public void ItemButtonOn()
    {
        if (skillPanel.activeSelf == true || itemPanel.activeSelf == true)
        {
            return;
        }

        if (itemPanel.activeSelf == false)
        {
            itemPanel.SetActive(true);
            playerCurrent = Enum.GetName(typeof(playerStateList), 3);
        }
    }

    //action Panel ���� ��ư ����
    public void GuardButtonOn()
    {
        if (skillPanel.activeSelf == true || itemPanel.activeSelf == true)
        {
            return;
        }

        if (skillPanel.activeSelf == false || itemPanel.activeSelf == false)
        {
            Main_BattleCameraManager(actionGaugeManager.playerActionOn);

            action_Guard = true;
            actionGaugeManager.playerActionOn = false;
            playerCurrent = Enum.GetName(typeof(playerStateList), 4);
        }
    }

    //action Panel �� ��ư ����
    public void RunButtonOn()
    {
        if (skillPanel.activeSelf == true || itemPanel.activeSelf == true)
        {
            return;
        }
    }

    //ī�޶� ����
    public void Main_BattleCameraManager(bool actionOnOff)
    {
        if (actionOnOff == false)
        {
            battlePlayerCamera.SetActive(false);
            battlePlayer.SetActive(false);

            battleMainCamera.SetActive(true);
            battleMain.SetActive(true);
        }

        else if (actionOnOff == true)
        {
            battlePlayerCamera.SetActive(true);
            battlePlayer.SetActive(true);

            battleMainCamera.SetActive(false);
            battleMain.SetActive(false);
        }
    }

    public void Main_VictoryCameraManager()
    {
        if (battle_Victory == true)
        {
            battleMainCamera.SetActive(false);
            battleMain.SetActive(false);

            battleVictoryCamera.SetActive(true);
            battleVictory.SetActive(true);
        }
    }
}
