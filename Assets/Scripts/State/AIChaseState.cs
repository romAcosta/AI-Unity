using UnityEngine;

public class AIChaseState :AIState
{
    
    public AIChaseState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIIdleState)).
            AddCondition(agent.enemyDistance,Condition.Predicate.Greater,4f);
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
