using UnityEngine;

public class PlayerBattleState : PlayerState
{
    private int presentHp;

    public PlayerBattleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.ZeroVelocity();

        presentHp = (int)player.NowHp;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //배틀 중 공격할 때 이동
        if (player.actionSystem.action_AttackStart == true && (player.actionSystem.action_movingStop == false || player.playerBusy == true))
        {
            player.stateMachine.ChangeState(player.runState);
        }

        if (player.actionSystem.action_Guard == true)
        {
            player.stateMachine.ChangeState(player.guardState);
        }

        if (player.actionSystem.action_skill1Start == true)
        {
            player.stateMachine.ChangeState(player.skill1State);
        }

        if (player.actionSystem.action_skill2Start == true)
        {
            player.stateMachine.ChangeState(player.skill2State);
        }

        if (player.actionSystem.action_skill3Start == true)
        {
            player.stateMachine.ChangeState(player.skill3State);
        }

        if (player.actionSystem.action_skill4Start == true)
        {
            player.stateMachine.ChangeState(player.skill4State);
        }

        if (player.actionSystem.action_skill5Start == true)
        {
            player.stateMachine.ChangeState(player.skill5State);
        }

        if (player.actionSystem.action_ItemStart == true)
        {
            player.stateMachine.ChangeState(player.itemState);
        }

        if (player.actionSystem.battle_Victory == true)
        {
            player.stateMachine.ChangeState(player.victoryState);
        }

        if(player.playerHIt == true)
        {
            player.stateMachine.ChangeState(player.damagedState);
        }
    }
}
