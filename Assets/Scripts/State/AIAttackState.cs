using System.Collections;
using UnityEngine;

public class AIAttackState : AIState
{
    private float attackTimer ;
    private bool  hasAttacked;
    public AIAttackState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIIdleState)).
            AddCondition(agent.enemySeen,Condition.Predicate.Equal,false);
        CreateTransition(nameof(AIChaseState)).
            AddCondition(agent.timer,Condition.Predicate.LessOrEqual,0);
    }

    public override void OnEnter()
    {
        attackTimer = 1;
        hasAttacked = false;
        agent.timer.value = 2;
        //TODO Add attack animation
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
    }

    public override void OnExit()
    {
        
    }

    
}
