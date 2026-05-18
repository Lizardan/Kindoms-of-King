using PurrNet;
using UnityEngine;

public class PrcBox : NetworkBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetColor(Color.red);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetColor(Color.green);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetColor(Color.blue);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetColor(Color.black);

    }

    [ObserversRpc]
    void SetColor(Color color)
    {
        //GetComponent<Renderer>().material.color = color;
    }
}
