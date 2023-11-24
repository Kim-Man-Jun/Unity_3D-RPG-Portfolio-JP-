using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cactus : Enemy
{
    //��ũ��Ʈ �� ĳ���� �ִϸ��̼� ������Ʈ�ӽſ� ����ֱ�
    public Enemy_Cactus_idleState idleState { get; private set; }
    public Enemy_Cactus_moveState moveState { get; private set; }
    public Enemy_Cactus_battleState battleState { get; private set; }
    public Enemy_Cactus_damagedState damagedState { get; private set; }
    public Enemy_Cactus_dieState dieState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_Cactus_idleState(this, stateMachine, "Idle", this);
        moveState = new Enemy_Cactus_moveState(this, stateMachine, "Move", this);
        battleState = new Enemy_Cactus_battleState(this, stateMachine, "Idle", this);

        damagedState = new Enemy_Cactus_damagedState(this, stateMachine, "Damaged", this);
        dieState = new Enemy_Cactus_dieState(this, stateMachine, "Die", this);

        Spd = UnityEngine.Random.Range(5, 10);
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
