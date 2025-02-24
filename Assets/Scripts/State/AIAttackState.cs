using System.Collections;
using UnityEngine;

public class AIAttackState : AIState
{
    private float attackTimer ;
    private bool  hasAttacked;
    public AIAttackState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIIdleState)).
            AddCondition(agent.timer, Condition.Predicate.LessOrEqual, 0);
    }

    public override void OnEnter()
    {
        attackTimer = 1;
        hasAttacked = false;
        agent.timer.value = 2;
        
        agent.animator.SetTrigger("Attack");
        agent.movement.Stop();
    }

    public override void OnUpdate()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0 && !hasAttacked)
        {
            agent.Attack();
            hasAttacked = true;
        }
        // set destination to enemy position
        agent.movement.Destination = agent.enemy.transform.position;
        // rotate towards movement direction
        if (agent.movement.Direction != Vector3.zero)
        {
            agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, Quaternion.LookRotation(agent.movement.Direction, Vector3.up), Time.deltaTime * 5);
        }
    }

    public override void OnExit()
    {
        
    }

    
}
