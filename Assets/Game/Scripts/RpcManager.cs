using UnityEngine;
using PurrNet;
using System.Threading.Tasks;

public class RpcManager : NetworkBehaviour
{
    [ServerRpc(requireOwnership: false)]
    public async static Task<bool> MyAwaiteableGenericRpc<T>(T data)
    {
        await Task.Delay(1000);
        Debug.Log($"Recevied generic data: {data} | Type: {typeof(T)}");
        //return typeof(T) == typeof(string);
        return data is string;
    }
}