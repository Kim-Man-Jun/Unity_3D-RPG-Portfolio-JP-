using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictoryState : PlayerState
{
    public PlayerVictoryState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.ZeroVelocity();

        SceneSoundManager.instance.musicStop();
        SceneSoundManager.instance.BattleVicrotyWin();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
