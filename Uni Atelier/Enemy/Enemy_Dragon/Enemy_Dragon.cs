using UnityEngine;

public class Enemy_Dragon : Enemy
{
    //스크립트 및 캐릭터 애니메이션 스테이트머신에 집어넣기
    public Enemy_Dragon_idleState idleState { get; private set; }
    public Enemy_Dragon_moveState moveState { get; private set; }
    public Enemy_Dragon_battleState battleState { get; private set; }
    public Enemy_Dragon_damagedState damagedState { get; private set; }
    public Enemy_Dragon_dieState dieState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_Dragon_idleState(this, stateMachine, "Idle", this);
        moveState = new Enemy_Dragon_moveState(this, stateMachine, "Move", this);
        battleState = new Enemy_Dragon_battleState(this, stateMachine, "Idle", this);
        damagedState = new Enemy_Dragon_damagedState(this, stateMachine, "Damaged", this);
        dieState = new Enemy_Dragon_dieState(this, stateMachine, "Die", this);

        Spd = UnityEngine.Random.Range(8, 16);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
