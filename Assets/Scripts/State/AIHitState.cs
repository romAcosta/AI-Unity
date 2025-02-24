using UnityEngine;

public class AIHitState : AIState
{
    
    
    public AIHitState(StateAgent agent) : base(agent)
    {
        
        CreateTransition(nameof(AIPatrolState)).
            AddCondition(agent.enemySeen,Condition.Predicate.Equal,false);
        CreateTransition(nameof(AIAttackState)).
            AddCondition(agent.enemyDistance,Condition.Predicate.LessOrEqual,1.5f);
    }

    public override void OnEnter()
    {
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
