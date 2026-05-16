using UnityEngine;
using PurrNet.StateMachine;

public class StateTwo : StateNode
{
    public int myData;

    public override void Enter(bool asServer)
    {
        if (!asServer)
            Debug.Log("Entering StateTwo");
    }

    public override void StateUpdate(bool asServer)
    {
        if (!asServer)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                machine.Next(myData);
            }
        }

    }

    public override void Exit(bool asServer)
    {
        if (!asServer)
            Debug.Log("Exiting StateTwo");
    }
}
