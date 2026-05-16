using UnityEngine;
using PurrNet.StateMachine;

public class StateThree : StateNode<int>
{
    public override void Enter(bool asServer)
    {
        if (!asServer)
            Debug.Log("Entering StateThree with no data!");
    }

    public override void Enter(int data, bool asServer)
    {
        if (!asServer)
            Debug.Log($"Entering StateThree with data: {data} ");
    }

    public override void StateUpdate(bool asServer)
    {
        if (!asServer)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                machine.Next();

            }
        }
    }

    public override void Exit(bool asServer)
    {
        if (!asServer)
            Debug.Log("Exiting StateThree");
    }
}
