using UnityEngine;

public class Enemy : Entity
{
    [Header("Move Info")]
    public float idleTime;
    public float moveSpeed;
    public float movingTime;
    public float rotateSpeed;

    [Header("Battle Info")]
    public statSO_enemy enemy_Stat;
    public bool battleStart;
    public int Lv;
    public float MaxHp;
    public float NowHp;
    public float MaxMp;
    public float NowMp;
    public float Atk;
    public float Def;
    public float Spd;
    public int Exp;
    public int Gold;

    //1,2,3에 따라 적 등장 
    public int enemyCount;

    public basicMaterialItemSO dropItme;

    public GameObject damageText;

    public EnemyStateMachine stateMachine { get; private set; }

    public Player player;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();

        enemySetting(battleStart, enemy_Stat.Lv, enemy_Stat.MaxHp, enemy_Stat.MaxMp,
            enemy_Stat.Atk, enemy_Stat.Def, enemy_Stat.Spd, enemy_Stat.Gold, enemy_Stat.Exp);

        if (gameObject.name != "Enemy_Dragon")
        {
            //첫 시작할 때 1-3까지 랜덤 숫자를 구해 적 등장시키게 만들기
            enemyCount = UnityEngine.Random.Range(1, 4);
        }

        else if (gameObject.name == "Enemy_Dragon")
        {
            enemyCount = 1;
        }
    }

    public void enemySetting(bool _battleStart, int _lv, float _maxHp, float _maxMp,
        float _atk, float _def, float _spd, int _gold, int _exp)
    {
        battleStart = _battleStart;
        Lv = _lv;
        MaxHp = _maxHp; NowHp = _maxHp;
        MaxMp = _maxMp; NowMp = _maxMp;
        Atk = _atk; Def = _def;
        Spd = _spd; Gold = _gold;
        Exp = _exp;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public void Damaged(int damage)
    {
        Vector3 pos = transform.position;
        pos.y = transform.position.y + 0.5f;
        transform.position = pos;

        GameObject damagePopup = Instantiate(damageText,
            transform.position, Quaternion.identity);
        DamagedPopup damagePopupText = damagePopup.GetComponent<DamagedPopup>();

        if (damage - Def <= 0)
        {
            damage = 0;

            damagePopupText.DamageSetup(damage);
        }
        else
        {
            damagePopupText.DamageSetup((int)(damage - Def));

            NowHp -= damage - Def;
        }

        if (gameObject.name == "Enemy_Slime(Clone)")
        {
            BattleSoundManager.instance.SlimeDamaged();
        }

        else if (gameObject.name == "Enemy_Cactus(Clone)")
        {
            BattleSoundManager.instance.CactusDamaged();
        }

        else if (gameObject.name == "Enemy_Mushroom(Clone)")
        {
            BattleSoundManager.instance.MushroomDamaged();
        }

        else if (gameObject.name == "Enemy_Dragon(Clone)")
        {
            BattleSoundManager.instance.DragonDamaged();
        }
    }

    public void enemyDie()
    {
        Destroy(gameObject);
    }

    public void enemyAttackCall()
    {
        transform.GetChild(0).gameObject.GetComponent<EnemyAttack>().dragonAttack();
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
}