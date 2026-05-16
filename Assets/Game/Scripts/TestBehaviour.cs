using UnityEngine;
using PurrNet;

public class TestBehaviour : NetworkBehaviour
{

    void Start()
    {
        //networkManager.onPlayerJoinedScene += SetNetworkManager();

        //NetworkManager.main.onPlayerJoined += SetNetworkManager();

        if (isOwner)
        {

        }

        if (isServer)
        {

        }

        if (owner.HasValue)
        {
            if (owner.Value == localPlayer)
            {

            }
        }
    }
}
