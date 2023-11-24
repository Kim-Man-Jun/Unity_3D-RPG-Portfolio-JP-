using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
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

        //플레이어가 적 타격 위치까지 도착했을 경우
        if (player.actionSystem.action_movingStop == true && player.playerBusy == false)
        {
            player.stateMachine.ChangeState(player.attackState);
            EffectSoundManager.instance.PlayerWandSwing();
        }

        //배틀 시작했을 때 움직임 막아놓기
        if (player.actionSystem.action_AttackStart == true)
        {
            return;
        }
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

            player.SetVelocity(dir.x * player.runSpeed, dir.z * player.runSpeed, player.rotateSpeed);
        }

        if (xInput == 0 && zInput == 0)
        {
            player.stateMachine.ChangeState(player.idleState);
        }

        //Lshift 토글에 따라 walk, run 구분
        if (Player.runOnOff == false)
        {
            if (xInput == 0 && zInput == 0)
            {
                player.stateMachine.ChangeState(player.idleState);
            }
            else
            {
                player.stateMachine.ChangeState(player.walkState);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.ZeroVelocity();
            player.stateMachine.ChangeState(player.attackState);
            EffectSoundManager.instance.PlayerWandSwing();
        }
    }
}
