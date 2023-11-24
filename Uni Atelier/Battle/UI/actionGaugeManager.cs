using UnityEngine;
using UnityEngine.UI;

public class actionGaugeManager : MonoBehaviour
{
    [Header("Player Action Related")]
    public Player player;
    public Image playerIcon;
    public bool playerActionOn = false;

    public bool playerActionSwitch = false;

    [Header("Enemy Action Related")]
    public Transform enemy1Pos;
    public Transform enemy2Pos;
    public Transform enemy3Pos;

    public GameObject enemy1Stat;
    public GameObject enemy2Stat;
    public GameObject enemy3Stat;

    public Image enemy1Icon = null;
    public Image enemy2Icon = null;
    public Image enemy3Icon = null;

    public Image slimeIcon;
    public Image cactusIcon;
    public Image mushroomIcon;
    public Image dragonIcon;

    public bool enemy1ActionOn;
    public bool enemy2ActionOn;
    public bool enemy3ActionOn;

    public Transform enemyAttackPos;

    private float dragonDelay;

    [Header("Action Gague Related")]
    public RectTransform actionGaugeStart;
    public RectTransform actionGaugeQuit;
    public float actionAccelSpeed;

    [Header("Component Related")]
    public actionSystem ActionSystem;
    public RectTransform actionGaugeTransform;

    private void Start()
    {
        //플레이어 액션 아이콘 초기 위치 설정
        Vector3 startPos = playerIcon.rectTransform.localPosition;
        startPos.x = actionGaugeStart.localPosition.x / player.GetComponent<Player>().Spd * 6;
        playerIcon.rectTransform.localPosition = startPos;

        if (enemy2Pos.transform.childCount > 1)
        {
            enemy1Stat = enemy2Pos.transform.GetChild(1).gameObject;
        }

        if (enemy1Pos.transform.childCount > 1)
        {
            enemy2Stat = enemy1Pos.transform.GetChild(1).gameObject;
        }

        if (enemy3Pos.transform.childCount > 1)
        {
            enemy3Stat = enemy3Pos.transform.GetChild(1).gameObject;
        }

        if (enemy1Stat != null)
        {
            actionSystem_EnemyIconSetting(enemy1Stat, enemy1Stat.name, ref enemy1Icon);
        }

        if (enemy2Stat != null)
        {
            actionSystem_EnemyIconSetting(enemy2Stat, enemy2Stat.name, ref enemy2Icon);
        }

        if (enemy3Stat != null)
        {
            actionSystem_EnemyIconSetting(enemy3Stat, enemy3Stat.name, ref enemy3Icon);
        }
    }

    //에너미 액션 아이콘 생성 및 속도 설정
    private void actionSystem_EnemyIconSetting(GameObject enemyNumber, string enemyName, ref Image enemyIcon)
    {
        float randomValue = UnityEngine.Random.Range(3f, 6f);

        switch (enemyName)
        {
            case "Enemy_Slime(Clone)":

                Image slimeIconGO = Instantiate(slimeIcon, playerIcon.rectTransform.position, Quaternion.identity);

                slimeIconGO.transform.SetParent(actionGaugeTransform);

                Vector3 slimeStartPos = slimeIconGO.rectTransform.localPosition;

                slimeStartPos.x = actionGaugeStart.localPosition.x / enemyNumber.GetComponent<Enemy_Slime>().Spd * randomValue;
                slimeIconGO.rectTransform.localPosition = slimeStartPos;

                enemyIcon = slimeIconGO;

                break;

            case "Enemy_Cactus(Clone)":

                Image cactusIconGO = Instantiate(cactusIcon, playerIcon.rectTransform.position, Quaternion.identity);

                cactusIconGO.transform.SetParent(actionGaugeTransform);

                Vector3 cactusStartPos = cactusIconGO.rectTransform.localPosition;

                cactusStartPos.x = actionGaugeStart.localPosition.x / enemyNumber.GetComponent<Enemy_Cactus>().Spd * randomValue;
                cactusIconGO.rectTransform.localPosition = cactusStartPos;

                enemyIcon = cactusIconGO;

                break;

            case "Enemy_Mushroom(Clone)":

                Image mushroomIconGO = Instantiate(mushroomIcon, playerIcon.rectTransform.position, Quaternion.identity);

                mushroomIconGO.transform.SetParent(actionGaugeTransform);

                Vector3 mushroomStartPos = mushroomIconGO.rectTransform.localPosition;
                mushroomStartPos.x = actionGaugeStart.localPosition.x / enemyNumber.GetComponent<Enemy_Mushroom>().Spd * randomValue;
                mushroomIconGO.rectTransform.localPosition = mushroomStartPos;

                enemyIcon = mushroomIconGO;

                break;

            case "Enemy_Dragon(Clone)":

                Image dragonIconGO = Instantiate(dragonIcon, playerIcon.rectTransform.position, Quaternion.identity);

                dragonIconGO.transform.SetParent(actionGaugeTransform);

                Vector3 dragonStartPos = dragonIconGO.rectTransform.localPosition;
                dragonStartPos.x = actionGaugeStart.localPosition.x / enemyNumber.GetComponent<Enemy_Dragon>().Spd * randomValue;
                dragonIconGO.rectTransform.localPosition = dragonStartPos;

                enemyIcon = dragonIconGO;

                break;
        }
    }

