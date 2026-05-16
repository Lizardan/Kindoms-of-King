using UnityEngine;
using PurrNet;

public class CubeSpawner : NetworkBehaviour
{
    public GameObject cubePrefab;
    public GameObject _currentCube;
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        enabled = isOwner;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentCube) Destroy(_currentCube);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentCube) Destroy(_currentCube);
            _currentCube = Instantiate(cubePrefab, transform.position + transform.forward, transform.rotation);
        }
    }
}