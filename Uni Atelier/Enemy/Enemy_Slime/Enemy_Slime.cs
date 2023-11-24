using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy
{
    //��ũ��Ʈ �� ĳ���� �ִϸ��̼� ������Ʈ�ӽſ� ����ֱ�
    public Enemy_Slime_idleState idleState { get; private set; }
    public Enemy_Slime_moveState moveState { get; private set; }
    public Enemy_Slime_battleState battleState { get; private set; }
    public Enemy_Slime_damagedState damagedState { get; private set; }
    public Enemy_Slime_dieState dieState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_Slime_idleState(this, stateMachine, "Idle", this);
        moveState = new Enemy_Slime_moveState(this, stateMachine, "Move", this);
        battleState = new Enemy_Slime_battleState(this, stateMachine, "Idle", this);
        damagedState = new Enemy_Slime_damagedState(this, stateMachine, "Damaged", this);
        dieState = new Enemy_Slime_dieState(this, stateMachine, "Die", this);

        Spd = UnityEngine.Random.Range(3, 8);
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
