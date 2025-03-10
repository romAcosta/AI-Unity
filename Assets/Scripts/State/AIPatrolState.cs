using UnityEngine;

public class AIPatrolState : AIState
{
    
    
    public AIPatrolState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIIdleState)).
            AddCondition(agent.destinationDistance,Condition.Predicate.LessOrEqual,1f);
        CreateTransition(nameof(AIChaseState)).
            AddCondition(agent.enemySeen, Condition.Predicate.Equal, true).
            AddCondition(agent.enemyHealth, Condition.Predicate.Greater, 0 );
    }

    public override void OnEnter()
    {
        
        agent.movement.Destination = NavNode.GetRandomNavNode().transform.position;
        agent.movement.Resume();
    }

    public override void OnUpdate()
    {
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
