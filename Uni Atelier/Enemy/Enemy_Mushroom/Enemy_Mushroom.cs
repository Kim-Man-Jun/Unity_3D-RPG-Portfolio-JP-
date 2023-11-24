using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    //스크립트 및 캐릭터 애니메이션 스테이트머신에 집어넣기
    public Enemy_Mushroom_idleState idleState { get; private set; }
    public Enemy_Mushroom_moveState moveState { get; private set; }
    public Enemy_Mushroom_battleState battleState { get; private set; }
    public Enemy_Mushroom_damagedState damagedState { get; private set; }
    public Enemy_Mushroom_dieState dieState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_Mushroom_idleState(this, stateMachine, "Idle", this);
        moveState = new Enemy_Mushroom_moveState(this, stateMachine, "Move", this);
        battleState = new Enemy_Mushroom_battleState(this, stateMachine, "Idle", this);
        damagedState = new Enemy_Mushroom_damagedState(this, stateMachine, "Damaged", this);
        dieState = new Enemy_Mushroom_dieState(this, stateMachine, "Die", this);

        Spd = UnityEngine.Random.Range(2, 18);
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
