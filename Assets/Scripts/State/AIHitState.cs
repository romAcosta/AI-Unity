using UnityEngine;

public class AIHitState : AIState
{
    
    
    public AIHitState(StateAgent agent) : base(agent)
    {
        
        CreateTransition(nameof(AIPatrolState)).
            AddCondition(agent.enemySeen,Condition.Predicate.Equal,false)
            .AddCondition(agent.timer, Condition.Predicate.LessOrEqual,0);
        CreateTransition(nameof(AIAttackState)).
            AddCondition(agent.enemyDistance,Condition.Predicate.LessOrEqual,1.5f)
            .AddCondition(agent.timer, Condition.Predicate.LessOrEqual,0);
    }

    public override void OnEnter()
    {
        agent.timer.value = 1;
        agent.animator.SetTrigger("Hit");
        agent.movement.Stop();
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
