using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    #region player Move
    [Header("Move Info")]
    public float walkSpeed;
    public float runSpeed;
    public float rotateSpeed;
    public static bool runOnOff = false;
    #endregion

    #region player Stats
    [Header("Player State")]
    public statSO_player player_Stat;
    public int Lv;
    public int MaxExp;
    public int nowExp;
    public float MaxHp;
    public float NowHp;
    public float MaxMp;
    public float NowMp;
    public int Atk;
    public int Def;
    public int Spd;
    #endregion

    #region stateMachine
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerIdle2State idle2State { get; private set; }
    public PlayerWalkState walkState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerBattleState battleState { get; private set; }
    public PlayerGuardState guardState { get; private set; }
    public PlayerSkill1State skill1State { get; private set; }
    public PlayerSkill2State skill2State { get; private set; }
    public PlayerSkill3State skill3State { get; private set; }
    public PlayerSkill4State skill4State { get; private set; }
    public PlayerSkill5State skill5State { get; private set; }
    public PlayerItemState itemState { get; private set; }
    public PlayerDamagedState damagedState { get; private set; }
    public PlayerVictoryState victoryState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion

    [Header("sight Related")]
    public GameObject cam;

    [Header("skill1 Related")]
    public Transform skillFirePos;
    public GameObject skill1_Projectile;

    [Header("skill2 Related")]
    public GameObject skill2_Projectile;

    [Header("skill3 Related")]
    public GameObject skill3_Projectile;

    [Header("skill4 Related")]
    public GameObject skill4_Projectile;

    [Header("item Related")]
    public Transform itemStartPos;
    public GameObject framItem;
    public GameObject frambeItem;
    public GameObject hpRestoreItem;
    public GameObject mpRestoreItem;
    public GameObject allRestoreItem;

    //플레이어가 배틀에 들어갔을 때
    public static bool playerBattle;

    public GameObject damageText;

    public bool playerBusy { get; set; }

    public actionSystem actionSystem { get; set; }
    public actionGaugeManager actionGaugeManager;

    public InventorySO playerinventoty;

    public BattleInventorySystem battleInven;

    public SaveLoadSystem saveLoadSystem;

    public bool playerHIt;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        idle2State = new PlayerIdle2State(this, stateMachine, "Idle2");
        walkState = new PlayerWalkState(this, stateMachine, "Walk");
        runState = new PlayerRunState(this, stateMachine, "Run");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        battleState = new PlayerBattleState(this, stateMachine, "BattleIdle");
        guardState = new PlayerGuardState(this, stateMachine, "Guard");
        skill1State = new PlayerSkill1State(this, stateMachine, "Skill1");
        skill2State = new PlayerSkill2State(this, stateMachine, "Skill2");
        skill3State = new PlayerSkill3State(this, stateMachine, "Skill3");
        skill4State = new PlayerSkill4State(this, stateMachine, "Skill4");
        skill5State = new PlayerSkill5State(this, stateMachine, "Skill5");
        itemState = new PlayerItemState(this, stateMachine, "Item");
        damagedState = new PlayerDamagedState(this, stateMachine, "Damaged");
        victoryState = new PlayerVictoryState(this, stateMachine, "Victory");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");

        playerSetting(player_Stat.Lv, player_Stat.MaxExp, player_Stat.nowExp,
            player_Stat.MaxHp, player_Stat.NowHp, player_Stat.MaxMp, player_Stat.NowMp,
            player_Stat.Atk, player_Stat.Def, player_Stat.Spd);

        actionSystem = GameObject.Find("ActionSystem").GetComponent<actionSystem>();
    }

    public void playerSetting(int _lv, int _maxExp, int _nowExp,
        float _maxHp, float _nowHp, float _maxMp, float _nowMp,
        int _atk, int _def, int _spd)
    {
        Lv = _lv;
        MaxExp = _maxExp; nowExp = _nowExp;
        MaxHp = _maxHp; NowHp = _nowHp;
        MaxMp = _maxMp; NowMp = _nowMp;
        Atk = _atk; Def = _def;
        Spd = _spd;
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);

        if (Player.playerBattle == false && player_Stat.playerBattleInTransform != new Vector3(0, 0, 0))
        {
            saveLoadSystem.PlayerBattleInTransformLoad();
            saveLoadSystem.weaponEquipSwitchLoad();

            gameObject.transform.position = player_Stat.playerBattleInTransform;
        }
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.FixedUpdate();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            runOnOff = !runOnOff;
        }
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    //플레이어 데미지 처리
    public void Damaged(int damage)
    {
        Vector3 pos = transform.position;
        pos.y = transform.position.y + 0.5f;
        transform.position = pos;

        if (actionSystem.action_Guard == true)
        {
            if ((int)(damage - Def) / 2 <= 0)
            {
                damage = 0;
            }

            else if ((int)(damage - Def) / 2 > 0)
            {
                NowHp -= (int)(damage - Def) / 2;
            }

            GameObject damagePopup = Instantiate(damageText,
                transform.position, Quaternion.identity);

            DamagedPopup damagePopupText = damagePopup.GetComponent<DamagedPopup>();

            if ((int)(damage - Def) / 2 <= 0)
            {
                damage = 0;

                damagePopupText.DamageSetup((int)(damage));
            }
            else
            {
                damagePopupText.DamageSetup((int)(damage - Def) / 2);
            }

            playerHIt = true;
        }
        else
        {
            if ((int)(damage - Def) <= 0)
            {
                damage = 0;
            }

            else if ((int)(damage - Def) > 0)
            {
                NowHp -= (int)(damage - Def);
            }

            GameObject damagePopup = Instantiate(damageText,
                 transform.position, Quaternion.identity);

            DamagedPopup damagePopupText = damagePopup.GetComponent<DamagedPopup>();

            if ((int)(damage - Def) <= 0)
            {
                damage = 0;

                damagePopupText.DamageSetup((int)(damage));
            }
            else
            {
                damagePopupText.DamageSetup((int)(damage - Def));
            }

            playerHIt = true;
        }

        if (NowHp <= 0)
        {
            StartCoroutine(playerDead());
        }
    }

    IEnumerator playerDead()
    {
        SceneSoundManager.instance.musicStop();
        SceneSoundManager.instance.GameOverSound();

        playerBattle = false;
        GetComponent<Animator>().SetBool("BattleStart", false);
        stateMachine.ChangeState(deadState);

        yield return new WaitForSeconds(0.8f);

        SceneManager.LoadScene("GameOverScene");
    }

    //플레이어 회복 처리
    public void Restore(int Value)
    {
        if (playerBattle == false)
        {
            if (battleInven.battleItemTitle.text == "체력 물약")
            {
                NowHp += Value;
            }
            else if (battleInven.battleItemTitle.text == "마나 물약")
            {
                NowMp += Value;
            }
            else if (battleInven.battleItemTitle.text == "종합 물약")
            {
                NowHp += Value;
                NowMp += Value;
            }
        }

        else if (playerBattle == true)
        {
            Vector3 pos = transform.position;
            pos.y = transform.position.y + 1f;
            transform.position = pos;

            GameObject restorePopup = Instantiate(damageText,
                transform.position, Quaternion.identity);

            DamagedPopup damagePopupText = restorePopup.GetComponent<DamagedPopup>();

            damagePopupText.RestoreSetup(Value);

            if (actionSystem.itemUseTitle == "체력 물약")
            {
                NowHp += Value;
            }
            else if (actionSystem.itemUseTitle == "마나 물약")
            {
                NowMp += Value;
            }
            else if (actionSystem.itemUseTitle == "종합 물약")
            {
                NowHp += Value;
                NowMp += Value;
            }
        }

        if (NowHp >= MaxHp)
        {
            NowHp = MaxHp;
        }

        if (NowMp >= MaxMp)
        {
            NowMp = MaxMp;
        }
    }

    public void Equip(int _atk, int _def, int _spd)
    {
        Atk += _atk;
        Def += _def;
        Spd += _spd;
    }

    public void Unequip(int _atk, int _def, int _spd)
    {
        Atk -= _atk;
        Def -= _def;
        Spd -= _spd;
    }

    public void skill1Fire()
    {
        GameObject go = Instantiate(skill1_Projectile,
            skillFirePos/*.transform.position, Quaternion.identity*/);
    }

    public void skill2Fire()
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += 1.5f;

        BattleSoundManager.instance.Skill2();

        GameObject go = Instantiate(skill2_Projectile, pos, Quaternion.identity);
        Destroy(go, 2f);
    }

    public void skill3Fire()
    {
        GameObject go = Instantiate(skill3_Projectile, skillFirePos);
    }

    public void skill4Fire()
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += 1.51f;

        BattleSoundManager.instance.Skill4();

        GameObject go = Instantiate(skill4_Projectile, pos, Quaternion.identity);
        Destroy(go, 2f);
    }

    public void attackItemFire()
    {
        if (actionSystem.attackSingleItem == true)
        {
            GameObject go = Instantiate(framItem, itemStartPos.transform.position, Quaternion.identity);
        }

        if (actionSystem.attackGroupItem == true)
        {
            GameObject go = Instantiate(frambeItem, itemStartPos.transform.position, Quaternion.identity);
        }
    }

    public void RestoreItemFire()
    {
        if (actionSystem.itemUseTitle == "체력 물약")
        {
            GameObject go = Instantiate(hpRestoreItem, itemStartPos.transform.position, Quaternion.identity);
        }
        else if (actionSystem.itemUseTitle == "마나 물약")
        {
            GameObject go = Instantiate(mpRestoreItem, itemStartPos.transform.position, Quaternion.identity);
        }
        else if (actionSystem.itemUseTitle == "종합 물약")
        {
            GameObject go = Instantiate(allRestoreItem, itemStartPos.transform.position, Quaternion.identity);
        }
    }

    public void skill5_PunchSound()
    {
        BattleSoundManager.instance.Skill5_Punch();
    }

    public void skill5_KickSound()
    {
        BattleSoundManager.instance.Skill5_Kick();
    }

    public void skill5_FinishSound()
    {
        BattleSoundManager.instance.Skill5_Finish();
    }
}
