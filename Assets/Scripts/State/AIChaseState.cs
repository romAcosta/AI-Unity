using UnityEngine;

public class AIChaseState :AIState
{
    
    public AIChaseState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIIdleState)).
            AddCondition(agent.enemyDistance,Condition.Predicate.GreaterOrEqual,4);
        CreateTransition(nameof(AIAttackState)).
            AddCondition(agent.enemyDistance, Condition.Predicate.LessOrEqual,1.5f);
    }

    public override void OnEnter()
    {
        agent.movement.MaxSpeed  *= 2;
    }

    public override void OnUpdate()
    {
        agent.movement.Destination = agent.enemy.transform.position;
        
    }

    public override void OnExit()
    {
        agent.movement.MaxSpeed  /= 2;
    }
}
