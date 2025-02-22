using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<String, AIState> states = new Dictionary<String, AIState>();
    
    public AIState CurrentState {get; private set;}

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void AddState(String name, AIState state)
    {
        Debug.Assert(!states.ContainsKey(name));
        states[name] = state;
    }

    public void SetState(String name)
    {
        Debug.Assert(states.ContainsKey(name));
        var newState = states[name];
        if (newState == CurrentState) return;
        CurrentState?.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}
