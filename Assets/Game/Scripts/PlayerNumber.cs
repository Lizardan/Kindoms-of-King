using UnityEngine;
using PurrNet;
using TMPro;
using System;

public class PlayerNumber : NetworkBehaviour
{

    public SyncVar<MyData> myData = new();

    public SyncVar<int> playerNumber = new(0, ownerAuth: true);
    public TMP_Text numberText;

    public int tt = 1;

    void Awake()
    {
        numberText.text = "0";
        playerNumber.onChanged += OnPlayerNumberChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        playerNumber.onChanged -= OnPlayerNumberChanged;
    }


    private void OnPlayerNumberChanged(int newNumber)
    {
        numberText.text = newNumber.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerNumber.value++;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerNumber.value--;
        }
    }


    public struct MyData
    {
        public int name;
        public int age;
        public int height;
    }
}
