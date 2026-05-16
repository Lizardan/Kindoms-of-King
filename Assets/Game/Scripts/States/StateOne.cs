using UnityEngine;
using PurrNet.StateMachine;

public class StateOne : StateNode
{
    public override void Enter(bool asServer)
    {
        if (!asServer)
            Debug.Log("Entering StateOne");
    }

    public override void StateUpdate(bool asServer)
    {
        if (!asServer)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                machine.Next();
                //machine.Previous();
                //machine.SetState(nextState);
            }
        }
    }

    public override void Exit(bool asServer)
    {
        if (!asServer)
            Debug.Log("Exiting StateOne");
    }
}