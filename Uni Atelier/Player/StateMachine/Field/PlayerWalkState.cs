using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
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

        //원신식 움직임 구현
        //카메라와 연동하여 움직임
        if (Player.playerBattle == false)
        {
            Vector3 camforward = player.cam.transform.forward;
            Vector3 camRight = player.cam.transform.right;

            camforward.y = 0;
            camRight.y = 0;

            Vector3 camforwardRelative = zInput * camforward;
            Vector3 camRightRelative = xInput * camRight;

            Vector3 dir = camforwardRelative + camRightRelative;

            player.SetVelocity(dir.x * player.walkSpeed, dir.z * player.walkSpeed, player.rotateSpeed);
        }

        if (xInput == 0 && zInput == 0)
        {
            player.stateMachine.ChangeState(player.idleState);
        }

        if (Player.runOnOff == true)
        {
            player.stateMachine.ChangeState(player.runState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.ZeroVelocity();
            player.stateMachine.ChangeState(player.attackState);
            EffectSoundManager.instance.PlayerWandSwing();
        }
    }

}
