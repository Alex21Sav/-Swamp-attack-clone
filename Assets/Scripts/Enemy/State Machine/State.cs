using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;   
    protected Player Target { get; set; }

    public void Enter (Player target)
    {
        if(enabled == false)
        {
            Target = target;
            enabled = true;
            foreach(var transitions in _transitions)
            {
                transitions.enabled = true;
                transitions.Init(target);
            }
        }
    }
    
    public void Exit()
    {
        if(enabled == true)
        {
            foreach (var transitions in _transitions)
            {
                transitions.enabled = false;
            }
            enabled = false;
        }
    }
    public State GetNextState()
    {
        foreach(var transition in _transitions)
        {
            if (transition.NeedTranslit)
                return transition.TargetState;
        }

        return null;
    }
}
