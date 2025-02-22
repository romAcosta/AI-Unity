using UnityEngine;

public class AIPatrolState : AIState
{
    
    
    public AIPatrolState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIIdleState)).
            AddCondition(agent.destinationDistance,Condition.Predicate.LessOrEqual,1f);
        CreateTransition(nameof(AIChaseState)).
            AddCondition(agent.enemySeen,Condition.Predicate.Equal,true);
    }

    public override void OnEnter()
    {
        
        agent.movement.Destination = NavNode.GetRandomNavNode().transform.position;
        agent.movement.Resume();
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
