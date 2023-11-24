using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill4State : PlayerState
{
    public PlayerSkill4State(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
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

        if (player.actionSystem.skillNum4 == false && triggerCalled == true)
        {
            player.actionGaugeManager.playerActionSwitch = false;

            player.stateMachine.ChangeState(player.battleState);
        }
    }
}
