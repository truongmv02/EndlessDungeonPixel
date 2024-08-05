using UnityEngine;

public class WeaponShootLaserState : WeaponActiveState
{

    AddObjectHandle addObjectHandle;
    float cooldown;

    public override void Enter()
    {
        base.Enter();
        addObjectHandle.Position = (Vector2)weapon.AttackPoint.position;
        addObjectHandle.Direction = (Vector2)weapon.AttackPoint.right;
        addObjectHandle.Handle();
        cooldown = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (addObjectHandle.Object == null) return;

        addObjectHandle.Object.transform.up = (Vector2)weapon.AttackPoint.right;
        addObjectHandle.Object.transform.position = (Vector2)weapon.AttackPoint.position;
        cooldown += Time.deltaTime;
        if (cooldown >= weapon.Stats["DamageCooldown"].Value)
        {
            cooldown = 0;
            UseEnergy();
        }
    }

    public override void Exit()
    {
        base.Exit();
        ObjectPool.Instance.DestroyObject(addObjectHandle.Object);
        foreach (var condition in conditions)
        {
            condition.ResetCondition();
        }
    }

    public override void Init(StateMachine stateMachine, Animator animator, Stats stats)
    {
        base.Init(stateMachine, animator, stats);
        addObjectHandle = weapon.GetHandle<AddObjectHandle>();
        BaseUtils.ValidateCheckNullValue(addObjectHandle, nameof(addObjectHandle), nameof(WeaponShootState), animator.name);
    }
}
