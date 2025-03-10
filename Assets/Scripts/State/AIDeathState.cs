using UnityEngine;

public class AIDeathState : AIState
{
    
    
    public AIDeathState(StateAgent agent) : base(agent)
    {
        
    }

    public override void OnEnter()
    {
        
        agent.movement.Stop();
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