    private void Update()
    {
        //플레이어 아이콘 이동 중
        if (playerActionOn == false &&
            (ActionSystem.playerCurrent == "Idle" || ActionSystem.playerCurrent == "Guard")
            && (enemy1ActionOn == false && enemy2ActionOn == false && enemy3ActionOn == false))
        {
            playerIcon.rectTransform.Translate
                (0, -player.Spd * actionAccelSpeed * Time.deltaTime, 0);
        }

        if (playerIcon.rectTransform.localPosition.x < actionGaugeQuit.localPosition.x)
        {
            playerActionOn = true;

            playerIcon.rectTransform.localPosition = actionGaugeStart.localPosition;
        }

        if (enemy1Stat != null)
        {
            enemyAction(enemy1Stat, ref enemy1ActionOn, enemy1Icon);
        }

        if (enemy2Stat != null)
        {
            enemyAction(enemy2Stat, ref enemy2ActionOn, enemy2Icon);
        }

        if (enemy3Stat != null)
        {
            enemyAction(enemy3Stat, ref enemy3ActionOn, enemy3Icon);
        }
    }

    //적들 게이지 아이콘 및 전체적인 움직임 제어
    private void enemyAction(GameObject enemyNumber, ref bool enemyActionSwitch, Image enemyIcon)
    {
        if (enemyActionSwitch == false && playerActionOn == false && playerActionSwitch == false)
        {
            switch (enemyNumber.name)
            {
                case "Enemy_Slime(Clone)":
                    if (enemy1ActionOn == false && enemy2ActionOn == false && enemy3ActionOn == false)
                    {
                        enemyIcon.rectTransform.Translate
                            (0, -enemyNumber.GetComponent<Enemy_Slime>().Spd * actionAccelSpeed
                            * Time.deltaTime, 0);
                    }
                    break;

                case "Enemy_Cactus(Clone)":
                    if (enemy1ActionOn == false && enemy2ActionOn == false && enemy3ActionOn == false)
                    {
                        enemyIcon.rectTransform.Translate
                             (0, -enemyNumber.GetComponent<Enemy_Cactus>().Spd * actionAccelSpeed
                            * Time.deltaTime, 0);
                    }
                    break;

                case "Enemy_Mushroom(Clone)":
                    if (enemy1ActionOn == false && enemy2ActionOn == false && enemy3ActionOn == false)
                    {
                        enemyIcon.rectTransform.Translate
                             (0, -enemyNumber.GetComponent<Enemy_Mushroom>().Spd * actionAccelSpeed
                            * Time.deltaTime, 0);
                    }
                    break;

                case "Enemy_Dragon(Clone)":
                    if (enemy1ActionOn == false && enemy2ActionOn == false && enemy3ActionOn == false)
                    {
                        enemyIcon.rectTransform.Translate
                              (0, -enemyNumber.GetComponent<Enemy_Dragon>().Spd * actionAccelSpeed
                            * Time.deltaTime, 0);
                    }
                    break;
            }
        }

        if (enemyIcon.rectTransform.localPosition.x < actionGaugeQuit.localPosition.x)
        {
            if (enemyIcon == enemy1Icon)
            {
                enemy1ActionOn = true;
            }

            if (enemyIcon == enemy2Icon)
            {
                enemy2ActionOn = true;
            }

            if (enemyIcon == enemy3Icon)
            {
                enemy3ActionOn = true;
            }

            enemyIcon.rectTransform.localPosition = actionGaugeStart.localPosition;
        }

        //에너미 행동 아이콘이 도달해 적들 공격 시작
        if (enemyActionSwitch == true && enemyNumber.name != "Enemy_Dragon(Clone)")
        {
            enemyNumber.GetComponent<Animator>().SetBool("Idle", false);
            enemyNumber.GetComponent<Animator>().SetBool("Move", true);

            enemyNumber.transform.position = Vector3.MoveTowards(enemyNumber.transform.position,
                enemyAttackPos.transform.position, 10 * Time.deltaTime);

            if (enemyNumber.transform.position == enemyAttackPos.transform.position
                && enemyNumber.transform.GetChild(0).gameObject.GetComponent<EnemyAttack>().hitOnce == false)
            {
                enemyNumber.GetComponent<Animator>().SetBool("Move", false);
                enemyNumber.GetComponent<Animator>().SetBool("Attack", true);
            }

            else if (enemyNumber.transform.GetChild(0).gameObject.GetComponent<EnemyAttack>().hitOnce == true
                && player != null)
            {
                enemyNumber.GetComponent<Animator>().SetBool("Attack", false);
                enemyNumber.GetComponent<Animator>().SetBool("Move", true);

                enemyNumber.transform.LookAt(enemyNumber.transform.parent.transform);

                enemyNumber.transform.position = Vector3.MoveTowards(enemyNumber.transform.position,
                    enemyNumber.transform.parent.transform.position, 15 * Time.deltaTime);

                if (enemyNumber.transform.position == enemyNumber.transform.parent.transform.position)
                {
                    enemyActionSwitch = false;
                    enemyNumber.transform.GetChild(0).gameObject.GetComponent<EnemyAttack>().hitOnce = false;

                    enemyNumber.transform.LookAt(player.transform.position);
                    enemyNumber.GetComponent<Animator>().SetBool("Move", false);
                    enemyNumber.GetComponent<Animator>().SetBool("Idle", true);
                }
            }
        }

        //드래곤 행동 개시
        else if (enemyActionSwitch == true && enemyNumber.name == "Enemy_Dragon(Clone)")
        {
            enemyNumber.GetComponent<Animator>().SetBool("Idle", false);
            enemyNumber.GetComponent<Animator>().SetBool("Attack", true);

            dragonDelay += Time.deltaTime;

            if (dragonDelay > 1.5f)
            {
                enemyNumber.GetComponent<Animator>().SetBool("Attack", false);
                enemyNumber.GetComponent<Animator>().SetBool("Idle", true);

                dragonDelay = 0;
                enemyActionSwitch = false;
            }
        }
    }
}
