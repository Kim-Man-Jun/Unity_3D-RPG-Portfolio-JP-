using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private float idleTime;

    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.ZeroVelocity();

        idleTime = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (xInput != 0 || zInput != 0)
        {
            player.stateMachine.ChangeState(player.walkState);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && (xInput != 0 || zInput != 0))
        {
            player.stateMachine.ChangeState(player.runState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.ZeroVelocity();
            player.stateMachine.ChangeState(player.attackState);
            EffectSoundManager.instance.PlayerWandSwing();
        }

        if (Player.playerBattle == true && player.actionSystem.action_AttackStart == false)
        {
            player.stateMachine.ChangeState(player.battleState);
            player.GetComponent<Animator>().SetBool("BattleStart", true);
        }

        idleTime += Time.deltaTime;

        if (idleTime > 5)
        {
            player.stateMachine.ChangeState(player.idle2State);
            idleTime = 0;
        }
    }
}
