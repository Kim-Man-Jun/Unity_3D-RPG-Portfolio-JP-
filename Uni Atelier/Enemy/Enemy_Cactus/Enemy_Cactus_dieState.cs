using UnityEngine;

public class Enemy_Cactus_dieState : EnemyState
{
    protected Enemy_Cactus enemy;

    private float delayTime;

    public Enemy_Cactus_dieState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Cactus enemy)
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        delayTime = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        delayTime += Time.deltaTime;

        if (delayTime > 1)
        {
            BattleSoundManager.instance.CactusDie();
            SaveLoadSystem.enemyDiecount++;
            enemy.enemyDie();
        }
    }
}
