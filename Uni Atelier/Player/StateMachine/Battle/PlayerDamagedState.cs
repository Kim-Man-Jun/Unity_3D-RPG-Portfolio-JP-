using UnityEngine;

public class PlayerDamagedState : PlayerState
{
    float delayTime;

    public PlayerDamagedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
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

        delayTime += Time.deltaTime;

        if (delayTime > 1)
        {
            player.playerHIt = false;
            stateMachine.ChangeState(player.battleState);
        }
    }
}
