using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AIIdleState: AIState
{
    
    public AIIdleState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIPatrolState)).
            AddCondition(agent.timer,Condition.Predicate.LessOrEqual,0);
    }

    public override void OnEnter()
    {
        agent.timer.value = Random.Range(1f, 3f);
        agent.movement.Stop();
    }

    public override void OnUpdate()
    {
        
        
    }

    public override void OnExit()
    {
        
    }
}
