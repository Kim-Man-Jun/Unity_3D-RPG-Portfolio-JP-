using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static actionSystem;

public class PlayerSkill5State : PlayerState
{
    public PlayerSkill5State(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
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

        if (triggerCalled == true)
        {
            player.actionSystem.skillNum5 = false;
            player.actionSystem.action_skill5Start = false;
            player.actionSystem.playerCurrent = Enum.GetName(typeof(playerStateList), 0);

            player.actionSystem.enemySelect = 0;

            player.transform.position = player.actionSystem.playerPos.transform.position;

            player.actionGaugeManager.playerActionSwitch = false;

            player.stateMachine.ChangeState(player.battleState);
        }
    }
}
