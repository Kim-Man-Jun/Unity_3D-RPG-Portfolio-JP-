using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class actionSystem : MonoBehaviour
{
    //플레이어 상태 목록
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
    //전투 화면 카메라
    public GameObject battleMain;
    //플레이어 카메라
    public GameObject battlePlayer;
    //전투 승리 카메라
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
    //공격버튼 눌렀을 때
    public bool action_AttackStart = false;
    //지정된 위치에 도착했을 때
    public bool action_movingStop;

    [Header("Enemy Select Related")]
    //적 지정할 때 2가 되면 공격 실행
    public int enemySelect = 0;
    //적 지정할 때 임시 대상, 두 번 째 선택한 대상이 이전과 같을 경우 공격 실행
    //아닐 경우 초기화
    public GameObject temporaryGO;
    //커서 오브젝트
    public GameObject enemySelectCursor;
    GameObject cursor;

    //전체 공격용
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
        //공격 아이템 실행 준비
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

        //회복 아이템 실행 준비
        if (itemUse.resItemUse == true)
        {
            restoreItem = true;

            itemUseTitle = itemUse.inBattleItemTitle.text;
            int.TryParse(itemUse.inBattleItemDmgResValue.text, out itemValue);
        }
    }

    private void Update()
    {
        //플레이어 HP, MP
        playerHpMpState();

        //적 유닛 선택 관련 raycast
        if (playerCurrent == "Attack" && action_AttackStart == false)
        {
            enemyMouseSelect();
            actionGaugeManager.playerActionSwitch = true;
        }

        //카메라 전환
        Main_BattleCameraManager(actionGaugeManager.playerActionOn);

        if (playerCurrent != "Idle")
        {
            GameObject go = GameObject.Find("HighlightPointer(Clone)");

            Destroy(go);
        }

        //플레이어 상태 attack일때
        if (playerCurrent == Enum.GetName(typeof(playerStateList), 1))
        {
            //어택 버튼 실행
            if (temporaryGO != null)
            {
                action_AttackButton_On(temporaryGO);
            }

            //어택 버튼 끝나고 복귀
            action_AttackButton_Off();
        }

        //적 단일대상 선택
        if (skillNum1 == true || skillNum5 == true)
        {
            playerCurrent = Enum.GetName(typeof(playerStateList), 2);

            skillPanel.SetActive(false);

            actionGaugeManager.playerActionOn = false;
            actionGaugeManager.playerActionSwitch = true;

            enemyMouseSelect();
        }

        //플레이어 자신 선택
        if (skillNum2 == true || skillNum4 == true)
        {
            playerCurrent = Enum.GetName(typeof(playerStateList), 2);

            skillPanel.SetActive(false);

            actionGaugeManager.playerActionOn = false;
            actionGaugeManager.playerActionSwitch = true;

            enemyMouseSelect();
        }

        //적 단체를 선택하도록 만들기
        if (skillNum3 == true)
        {
            playerCurrent = Enum.GetName(typeof(playerStateList), 2);

            skillPanel.SetActive(false);

            actionGaugeManager.playerActionOn = false;
            actionGaugeManager.playerActionSwitch = true;

            enemyMouseSelect();
        }

        //아이템 사용
        if (attackSingleItem == true || attackGroupItem == true || restoreItem == true)
        {
            playerCurrent = Enum.GetName(typeof(playerStateList), 3);

            itemPanel.SetActive(false);

            actionGaugeManager.playerActionOn = false;
            actionGaugeManager.playerActionSwitch = true;

            enemyMouseSelect();
        }

        //저장된 적의 숫자와 처치한 적의 숫자가 같아질 경우
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
        //플레이어나 적 선택 레이캐스트
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (skillNum2 == true || skillNum4 == true || restoreItem == true)
        {
            //플레이어 자신을 선택
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

        //공격 관련 스킬이나 아이템일 경우
        else
        {
            //레이어가 enemy일 경우
            if (Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Enemy")) && Input.GetMouseButtonDown(0))
            {
                GameObject go = hit.collider.gameObject;

                //스킬 3번 전체 공격기 선택할 때
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

                //일반 공격과 스킬 1, 5번, 단일 공격 아이템 선택할 때
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
                    //마우스 클릭한 대상과 임시 저장한 대상이 같을 경우 공격 실행
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

                    //다른 대상을 골랐을 경우
                    //temporaryGO 초기화
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

    //커서 위치 조정 후 커서 생성
    private void enemySelectCursorPos(GameObject enemyPos)
    {
        Vector3 pos = enemyPos.transform.position;
        pos.y = enemyPos.transform.position.y + 2f;

        cursor = Instantiate(enemySelectCursor, pos, Quaternion.identity);
    }

    //선택한 적과 그전에 선택한 적이 같을 경우
    //true 반환 아닐 경우 false 반환
    public bool enemyChoose(GameObject go, GameObject temporaryGO)
    {
        if (go == temporaryGO)
        {
            return true;
        }
        else { return false; }
    }

    //action Panel 공격 버튼 선택
    //다른 패널들이 나와있을 경우 return 처리
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

    //action Panel 스킬 버튼 선택
    //Skill_Fire에서 종료 초기화
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

    //action Panel 아이템 버튼 선택
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

    //action Panel 가드 버튼 선택
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

    //action Panel 런 버튼 선택
    public void RunButtonOn()
    {
        if (skillPanel.activeSelf == true || itemPanel.activeSelf == true)
        {
            return;
        }
    }

    //카메라 조정
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
