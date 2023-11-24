using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (triggerCalled == true && Player.playerBattle == false)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (Player.playerBattle == true && player.actionSystem.action_AttackStart == false)
        {
            stateMachine.ChangeState(player.battleState);
        }

        if (Player.playerBattle == true && player.playerBusy == true)
        {
            stateMachine.ChangeState(player.runState);
        }
    }
}

