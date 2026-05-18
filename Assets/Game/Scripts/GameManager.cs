using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using LiteNetLib;
using PurrNet.Logging;
using PurrNet;
using System.Net;
using System.Net.Sockets;
using TMPro;
using System.Diagnostics;

public class GameManager : NetworkBehaviour
{
    public PurrNet.Transports.UDPTransport udpTransport;
    public TMP_InputField MyJoinCode;

    [Header("UI References")]
    [SerializeField] private TMP_InputField codeInputField;
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private TextMeshProUGUI gameVersion;

    private const int REQUIRED_LENGTH = 6;

    string GetGlobalIP()
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString("https://api.ipify.org");
            }
        }
        catch
        {
            return "Не удалось получить IP";
        }
    }
    void Start()
    {
        string publicIp = GetGlobalIP();
        UnityEngine.Debug.Log("Ваш глобальный IP: " + publicIp);
        MyJoinCode.text = IpJoinCode.IpToJoinCode(publicIp);
        gameVersion.text = $"v{Application.version}";
    }

    [System.Obsolete]
    public void HostLocal()
    {
        udpTransport.address = "127.0.0.1";
        UnityEngine.Debug.Log("Хост локально с IP " + udpTransport.address);
        NetworkManager.main.StartHost();

        FindObjectOfType<SceneManager>().ChangeScene();
    }

    [System.Obsolete]
    public void Host()
    {
        if (string.IsNullOrEmpty(codeInputField.text.Trim()))
        {
            udpTransport.address = GetGlobalIP();
            UnityEngine.Debug.Log("Хост с IP " + udpTransport.address);
            NetworkManager.main.StartHost();
        }
        else
        {
            udpTransport.address = IpJoinCode.JoinCodeToIp(codeInputField.text);
            UnityEngine.Debug.Log("Коннект с IP " + udpTransport.address);
            NetworkManager.main.StartClient();
        }

        FindObjectOfType<SceneManager>().ChangeScene();
    }

    public void RestartApplication()
    {
        string exePath = Application.dataPath.Replace("_Data", ".exe");
        Process.Start(exePath);
        Application.Quit();
    }

    private void Update()
    {
        string code = codeInputField.text.Trim();

        if (string.IsNullOrEmpty(code))
        {
            SetButtonState(true, "Host Game");
        }
        else if (code.Length == REQUIRED_LENGTH)
        {
            SetButtonState(true, "Join Game");
        }
        else
        {
            SetButtonState(false, "Invalid Code");
        }
    }

    private void SetButtonState(bool isActive, string text)
    {
        if (actionButton != null)
            actionButton.interactable = isActive;

        if (buttonText != null)
            buttonText.text = text;
    }
}