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

        //�÷��̾ �� Ÿ�� ��ġ���� �������� ���
        if (player.actionSystem.action_movingStop == true && player.playerBusy == false)
        {
            player.stateMachine.ChangeState(player.attackState);
            EffectSoundManager.instance.PlayerWandSwing();
        }

        //��Ʋ �������� �� ������ ���Ƴ���
        if (player.actionSystem.action_AttackStart == true)
        {
            return;
        }
        //���Ž� ������ ����
        //ī�޶�� �����Ͽ� ������
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

        //Lshift ��ۿ� ���� walk, run ����
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
